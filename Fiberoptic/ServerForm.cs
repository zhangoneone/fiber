using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FiberopticServer.common;
using _98421;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;
using ZedGraph;
namespace FiberopticServer
{
    public partial class ServerForm : Form
    {
        public int ret;
        static uint buffernum = 4000;
        public static short[] abc = new short[7];//用于凑出16字节地址对齐，取值为8*n-3(1,3,5,7),调试时要加上，运行exe时去掉这句就ok
        public static short[] data_buffer = new short[buffernum]; //接收到板卡的数据数组
     //   List<importdll.Frame> frame_pool = new List<importdll.Frame>();//帧池
        importdll.Frame []frame_pool =new importdll.Frame[655];//帧池
        int fpPtr=0;//帧池指针
        Queue<importdll.WarningInfo> msgque = new Queue<importdll.WarningInfo>();//报警队列
        static private ReaderWriterLockSlim rw1 = new ReaderWriterLockSlim();//帧池读写锁
        public ushort buf_id;
        public byte stopped = 0;
        public uint access_cnt = 0, StartPos = 0;
        private int m_dev;
        byte scantlv = 1, samptvl = 1;
        int wrtdata = 200 , wrtpoint = 200;
        public static int getpointstop = 1;
        public static int ReflushTimestop = 1;
        public static int Real_Timestop = 1;
        string s = "采集方式：异步单buffer\r\n数据个数:" + buffernum + "\r\n" + "统计数据情况\r\n";
        System.Diagnostics.Stopwatch t = new System.Diagnostics.Stopwatch();
        long fre = System.Diagnostics.Stopwatch.Frequency;//获取计时器频率，静态方法
        Thread Recorddata,RecordPoint;//记录线程
        Thread GetData, ReflushUI;//获取数据线程、刷新UI线程
        public static Thread ReflushTimeData;//刷新实时数据的线程
        public static Thread GetTimeData;//获取实时数据的线程
        public static setRealPoint setrealpoint;
        public static int setPoint = 0;
        short reqSeq=0;//请求的帧号
        short flushSeq = 0;//刷新的帧号
        static int uipointnum = 2000;//ui上显示的点个数
        public static LineItem myCurve2;
        DataTable dt;
        DataRow dr;
        PointPairList list1 = new PointPairList();//坐标系1数据
        public static RollingPointPairList list2 = new RollingPointPairList(uipointnum);//坐标系2数据
        double MS=0;//list2的x轴,时间
        Single avgtime = 0;
        Single frame_rate=0;
        int getpointnum = 0;
        //c#开始和中间件通信
        importdll.Command cf=new importdll.Command();
        GraphPane myPane1;
        public static GraphPane myPane2;
        int tmp_start_sign = 0;
        importdll.ShowPoint showpoint = new importdll.ShowPoint();
        public delegate void MyInvoke();
        private void TextInput(short[] dataInput,int i)//数据写文件
        {
            foreach (var b in dataInput)
            {
                LogClass.WriteInforLog(b.ToString()+" ",i);
            }
        }
        private void TextInput(double[] dataInput, int i)//数据写文件
        {
            foreach (var b in dataInput)
            {
                LogClass.WriteInforLog(b.ToString() + " ", i);
            }
        }
        private void TextInput(short[] data)//cache_buffer数据写文件
        {
            string s = Directory.GetCurrentDirectory();
            System.IO.File.WriteAllText(@s + "//实时数据.txt", string.Empty);//先清空txt
            foreach (var b in data)
            {
                LogClass.WriteInforLognotime(b.ToString() + " ", "实时数据");
            }
        }
        private void PointInput()//Point数据写文件
        {
            foreach (var b in msgque)
            {
                LogClass.WriteInforLog(b.ToString() + " ", "异常点");
            }
        }
        private void logtext(string s)//保存测试信息
        {
            LogClass.WriteErrorLog(s + " ");
        }
        public ServerForm()//窗口初始化
        {
            InitializeComponent();
            createPane1(zedGraphControl1);
            createPane2(zedGraphControl2);
            myPane1 = zedGraphControl1.GraphPane;
            myPane2 = zedGraphControl2.GraphPane;
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;//允许跨线程访问控件，但不安全
        }
        public void set_show_point(int num)
        {
            uipointnum = num;
            showpoint.pointLength = uipointnum;//5s的数据
            showpoint.buffer = new double[showpoint.pointLength];
            list2 = new RollingPointPairList(uipointnum);//坐标系2数据
            GraphPane myPane = zedGraphControl2.GraphPane;
            myPane.XAxis.Scale.Max = uipointnum; //X轴最大2000
        }

        public void createPane1(ZedGraphControl zgc)
        {
            GraphPane myPane = zgc.GraphPane;

            //设置图标标题和x、y轴标题
            myPane.Title.Text = "后向散射曲线";
            myPane.XAxis.Title.Text = "x/m";
            myPane.YAxis.Title.Text = "波动值";

            //更改标题的字体
            FontSpec myFont = new FontSpec("Arial", 20, Color.Red, false, false, false);
            myPane.Title.FontSpec = myFont;
            myPane.XAxis.Title.FontSpec = myFont;
            myPane.YAxis.Title.FontSpec = myFont;

            // 造一些数据，PointPairList里有数据对x，y的数组
            Random y = new Random();
            list1 = new PointPairList();
            //for (int i = 0; i < 36; i++)
            //{
            //    double x = i;
            //    //double y1 = 1.5 + Math.Sin((double)i * 0.2);
            //    double y1 = y.NextDouble() * 1000;
            //    list1.Add(x, y1); //添加一组数据
            //}

            // 用list1生产一条曲线，标注是“后向散射曲线”
            LineItem myCurve = myPane.AddCurve("后向散射曲线", list1, Color.Red, SymbolType.None);

            //填充图表颜色
            myPane.Fill = new Fill(Color.White, Color.FromArgb(200, 200, 255), 45.0f);

            //以上生成的图标X轴为数字，下面将转换为日期的文本
            //string[] labels = new string[36];
            //for (int i = 0; i < 36; i++)
            //{
            //    labels[i] = System.DateTime.Now.AddDays(i).ToShortDateString();
            //}
            //myPane.XAxis.Scale.TextLabels = labels; //X轴文本取值
            myPane.XAxis.Type = AxisType.Text;   //X轴类型

            //画到zedGraphControl1控件中，此句必加
            zgc.AxisChange();

            //重绘控件
            Refresh();
        }
        public void createPane2(ZedGraphControl zgc)
        {
            GraphPane myPane = zgc.GraphPane;

            //设置图标标题和x、y轴标题
            myPane.Title.Text = "实时震动曲线";
            myPane.XAxis.Title.Text = "时间/ms";
            myPane.YAxis.Title.Text = "波动值";

            //更改标题的字体
            FontSpec myFont = new FontSpec("Arial", 20, Color.Red, false, false, false);
            myPane.Title.FontSpec = myFont;
            myPane.XAxis.Title.FontSpec = myFont;
            myPane.YAxis.Title.FontSpec = myFont;

            //myPane.XAxis.Scale.Min = 0; //X轴最小值0
            myPane.XAxis.Scale.Max = uipointnum; //X轴最大2000
            myPane.XAxis.Scale.MinorStep = 1;//X轴小步长1,也就是小间隔
            //myPane.XAxis.Scale.MajorStep = 100;//X轴大步长为5，也就是显示文字的大间隔

            //改变轴的刻度
            //  zedGraphControl1.AxisChange();

            // 用list2生产一条曲线，标注是“实时震动曲线”
            LineItem myCurve = myPane.AddCurve("实时震动曲线", list2, Color.Red, SymbolType.None);

            //填充图表颜色
            myPane.Fill = new Fill(Color.White, Color.FromArgb(200, 200, 255), 45.0f);

            myPane.XAxis.Type = AxisType.Text;   //X轴类型

            //画到zedGraphControl1控件中，此句必加
            zgc.AxisChange();

            //重绘控件
           // Refresh();
        }
        private void ServerForm_Load(object sender, EventArgs e)
        {
            //创建运行日志
            LogClass.WriteErrorLog("系统启动中...");
            //申请帧池载荷的内存
            for (int i = 0; i < frame_pool.Length; i++)
            {
                frame_pool[i].buffer = new double[buffernum];
            }
            showpoint.pointLength = uipointnum;//5s的数据
            showpoint.currentLoction=0;
            showpoint.buffer = new double[showpoint.pointLength];
            //设置报警项
            dt = new DataTable();
            dt.Columns.Add("报警位置/m");
            dt.Columns.Add("报警最大值");
            dt.Columns.Add("报警异常比%");
            dt.Columns.Add("报警确认比%");
            dt.Columns.Add("丢帧率%");
            dataGridView1.DataSource = dt;

        }//窗口加载事件
        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)//窗口关闭事件
        {
            if (m_dev >= 0)
            {
                //功能:停止异步模拟输入
                //如果是pre或者middle触发模式，startpos返回ad buffer第一个数据的位置
                //如果是pre或者middle触发模式，access_cnt返回ad buffer的数据个数。如果是双buffer，返回第二个buffer的数据首位置
                //    WD_DASK.WD_Buffer_Free((ushort)m_dev, data_buffer);
           //     WD_DASK.WD_AI_AsyncClear((ushort)m_dev, out StartPos, out access_cnt);
           //     WD_DASK.WD_Release_Card((ushort)m_dev);//释放卡
            }

        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)//启动板卡
        {
            cf.class_code = 0x00;
            cf.command_code = 0x00;
            cf.data_code = 0x00;
            importdll.Command_function(ref cf);
            toolStripMenuItem2.Enabled = false;
            参数设置ToolStripMenuItem.Enabled = true;
            采集方式ToolStripMenuItem.Enabled = true;
            //开启获取数据线程
            GetData = new Thread(GetFrameData);
            GetData.Priority = ThreadPriority.AboveNormal;
            GetData.Start();
        }

        public void GetFrameData()
        {
            importdll.Frame tmp_frame = new importdll.Frame();//定义单帧
            double[] tmp_buffer = new double[buffernum];//定义临时数组

            //数组 托管内存->非托管内存
            int sizeArray = (int)(Marshal.SizeOf(tmp_buffer[0]) * buffernum);//获取数组占用空间大小
            tmp_frame.ptr = Marshal.AllocHGlobal(sizeArray);//声明一个同样大小的空间

            //结构体 托管内存->非托管内存
            int size = Marshal.SizeOf(tmp_frame);//获取结构体占用空间大小
            IntPtr framePtr = Marshal.AllocHGlobal(size);//声明一个同样大小的空间

            while (ReflushTimestop==1)
            {
                while (tmp_start_sign != 1) ;
                //设置交互的参数
                tmp_frame.Seq = reqSeq++;
                reqSeq %= 655;
                //数组 托管内存->非托管内存
                Marshal.Copy(tmp_buffer, 0, tmp_frame.ptr, (int)buffernum);//将数组放到这个空间中

                //结构体 托管内存->非托管内存
                Marshal.StructureToPtr(tmp_frame, framePtr, false);//将结构体放到这个空间中

                t.Restart();
                //执行函数，获取数据帧
                while (importdll.Data_function(framePtr) != 0) ;//阻塞读取
                t.Stop();
                long time = t.ElapsedTicks;
              //  Console.WriteLine("阻塞读取毫秒数：" + (time / (fre + 1.0 - 1.0)) * 1000);

                //非托管内存->托管内存 数组
                Marshal.Copy(tmp_frame.ptr, tmp_buffer, 0, (int)buffernum);
                //非托管内存->托管内存 结构
                tmp_frame = (importdll.Frame)Marshal.PtrToStructure(framePtr, typeof(importdll.Frame));
                //获取写者锁
                rw1.EnterWriteLock();
                //帧复制到帧池
                frame_pool[tmp_frame.Seq].Seq = tmp_frame.Seq;
                frame_pool[tmp_frame.Seq].time_cost = tmp_frame.time_cost;
                for (int i = 0; i < tmp_buffer.Length; i++)
                {
                    frame_pool[tmp_frame.Seq].buffer[i] = tmp_buffer[i];
                }
                fpPtr++;
                fpPtr %= 655;//帧池指针移动
                //释放写者锁
                rw1.ExitWriteLock();
                importdll.WarningInfo wi = new importdll.WarningInfo();
                //轮询报警信息
                if( importdll.Warning_function(ref wi) !=-1)
                    //报警信息入队
                    msgque.Enqueue(wi);
              //  Thread.Sleep(500);
            }
        }


        public void GetPointData()
        {
            importdll.Point tmp_point = new importdll.Point();//定义点帧
            double[] tmp_buffer = new double[buffernum];//定义临时数组
            double time = 0;
            long k=0;
            //数组 托管内存->非托管内存
            int sizeArray = (int)(Marshal.SizeOf(tmp_buffer[0]) * buffernum);//获取数组占用空间大小
            tmp_point.ptr = Marshal.AllocHGlobal(sizeArray);//声明一个同样大小的空间

            //结构体 托管内存->非托管内存
            int size = Marshal.SizeOf(tmp_point);//获取结构体占用空间大小
            IntPtr pointPtr = Marshal.AllocHGlobal(size);//声明一个同样大小的空间

            while (getpointstop==1)
            {
                t.Restart();
                k++;
                tmp_point.point = setrealpoint.GetPoint();
                //数组 托管内存->非托管内存
                Marshal.Copy(tmp_buffer, 0, tmp_point.ptr, (int)buffernum);//将数组放到这个空间中

                //结构体 托管内存->非托管内存
                Marshal.StructureToPtr(tmp_point, pointPtr, false);//将结构体放到这个空间中
                //执行函数，获取数据帧
                while (importdll.Point_function(pointPtr) != 0) ;//阻塞读取
            //    Console.WriteLine("阻塞读取毫秒数：" + (t.ElapsedTicks / (fre + 1.0 - 1.0)) * 1000);

                //非托管内存->托管内存 数组
                Marshal.Copy(tmp_point.ptr, tmp_buffer, 0, (int)buffernum);
                //非托管内存->托管内存 结构
                tmp_point = (importdll.Point)Marshal.PtrToStructure(pointPtr, typeof(importdll.Point));
                //获取写者锁
             //   rw1.EnterWriteLock();
                //帧复制到显示数组
                for(int i=0;i<(tmp_point.StopSeq-tmp_point.StartSeq);i++)
                {
                    showpoint.buffer[showpoint.currentLoction] = tmp_buffer[tmp_point.StartSeq + i];
                    showpoint.currentLoction++;
                    showpoint.currentLoction %= uipointnum;
                }
                t.Stop();
                //   time += ((t.ElapsedTicks / (fre + 1.0 - 1.0)) * 1000) / (tmp_point.StopSeq - tmp_point.StartSeq);
                 time += ((t.ElapsedTicks / (fre + 1.0 - 1.0)) * 1000);
                 time += 200;
                avgtime = (Single)(time / k);
                getpointnum = (tmp_point.StopSeq - tmp_point.StartSeq);
                //释放写者锁
            //    rw1.ExitWriteLock();
                Thread.Sleep(200);
            }
        }

        private void 异步采集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cf.class_code = 0x03;
            cf.command_code = 0x07;
            cf.data_code = 0x00;
            importdll.Command_function(ref cf);
            //启动c++数据处理和回传线程
            cf.class_code = 0x04;
            cf.command_code = 0x08;
            cf.data_code = 0x00;
            importdll.Command_function(ref cf);
            tmp_start_sign = 1;
            回调采集ToolStripMenuItem.Enabled = false;
        }
        private void 回调采集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buffernum = (uint)toolStripComboBox1.SelectedItem;
            cf.class_code = 0x02;
            cf.command_code = 0x04;
            cf.data_code = (short)buffernum;
            importdll.Command_function(ref cf);
        }
        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            scantlv = samptvl = (byte)toolStripComboBox2.SelectedItem;
            cf.class_code = 0x01;
            cf.command_code = 0x02;
            cf.data_code = (short)scantlv;
            importdll.Command_function(ref cf);
        }
        private void 数据查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe",Directory.GetCurrentDirectory()+"\\实时数据.txt");
        }
        private void toolStripComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            wrtdata = (int)toolStripComboBox3.SelectedItem;
            cf.class_code = 0x02;
            cf.command_code = 0x05;
            cf.data_code = (short)wrtdata;
            importdll.Command_function(ref cf);
        }
        private void toolStripComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            wrtpoint = (int)toolStripComboBox4.SelectedItem;
            cf.class_code = 0x02;
            cf.command_code = 0x06;
            cf.data_code = (short)wrtpoint;
            importdll.Command_function(ref cf);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
           // Recorddata = new Thread();
          //  Recorddata.Priority = ThreadPriority.BelowNormal;
          //  Recorddata.Start();
        }
        private void timer2point写文件_Tick(object sender, EventArgs e)
        {
            RecordPoint = new Thread(PointInput);
            RecordPoint.Priority = ThreadPriority.BelowNormal;
            RecordPoint.Start();
        }
        private void 数据写文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            数据写文件ToolStripMenuItem.Enabled = false;
            关闭写数据ToolStripMenuItem.Enabled = true;
            timer1data写文件.Enabled = true;
            timer1data写文件.Start();
        }
        private void 关闭写数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            关闭写数据ToolStripMenuItem.Enabled = false;
            数据写文件ToolStripMenuItem.Enabled = true;
            timer1data写文件.Stop();//关闭写文件定时器
            Recorddata.Abort();
            timer1data写文件.Enabled = false;
        }
        private void 停止记录异常点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            停止记录异常点ToolStripMenuItem.Enabled = false;
            异常点记录ToolStripMenuItem.Enabled = true;
            RecordPoint.Abort();
            timer2point写文件.Stop();
            timer2point写文件.Enabled = false;
        }
        private void 异常点记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            异常点记录ToolStripMenuItem.Enabled = false;
            停止记录异常点ToolStripMenuItem.Enabled = true;
            timer2point写文件.Enabled = true;
            timer2point写文件.Start();
        }
        private void 异常点查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", Directory.GetCurrentDirectory() + "\\异常点.txt");
        }
        private void 运行日志ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", Directory.GetCurrentDirectory() + "\\运行日志.txt");
        }
        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe",Directory.GetCurrentDirectory()+"\\帮助文档.html");
        }

        private void 历史记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void StartBand_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void StartCollect_Click(object sender, EventArgs e)
        {

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {
            this.zedGraphControl1.GraphPane.Title.Text = "后向散射曲线";
        }

        private void zedGraphControl2_Load(object sender, EventArgs e)
        {
            this.zedGraphControl2.GraphPane.Title.Text = "实时震动曲线";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //注销采集卡
            cf.class_code = 0x00;
            cf.command_code = 0x01;
            cf.data_code = 0x00;
            importdll.Command_function(ref cf);
            getpointstop = 0;
            Real_Timestop = 0;
            ReflushTimestop = 0;
            Thread.Sleep(400);
            if (GetTimeData.IsAlive)
            {
                GetTimeData.Abort(); 
            }
            if (ReflushTimeData.IsAlive)
            {
                ReflushTimeData.Abort();
            }
            if (GetData.IsAlive)
            {
                GetData.Abort();
            }
            Form.ActiveForm.Close();
        }

        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cf.class_code = 0x04;
            cf.command_code = 0x08;
            cf.data_code = 0x00;
            importdll.Command_function(ref cf);
        }

        private void ui_reflush_Tick(object sender, EventArgs e)
        {
            if (frame_pool[flushSeq].time_cost== 0)//帧尚未取
                goto Err;
        //    if (!frame_pool.Contains(fram e_pool[flushSeq]))
        //        goto Err;
            //清理上一次list1
            list1.RemoveRange(0, list1.Count);
            myPane1.CurveList.Clear();
            //清理datagridviewer
            if (dt.Rows.Count > 4)
            {
                dt.Rows.Remove(dt.Rows[0]);
            }
            //获取读者锁
            rw1.EnterReadLock();
            for (int i = 0; i < buffernum; i++)
            {
                list1.Add(i, frame_pool[flushSeq].buffer[i]);
            }
            //释放读者锁
            rw1.ExitReadLock();
            // 用list1生产一条曲线，标注是""
            LineItem myCurve1 = myPane1.AddCurve("", list1, Color.Red, SymbolType.None);

            //画到zedGraphControl1控件中
            zedGraphControl1.AxisChange();

            //更新报警信息
            if (msgque.Count > 0)
            {
                dr = dt.NewRow();
                dr[0] = msgque.Peek().location;
                dr[1] = msgque.Peek().warningMax;
                dr[2] = msgque.Peek().warningExecRate;
                dr[3] = msgque.Peek().warningAckRate;
                dr[4] = msgque.Peek().loseFramRate;
                dt.Rows.Add(dr);
                msgque.Dequeue();//删除一个元素
            }
            //重绘控件
            Refresh();
            flushSeq++;
        Err:
            flushSeq %= 655;
         //   Thread.Sleep(500);//休眠500ms
        }

        private void 显示实时曲线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setrealpoint = new setRealPoint();
            setrealpoint.Show();
            GetTimeData = new Thread(GetPointData);
            GetTimeData.Priority = ThreadPriority.AboveNormal;
            ReflushTimeData = new Thread(Real_Time_Data);
            ReflushTimeData.Priority = ThreadPriority.AboveNormal;
        }
        private void Real_Time_Data()
        {
            //this.zedGraphControl2.GraphPane.XAxis.Scale.MajorStepAuto = true;
            //this.zedGraphControl2.GraphPane.YAxis.Scale.MajorStepAuto = true;
            //this.zedGraphControl2.GraphPane.XAxis.Scale.MinorStepAuto = true;
            //this.zedGraphControl2.GraphPane.YAxis.Scale.MinorStepAuto = true;
            double time = 0;
            int i = showpoint.currentLoction;//记录指针
            createPane2(zedGraphControl2);
            while (Real_Timestop==1)
            {
                t.Restart();
                label1.Text =Convert.ToString(avgtime)+" ms";
                label6.Text = getpointnum.ToString();
                myPane2.CurveList.Clear();
                //检索showpoint内的新添加帧
                for (; i != showpoint.currentLoction; i++, i %= uipointnum)
                {
                    time+=1;
                    list2.Add(time, showpoint.buffer[i]);
                }
                // 用list2产生曲线，标注是""
                myCurve2 = myPane2.AddCurve("", list2, Color.Red, SymbolType.None);
                //画到zedGraphControl2控件中
                zedGraphControl2.AxisChange();
                //重绘控件
               // Refresh();
                zedGraphControl2.Invalidate();
                t.Stop();
                frame_rate = (Single)(t.ElapsedTicks / (fre + 1.0 - 1.0)) * 1000;
                label2.Text = (frame_rate).ToString()+" ms";
            //    Thread.Sleep(100);
            }


        }


        private void k1ToolStripMenuItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void k2ToolStripMenuItem_TextChanged(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x0f;
            cf.data_code = Convert.ToDouble(k2ToolStripMenuItem.Text);
            importdll.Command_function(ref cf);
        }

        private void 阈值ToolStripMenuItem_TextChanged(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x10;
            cf.data_code = Convert.ToDouble(阈值ToolStripMenuItem.Text);
            importdll.Command_function(ref cf);
        }

        private void 异常比ToolStripMenuItem_TextChanged(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x11;
            cf.data_code = Convert.ToDouble(异常比ToolStripMenuItem.Text) / 100;
            importdll.Command_function(ref cf);
        }

        private void 确认比ToolStripMenuItem_TextChanged(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x12;
            cf.data_code = Convert.ToDouble(确认比ToolStripMenuItem.Text) / 100;
            importdll.Command_function(ref cf);
        }

        private void toolStripTextBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x0e;
            cf.data_code = Convert.ToDouble( toolStripTextBox1.SelectedItem);
            importdll.Command_function(ref cf);
        }

        private void toolStripTextBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x0f;
            cf.data_code = Convert.ToDouble( toolStripTextBox2.SelectedItem);
            importdll.Command_function(ref cf);
        }

        private void toolStripTextBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x10;
            cf.data_code = Convert.ToDouble( toolStripTextBox3.SelectedItem);
            importdll.Command_function(ref cf);
        }

        private void toolStripTextBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x11;
            cf.data_code = Convert.ToDouble( toolStripTextBox4.SelectedItem)/100;
            importdll.Command_function(ref cf);
        }

        private void toolStripComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x12;
            cf.data_code = Convert.ToDouble( toolStripComboBox5.SelectedItem )/ 100;
            importdll.Command_function(ref cf);
        }

        private void toolStripTextBox1_TextUpdate(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x0e;
            cf.data_code = Convert.ToDouble(toolStripTextBox1.Text);
            importdll.Command_function(ref cf);
        }

        private void toolStripTextBox2_TextUpdate(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x0f;
            cf.data_code = Convert.ToDouble(toolStripTextBox2.Text);
            importdll.Command_function(ref cf);
        }

        private void toolStripTextBox3_TextUpdate(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x10;
            cf.data_code = Convert.ToDouble(toolStripTextBox3.Text);
            importdll.Command_function(ref cf);
        }

        private void toolStripTextBox4_TextUpdate(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x11;
            cf.data_code = Convert.ToDouble(toolStripTextBox4.Text);
            importdll.Command_function(ref cf);
        }

        private void toolStripComboBox5_TextUpdate(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x12;
            cf.data_code = Convert.ToDouble(toolStripComboBox5.Text);
            importdll.Command_function(ref cf);
        }

        private void toolStripComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_show_point(Convert.ToInt32(toolStripComboBox6.SelectedItem));
        }

        private void toolStripComboBox6_TextUpdate(object sender, EventArgs e)
        {
            set_show_point(Convert.ToInt32(toolStripComboBox6.Text));
        }
    }
}
