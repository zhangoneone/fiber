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
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;
using ZedGraph;
using System.Globalization;
namespace FiberopticServer
{
    public partial class ServerForm : Form
    {
        public int ret;
        static uint buffernum = 5000;
        importdll.Frame []frame_pool =new importdll.Frame[655];//帧池
        int Max_Thread_User = 10;
        Queue<importdll.WarningInfo> msgque = new Queue<importdll.WarningInfo>();//报警队列
        static private ReaderWriterLockSlim rw1 = new ReaderWriterLockSlim();//帧池读写锁
        public ushort buf_id;
        public byte stopped = 0;
        public uint access_cnt = 0, StartPos = 0;
        public static byte scantlv = 20, samptvl = 20;
        public static int getpointstop = 1;
        public static int ReflushTimestop = 1;
        public static int Real_Timestop = 1;
        System.Diagnostics.Stopwatch t = new System.Diagnostics.Stopwatch();
        long fre = System.Diagnostics.Stopwatch.Frequency;//获取计时器频率，静态方法
        Thread GetData;//获取数据线程、刷新UI线程
        public static Thread ReflushTimeData;//刷新实时数据的线程
        public static Thread GetTimeData;//获取实时数据的线程
        public static Thread ScatteringRecord;//散射数据记录线程
        public static int ScatteringRecordFlag = 0;//线程同步标记位
        public static Thread RealPointRecord;//实时点数据记录线程
        public static int RealPointRecordFlag = 0;//线程同步标记位
        public static Thread LoseFrameRecord;//丢帧率数据记录线程
        public static int LoseFrameRecordFlag = 0;//线程同步标记位
        public static int CanCollectionWarning = 0;//报警信息采集开关
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
        SetZoom1 sz;//设置报警区域类
        //c#开始和中间件通信
        importdll.Command cf=new importdll.Command();
        GraphPane myPane1;
        GraphPane myPane2;
        int tmp_start_sign = 0;
        importdll.ShowPoint showpoint = new importdll.ShowPoint();
        public delegate void MyInvoke();
        public MyInvoke TimeSetEnd0;
        public MyInvoke TimeSetEnd1;
        public string StartTime散射;
        public string StopTime散射;
        public string StartTime实时;
        public string StopTime实时;
        OrderTime ot0, ot1;
        string SoftwareVersion = "REALEASE VER1.0";
        string CopyRight = "COPYRIGHT @AHNU\r\n\r\n\r\n\r\nBUILD IN 2018.06";
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
            RealPointRecordFlag = 0;
            ReflushTimeData.Abort();
            uipointnum = num;
            showpoint.currentLoction = 0;//指针清0
            showpoint.pointLength = uipointnum;//uipointnum ms的数据
            showpoint.buffer = new double[showpoint.pointLength];
            list2.Clear();
            list2 = new RollingPointPairList(uipointnum);//坐标系2数据
            GraphPane myPane = zedGraphControl2.GraphPane;
            myPane.XAxis.Scale.Max = uipointnum; //X轴最大2000
            RealPointRecordFlag = 1;
            ReflushTimeData = new Thread(Real_Time_Data);
            ReflushTimeData.Start();
        }

        public void createPane1(ZedGraphControl zgc)
        {
            GraphPane myPane = zgc.GraphPane;

            //设置图标标题和x、y轴标题
            myPane.Title.Text = "后向散射曲线";
            myPane.XAxis.Title.Text = "距离/m";
            myPane.YAxis.Title.Text = "振动幅度";

            myPane.XAxis.Scale.Max = 50000; //X轴最大值

            //更改标题的字体
            FontSpec myFont = new FontSpec("Arial", 20, Color.Red, false, false, false);
            myPane.Title.FontSpec = myFont;
            myPane.XAxis.Title.FontSpec = myFont;
            myPane.YAxis.Title.FontSpec = myFont;

            list1 = new PointPairList();

            // 用list1生产一条曲线，标注是“后向散射曲线”
            LineItem myCurve = myPane.AddCurve("后向散射曲线", list1, Color.Red, SymbolType.None);

            //填充图表颜色
            myPane.Fill = new Fill(Color.White, Color.FromArgb(200, 200, 255), 45.0f);

            myPane.XAxis.Scale.Format = "0km";

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
            myPane.XAxis.Title.Text = "监测位置:0m处\r\n时间单位:ms";
            myPane.YAxis.Title.Text = "振动幅度";

            //更改标题的字体
            FontSpec myFont = new FontSpec("Arial", 20, Color.Red, false, false, false);
            myPane.Title.FontSpec = myFont;
            myPane.XAxis.Title.FontSpec = myFont;
            myPane.YAxis.Title.FontSpec = myFont;

            //myPane.XAxis.Scale.Min = 0; //X轴最小值0
            myPane.XAxis.Scale.Max = uipointnum; //X轴最大2000
            myPane.XAxis.Scale.MinorStep = 1;//X轴小步长1,也就是小间隔
            myPane.XAxis.Scale.MajorStep = 100;//X轴大步长为5，也就是显示文字的大间隔

            //改变轴的刻度
              zedGraphControl1.AxisChange();

            // 用list2生产一条曲线，标注是“实时震动曲线”
            LineItem myCurve = myPane.AddCurve("实时震动曲线", list2, Color.Red, SymbolType.None);

            //填充图表颜色
            myPane.Fill = new Fill(Color.White, Color.FromArgb(200, 200, 255), 45.0f);

            myPane.XAxis.Type = AxisType.Text;   //X轴类型
            //画到zedGraphControl1控件中，此句必加
            zgc.AxisChange();

            //重绘控件
            Refresh();
        }
        //临时隐藏标签
        public void HideLabel()
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
         //   label6.Visible = false;
        }
        private void ServerForm_Load(object sender, EventArgs e)
        {
            //创建运行日志
            LogClass.WriteErrorLog("系统启动中...");
            //申请帧池载荷和user内存
            for (int i = 0; i < frame_pool.Length; i++)
            {
                frame_pool[i].buffer = new double[buffernum];
                frame_pool[i].user = new importdll.user_manage[Max_Thread_User];
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
          //  dt.Columns.Add("丢帧率%");
            dt.Columns.Add("报警区间");
            dataGridView1.DataSource = dt;

            显示实时曲线ToolStripMenuItem.Enabled = false;
            采样点数量ToolStripMenuItem.Enabled = false;
            //设置开关
            toolStripMenuItem4.Checked = true;
            toolStripMenuItem6.Checked = true;
            toolStripMenuItem3.Enabled = false;

            button1.Text = "OFF";
            button2.Text = "OFF";
            button1.Enabled = false;
            button2.Enabled = false;

            toolStripMenuItem8.Checked = true;
            toolStripMenuItem10.Checked = true;
            toolStripMenuItem12.Checked = true;

            HideLabel();

            TimeSetEnd0 = new MyInvoke(SetOrder0);
            TimeSetEnd1 = new MyInvoke(SetOrder1);
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
            显示实时曲线ToolStripMenuItem.Enabled = true;
            //开启获取数据线程
            GetData = new Thread(GetFrameData);
            GetData.Priority = ThreadPriority.AboveNormal;
            GetData.Start();
            //设置异步采集方式
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
            toolStripMenuItem3.Enabled = true;
            button1.Enabled = true;
            //实时震动曲线相关的线程
            GetTimeData = new Thread(GetPointData);
            GetTimeData.Priority = ThreadPriority.AboveNormal;
            ReflushTimeData = new Thread(Real_Time_Data);
            ReflushTimeData.Priority = ThreadPriority.AboveNormal;
        }
        //更改实时点显示的方法
        public void Change_point(object sender, EventArgs e)
        {
            if (ServerForm.GetTimeData.ThreadState == System.Threading.ThreadState.Unstarted)
            {
                ServerForm.GetTimeData.Start();
            }
            if (ServerForm.ReflushTimeData.ThreadState == System.Threading.ThreadState.Unstarted)
            {
                ServerForm.ReflushTimeData.Start();
            }
            createPane2(zedGraphControl2);
            list2.Clear();
            myPane2.CurveList.Clear();
            try
            {
                zedGraphControl2.AxisChange();
                myCurve2.Clear();
            }
            catch { }

            ServerForm.getpointstop = 1;
            ServerForm.Real_Timestop = 1;

            if (button2.Text == "ON")
            {
                GraphPane myPane = zedGraphControl2.GraphPane;
                myPane.XAxis.Title.Text = "监测位置:" + (setrealpoint.GetPoint() * (ServerForm.scantlv / 2)).ToString() + "m处\r\n时间长度:"+uipointnum.ToString()+"ms";
            }
            //如果使能了实时点存储,则重启实时点存储线程
            if (RealPointRecordFlag == 1)
            {
                RealPointRecordFlag = 0;
                try
                {
                    while (RealPointRecord.ThreadState != System.Threading.ThreadState.Stopped) ;//当线程没有关闭时，等候
                }
                catch { }
                RealPointRecord = new Thread(RealPointRecordFunc);
                RealPointRecord.Priority = ThreadPriority.AboveNormal;
                RealPointRecord.Start();
                RealPointRecordFlag = 1;

            }
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
                while (tmp_start_sign != 1);
               // t.Restart();
                //设置交互的参数
                tmp_frame.Seq = reqSeq++;
                reqSeq %= 655;
                if (ScatteringRecordFlag == 1)
                {
                    //检测当前帧是否被使用，可以覆盖？否则阻塞
                    while (frame_pool[tmp_frame.Seq].user[0].user > 0) ;
                }
                //数组 托管内存->非托管内存
                Marshal.Copy(tmp_buffer, 0, tmp_frame.ptr, (int)buffernum);//将数组放到这个空间中

                //结构体 托管内存->非托管内存
                Marshal.StructureToPtr(tmp_frame, framePtr, false);//将结构体放到这个空间中

                //执行函数，获取数据帧
                while (importdll.Data_function(framePtr) != 0) ;//阻塞读取
                
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
                //这个是为了散射数据保存线程设置的参数
                if (ScatteringRecordFlag == 1)
                {
                    frame_pool[tmp_frame.Seq].user[0].thread_num = 0;
                    frame_pool[tmp_frame.Seq].user[0].user = 1;
                }
                //释放写者锁
                rw1.ExitWriteLock();
                importdll.WarningInfo wi = new importdll.WarningInfo();
                if (CanCollectionWarning == 1)
                {
                    //轮询报警信息
                    if (toolStripMenuItem5.Checked == true && importdll.Warning_function(ref wi) != -1)
                        //报警信息入队
                        msgque.Enqueue(wi);
                }
              //  t.Stop();
                //long time = t.ElapsedTicks;
                //double ti = (time / (fre + 1.0 - 1.0)) * 1000;
                //LogClass.WriteLogFile(ti.ToString() + "\r\n", "Get_One_Frame_Time_Ms.txt");
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
                if (tmp_point.point != setrealpoint.GetPoint())
                {
                    tmp_point.point = setrealpoint.GetPoint();//重新设置采集点
                    showpoint.buffer.SetValue(0, 0);
                }
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
            scantlv = samptvl = Convert.ToByte(toolStripComboBox2.SelectedItem);
            cf.class_code = 0x01;
            cf.command_code = 0x02;
            cf.data_code = (short)scantlv;
            importdll.Command_function(ref cf);
        }
        private void 数据查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe",Directory.GetCurrentDirectory()+"\\实时数据.txt");
        }
        private void 运行日志ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", Directory.GetCurrentDirectory() + "\\运行日志.txt");
        }
        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("帮助.pdf");
             //OpenFileDialog openFile=new OpenFileDialog();
             //openFile.Filter = "test|*.pdf";
             //openFile.ShowDialog();
           //  axAcroPDF1.src = openFile.FileName;
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {
            this.zedGraphControl1.GraphPane.Title.Text = "后向散射曲线";
        }

        private void zedGraphControl2_Load(object sender, EventArgs e)
        {
            this.zedGraphControl2.GraphPane.Title.Text = "实时震动曲线";
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
            Thread.Sleep(800);
            try
            {

                if (GetTimeData.IsAlive)
                {
                    GetTimeData.Abort();
                }
            }
            catch (Exception ex) { }
            try
            {

                if (ReflushTimeData.IsAlive)
                {
                    ReflushTimeData.Abort();
                }
            }
            catch (Exception ex) { }
            try
            {
                if (GetData.IsAlive)
                {
                    GetData.Abort();
                }
            }
            catch (Exception ex) { }
            //散射记录线程关闭
            if (ScatteringRecordFlag == 1)
            {
                ScatteringRecordFlag = 0;
            }
            //实时点记录线程关闭
            if (RealPointRecordFlag == 1)
            {
                RealPointRecordFlag = 0;
            }
            //丢帧率记录线程关闭
            if (LoseFrameRecordFlag == 1)
            {
                LoseFrameRecordFlag = 0;
            }
            散射数据预约写文件.Enabled = false;
            实时数据预约写文件.Enabled = false;
            ui_reflush.Enabled = false;
            Form.ActiveForm.Close();
        }

        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cf.class_code = 0x04;
            cf.command_code = 0x08;
            cf.data_code = 0x00;
            importdll.Command_function(ref cf);
        }
        private string GetZoomString(double location)
        {
           // location/=(scantlv/2);
            foreach(var b in SetZoom1.zoomInfo)
            {
                if(location>b.ZoomStartLoc &&location<b.ZoomEndLoc)
                {
                    return b.ZoomName;
                }
            }
            return "未命名区域";
        }
        private void ui_reflush_Tick(object sender, EventArgs e)
        {
            if (frame_pool[flushSeq].time_cost== 0)//帧尚未取
                goto Err;
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
                list1.Add(i*(scantlv/2.0), frame_pool[flushSeq].buffer[i]);
            }
            //释放读者锁
            rw1.ExitReadLock();
            if (button1.Text == "ON")//显示开关
            {
                // 用list1生产一条曲线，标注是""
                LineItem myCurve1 = myPane1.AddCurve("", list1, Color.Red, SymbolType.None);

                //画到zedGraphControl1控件中
                zedGraphControl1.AxisChange();

                //重绘控件
                Refresh();
            }
            else
            {
                list1.RemoveRange(0, list1.Count);
                myPane1.CurveList.Clear();
                // 用list1生产一条曲线，标注是""
                LineItem myCurve1 = myPane1.AddCurve("", list1, Color.Red, SymbolType.None);
                //画到zedGraphControl1控件中
                zedGraphControl1.AxisChange();
                //重绘控件
                Refresh();
            }
            //更新报警信息
            if (msgque.Count > 0)
            {
                dr = dt.NewRow();
                dr[0] = msgque.Peek().location;
                dr[1] = msgque.Peek().warningMax;
                dr[2] = msgque.Peek().warningExecRate;
                dr[3] = msgque.Peek().warningAckRate;
                //dr[4] = msgque.Peek().loseFramRate;
                dr[4] = GetZoomString(msgque.Peek().location);
                dt.Rows.Add(dr);
                //如果使能了写文件，报警信息写入
                if (LoseFrameRecordFlag == 1)
                {
                    string s = dr[0].ToString() + "      " + dr[1].ToString() + "      " + dr[2].ToString() + "      " + dr[3].ToString()
                        + "      " + msgque.Peek().loseFramRate.ToString() + "      " + dr[4].ToString() + "\r\n";
                    LogClass.WriteLogFile(s, "报警信息丢帧率.txt");
                }
                msgque.Dequeue();//删除一个元素
            }
            flushSeq++;
        Err:
            flushSeq %= 655;
         //   Thread.Sleep(500);//休眠500ms
        }

        private void 显示实时曲线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setrealpoint = new setRealPoint();
            setrealpoint.Show();
            button2.Enabled = true;
            setrealpoint.FormClosed += new FormClosedEventHandler(Change_point);
        }
        private void Real_Time_Data()
        {
            double time = 0;
            int i = showpoint.currentLoction;//记录指针
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
                if (button2.Text == "ON")
                {
                    // 用list2产生曲线，标注是""
                    myCurve2 = myPane2.AddCurve("", list2, Color.Red, SymbolType.None);
                    //画到zedGraphControl2控件中
                    zedGraphControl2.AxisChange();
                    //重绘控件
                    // Refresh();
                    zedGraphControl2.Invalidate();
                }
                else
                {
                    list2.Clear();
                    myPane2.CurveList.Clear();
                    // 用list2产生曲线，标注是""
                    myCurve2 = myPane2.AddCurve("", list2, Color.Red, SymbolType.None);
                    //画到zedGraphControl2控件中
                    zedGraphControl2.AxisChange();
                    //重绘控件
                    // Refresh();
                    zedGraphControl2.Invalidate();
                }
                t.Stop();
                frame_rate = (Single)(t.ElapsedTicks / (fre + 1.0 - 1.0)) * 1000;
                label2.Text = (frame_rate).ToString()+" ms";
            //    Thread.Sleep(100);
            }
        }

        private void toolStripComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_show_point(Convert.ToInt32(toolStripComboBox6.SelectedItem));
            if (button2.Text == "ON")
            {
                GraphPane myPane = zedGraphControl2.GraphPane;
                myPane.XAxis.Title.Text = "监测位置:" + (setrealpoint.GetPoint() * (ServerForm.scantlv / 2)).ToString() + "m处\r\n时间长度:" + uipointnum.ToString() + "ms";
            }
        }
        private void toolStripComboBox6_TextUpdate(object sender, EventArgs e)
        {
            set_show_point(Convert.ToInt32(toolStripComboBox6.Text));
            if (button2.Text == "ON")
            {
                GraphPane myPane = zedGraphControl2.GraphPane;
                myPane.XAxis.Title.Text = "监测位置:" + (setrealpoint.GetPoint() * (ServerForm.scantlv / 2)).ToString() + "m处\r\n时间长度:" + uipointnum.ToString() + "ms";
            }
        }
        //散射数据编码
        public void int2byte(int Seq, int[] intarray, out byte[] bytearray)
        {
            bytearray = new byte[intarray.Length*(sizeof(int)/sizeof(byte))+10];
            byte []tmparray=new byte[(sizeof(int)/sizeof(byte))];
            //前4字节和后3字节相同，都是88，中间4字节是Seq
            //中间4字节
            byte[] seqarray = BitConverter.GetBytes(Seq);
            bytearray[6] = seqarray[3];
            bytearray[5] = seqarray[2];
            bytearray[4] = seqarray[1];
            bytearray[3] = seqarray[0];
            //首尾6字节
            bytearray[0] = bytearray[1] = bytearray[2] = 88;
            bytearray[7] = bytearray[8] = bytearray[9] = 88;
            for (int i = 0; i < intarray.Length; i++)
            {
               tmparray = BitConverter.GetBytes(intarray[i]);
               for (int j = 0; j < tmparray.Length;j++ )
               {
                   bytearray[i * tmparray.Length + j + 10] = tmparray[j];
               }
            }
        }
        public void double2byte(int Seq, double[] doublearray, out byte[] bytearray)
         {
            //8个字节的帧序号标记
             bytearray = new byte[doublearray.Length * (sizeof(double) / sizeof(byte))+8];
            //前2字节和后2字节相同，都是88，中间4字节是Seq
            //中间4字节
             byte []seqarray = BitConverter.GetBytes(Seq);
             bytearray[5] = seqarray[3];
             bytearray[4] = seqarray[2];
             bytearray[3] = seqarray[1];
             bytearray[2] = seqarray[0];
            //首尾6字节
             bytearray[0] = bytearray[1] = 88;
             bytearray[6] = bytearray[7] = 88;
             byte[] tmparray = new byte[(sizeof(double) / sizeof(byte))];
             for (int i = 0; i < doublearray.Length; i++)
             {
                 tmparray = BitConverter.GetBytes(doublearray[i]);
                 for (int j = 0; j < tmparray.Length; j++)
                 {
                     bytearray[i * tmparray.Length + j + 8] = tmparray[j];
                 }
             }
         }
        //散射曲线记录
         public void ScatteringRecordFunc()
         {
             byte[] bytes;
             int Seq = 0;
             //文件设置
             string filePath = "./散射数据.dat";
             FileStream fs;
             if (File.Exists(filePath))
             {
                 fs = new FileStream(filePath, FileMode.Open);
             }
             else
             {
                 fs = new FileStream(filePath, FileMode.Create);
             }
             while (ScatteringRecordFlag == 1)
             {
                // t.Restart();
                 //找到可用帧,线程0,找不到则阻塞，或者退出线程
                 while (frame_pool[Seq].user[0].thread_num == 0
                        && frame_pool[Seq].user[0].user <= 0 )
                 {
                     if (ScatteringRecordFlag == 1)
                     {
                         //设置请求帧编号
                         Seq++;
                         Seq %= 655;
                     }
                     else
                     {
                         //退出线程
                         goto ThreadExit;
                     }
                 }
                 //将帧池数据转换为字节数组  
                 double2byte(Seq,frame_pool[Seq].buffer, out bytes);
                 //user自减
                 frame_pool[Seq].user[0].user--;
                 //设定书写的开始位置为文件的末尾  
                 fs.Position = fs.Length;
                 //开始写入
                 fs.Write(bytes, 0, bytes.Length);
                 //清空缓冲区、关闭流
                 fs.Flush();
                 //t.Stop();
                 //long time = t.ElapsedTicks;
                 //double ti = (time / (fre + 1.0 - 1.0)) * 1000;
               //  LogClass.WriteLogFile(ti.ToString() + "\r\n", "Save_One_Frame_Time_Ms.txt");
             ThreadExit:
                 ;
             }
             //写文件关闭，fs关闭
             try { fs.Close(); }
             catch { }

         }

        private void toolStripComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x0e;
            cf.data_code = Convert.ToDouble(toolStripComboBox7.SelectedItem);
            importdll.Command_function(ref cf);
        }

        private void toolStripComboBox7_TextUpdate(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x0e;
            cf.data_code = Convert.ToDouble(toolStripComboBox7.Text);
            importdll.Command_function(ref cf);
        }

        private void toolStripComboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x0f;
            cf.data_code = Convert.ToDouble(toolStripComboBox8.SelectedItem);
            importdll.Command_function(ref cf);
        }

        private void toolStripComboBox8_TextUpdate(object sender, EventArgs e)
        {
            cf.class_code = 0x02;
            cf.command_code = 0x0f;
            cf.data_code = Convert.ToDouble(toolStripComboBox8.Text);
            importdll.Command_function(ref cf);
        }

        private void 软件版本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            softversion sv = new softversion();
            sv.Visible = true;
            sv.label2.Text = SoftwareVersion;
            sv.label3.Text = CopyRight;
        }

        private void 分区报警设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sz = new SetZoom1();
            sz.Visible = true;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            CanCollectionWarning = 1;
            //开启报警算法
            toolStripMenuItem5.Checked = true;
            toolStripMenuItem6.Checked = false;
            cf.class_code = 0x04;
            cf.command_code = 0x14;
            cf.data_code = 0x01;
            importdll.Command_function(ref cf);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            CanCollectionWarning = 0;
            //关闭报警算法
            toolStripMenuItem5.Checked = false;
            toolStripMenuItem6.Checked = true;
            cf.class_code = 0x04;
            cf.command_code = 0x14;
            cf.data_code = 0x00;
            importdll.Command_function(ref cf);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripMenuItem1.Checked = true;
            toolStripMenuItem4.Checked = false;
            cf.class_code = 0x04;
            cf.command_code = 0x13;
            cf.data_code = 0x01;
            importdll.Command_function(ref cf);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            toolStripMenuItem4.Checked = true;
            toolStripMenuItem1.Checked = false;
            cf.class_code = 0x04;
            cf.command_code = 0x13;
            cf.data_code = 0x00;
            importdll.Command_function(ref cf);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (toolStripMenuItem3.Text == "暂停")
            {
                toolStripMenuItem3.Text = "继续";
                try
                {
                    if (GetData.IsAlive)
                        GetData.Suspend();
                    if (ReflushTimeData.IsAlive)
                        ReflushTimeData.Suspend();
                }
                catch (Exception ex)
                {
                    ;
                }
                ui_reflush.Enabled = false;
            }
            else
            {
                toolStripMenuItem3.Text = "暂停";
                try
                {
                    GetData.Resume();
                    ReflushTimeData.Resume();
                }
                catch (Exception ex)
                {
                    ;
                }
                ui_reflush.Enabled = true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //后向散射曲线开关
            if (button1.Text == "ON")
            {
                button1.Text = "OFF";
                myPane1.CurveList.Clear();
            }
            else
            {
                button1.Text = "ON";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //实时震动曲线开关
            if (button2.Text == "ON")
            {
                button2.Text = "OFF";
                myPane2.CurveList.Clear();
                GraphPane myPane = zedGraphControl2.GraphPane;
                myPane.XAxis.Title.Text = "监测位置: 0m处\r\n时间单位:ms";
            }
            else
            {
                button2.Text = "ON";
                 GraphPane myPane = zedGraphControl2.GraphPane;
                 myPane.XAxis.Title.Text = "监测位置:" + (setrealpoint.GetPoint() * (ServerForm.scantlv / 2)).ToString() + "m处\r\n时间长度:" + uipointnum.ToString() + "ms";
            }
        }

        private Size beforeResizeSize = Size.Empty;

        protected override void OnResizeBegin(EventArgs e)
        {
            base.OnResizeBegin(e);
            beforeResizeSize = this.Size;
        }
        protected override void OnResizeEnd(EventArgs e)
        {
      base.OnResizeEnd(e);
      //窗口resize之后的大小
      Size endResizeSize =this.Size;
      //获得变化比例
      float percentWidth = (float)endResizeSize.Width / beforeResizeSize.Width;
      float percentHeight = (float)endResizeSize.Height / beforeResizeSize.Height;
      foreach (Control control in this.Controls)
      {
            if (control is DataGridView)
                 continue;
            //按比例改变控件大小
            control.Width = (int)(control.Width * percentWidth);
            control.Height = (int)(control.Height * percentHeight);
            //为了不使控件之间覆盖 位置也要按比例变化
            control.Left = (int)(control.Left * percentWidth);
            control.Top = (int)(control.Top * percentHeight);
     }
}

        private void ServerForm_Resize(object sender, EventArgs e)
        {
            panel1.Width = Convert.ToInt32(this.Size.Width * 0.49);
            panel2.Width = Convert.ToInt32(this.Size.Width * 0.49);
          //  panel1.Height = Convert.ToInt32(this.Size.Height * 0.5);
          //  panel2.Height = Convert.ToInt32(this.Size.Height * 0.5);
            panel1.Location = new Point(12,53);
            panel2.Location = new Point(Convert.ToInt32(this.Size.Width * 0.50), 53);
            label7.Location = new Point(Convert.ToInt32(this.Size.Width * 0.42), 25);
           // dataGridView1.Location = new Point(0, Convert.ToInt32(this.Size.Height * 0.5));
           // lb.Location = new Point(100, 100);//3
          //  zedGraphControl1.Width = this.Size.Width/2;
            //zedGraphControl1.Height = Convert.ToInt32(this.Size.Height*0.6);
          //  zedGraphControl2.Width = this.Size.Width / 2;
            //zedGraphControl2.Height = Convert.ToInt32(this.Size.Height * 0.6);
            //MessageBox.Show("Height" + this.Size.Height.ToString() + "\r\nWidth" + this.Size.Width.ToString());
        }

        private void ServerForm_ResizeEnd(object sender, EventArgs e)
        {
         
        }

        private void 实时震动曲线记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            //散射记录开
            ScatteringRecord = new Thread(ScatteringRecordFunc);
            ScatteringRecord.Priority = ThreadPriority.AboveNormal;
            ScatteringRecord.Start();
            ScatteringRecordFlag = 1;
            toolStripMenuItem7.Checked = true;
            toolStripMenuItem8.Checked = false;
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            ScatteringRecordFlag = 0;
            toolStripMenuItem7.Checked = false;
            toolStripMenuItem8.Checked = true;
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            //实时记录开
            RealPointRecord = new Thread(RealPointRecordFunc);
            RealPointRecord.Priority = ThreadPriority.AboveNormal;
            RealPointRecord.Start();
            RealPointRecordFlag = 1;
            toolStripMenuItem9.Checked = true;
            toolStripMenuItem10.Checked = false;
        }
        //记录实时点数据的线程函数
        public void RealPointRecordFunc()
        {
            int i = showpoint.currentLoction;//记录指针
            byte[] bytes;
            //文件设置
            string filePath = "./"+setrealpoint.GetPoint().ToString()+"点数据.dat";
            FileStream fs;
            if (File.Exists(filePath))
            {
                fs = new FileStream(filePath, FileMode.Open);
            }
            else
            {
                fs = new FileStream(filePath, FileMode.Create);
            }
            while (RealPointRecordFlag == 1)
            {
                //检索showpoint内的新添加帧
                for (; i != showpoint.currentLoction; i++, i %= uipointnum)
                {
                    //将数据转换为字节数组  
                    bytes = BitConverter.GetBytes(showpoint.buffer[i]);
                    // bytes = BitConverter.GetBytes(i*0.01);
                    //设定书写的开始位置为文件的末尾  
                    fs.Position = fs.Length;
                    //开始写入
                    fs.Write(bytes, 0, bytes.Length);
                }
                //清空缓冲区
                fs.Flush();
            }
            try { fs.Close(); }
            catch{}
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            RealPointRecordFlag = 0;
            toolStripMenuItem9.Checked = false;
            toolStripMenuItem10.Checked = true;
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            //丢帧率记录开
            //LoseFrameRecord = new Thread(LoseFrameRecordFunc);
            //LoseFrameRecord.Priority = ThreadPriority.AboveNormal;
            //LoseFrameRecord.Start();
            LoseFrameRecordFlag = 1;
            toolStripMenuItem11.Checked = true;
            toolStripMenuItem12.Checked = false;
            string s="报警位置      报警最大值       报警异常比       报警确认比       丢帧率     报警区间\r\n";
            LogClass.WriteLogFile(s,"报警信息丢帧率.txt");
        }
        public void LoseFrameRecordFunc()
        {
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            LoseFrameRecordFlag = 0;
            toolStripMenuItem11.Checked = false;
            toolStripMenuItem12.Checked = true;
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            ot0 = new OrderTime("散射采集");
            ot0.Visible = true;
        }
        public void SetOrder0()
        {
            //此处完成预约散射采集的注册
            StartTime散射 = ot0.RetStartTime();
            StopTime散射 = ot0.RetStopTime();
            ot0.Close();
            散射数据预约写文件.Enabled = true;
        }
        public void SetOrder1()
        {
            //此处完成预约实时采集的注册
            StartTime实时 = ot1.RetStartTime();
            StopTime实时 = ot1.RetStopTime();
            try
            {
                MessageBox.Show("本次预约采集点是:" + setrealpoint.GetPoint().ToString());
                实时数据预约写文件.Enabled = true;
            }
            catch {
                MessageBox.Show("请先在'开始-实时曲线'中设置采集点!");
            }
            ot1.Close();
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            ot1 = new OrderTime("实时采集");
            ot1.Visible = true;
            //设置完成后，开启预约任务
        }

        private void 散射数据预约写文件_Tick(object sender, EventArgs e)
        {
            string timenow = DateTime.Now.ToString();
            DateTime dt0,dt1,dt2;
            DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy/MM/dd HH:mm:ss";
            dt0 = Convert.ToDateTime(StartTime散射, dtFormat);
            dt1=Convert.ToDateTime(timenow, dtFormat);
            dt2 = Convert.ToDateTime(StopTime散射, dtFormat);
            if (DateTime.Compare(dt1, dt0) >= 0
                && DateTime.Compare(dt1, dt2) < 0)
            {
                //正在采集？
                if (ScatteringRecordFlag == 1)
                {

                }
                else
                {
                    //没在采集，现在打开
                    if (ScatteringRecord == null)
                    {
                        ScatteringRecord = new Thread(ScatteringRecordFunc);
                        ScatteringRecord.Priority = ThreadPriority.AboveNormal;
                    }
                    if (ScatteringRecord.ThreadState == System.Threading.ThreadState.Unstarted
                        || ScatteringRecord.ThreadState == System.Threading.ThreadState.Stopped)
                    {
                        ScatteringRecord.Start();
                        ScatteringRecordFlag = 1;
                        //设置开关
                        toolStripMenuItem7.Checked = true;
                        toolStripMenuItem8.Checked = false;
                    }
                }
            }
             //关闭采集
            else if (DateTime.Compare(dt1, dt2)>= 0)
            {
                ScatteringRecordFlag = 0;
                散射数据预约写文件.Enabled = false;
                //设置开关
                toolStripMenuItem7.Checked = false;
                toolStripMenuItem8.Checked = true;
            }
        }

        private void 实时数据预约写文件_Tick(object sender, EventArgs e)
        {
            string timenow = DateTime.Now.ToString();
            DateTime dt0, dt1, dt2;
            DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy/MM/dd HH:mm:ss";
            dt0 = Convert.ToDateTime(StartTime实时, dtFormat);
            dt1 = Convert.ToDateTime(timenow, dtFormat);
            dt2 = Convert.ToDateTime(StopTime实时, dtFormat);
            if (DateTime.Compare(dt1, dt0) >= 0
                && DateTime.Compare(dt1, dt2) < 0)
            {
                //正在采集？
                if (RealPointRecordFlag == 1)
                {

                }
                else
                {
                    //没在采集，现在打开
                    if (RealPointRecord == null)
                    {
                        RealPointRecord = new Thread(RealPointRecordFunc);
                        RealPointRecord.Priority = ThreadPriority.AboveNormal;
                    }
                    if (RealPointRecord.ThreadState == System.Threading.ThreadState.Unstarted
                        || RealPointRecord.ThreadState == System.Threading.ThreadState.Stopped)
                    {
                        RealPointRecord.Start();
                        RealPointRecordFlag = 1;
                        //设置开关
                        toolStripMenuItem9.Checked = true;
                        toolStripMenuItem10.Checked = false;
                    }
                }
            }
            //关闭采集
            else if (DateTime.Compare(dt1, dt2) >= 0)
            {
                RealPointRecordFlag = 0;
                实时数据预约写文件.Enabled = false;
                //设置开关
                toolStripMenuItem9.Checked = false;
                toolStripMenuItem10.Checked = true;
            }
        }

        private void 格式转换工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("FormatConvert.exe");
            }
            catch { MessageBox.Show("打开失败，请检查软件文件是否完整！"); }
        }

        private void toolStripComboBox2_TextChanged(object sender, EventArgs e)
        {
            scantlv = samptvl = Convert.ToByte(toolStripComboBox2.Text);
            cf.class_code = 0x01;
            cf.command_code = 0x02;
            cf.data_code = (short)scantlv;
            importdll.Command_function(ref cf);
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            //关闭报警信息采集
          //  CanCollectionWarning = 0;
            //清空报警队列
            importdll.WarningClear_function();
            //清空缓存信息
            msgque.Clear();
            //清除页面信息
            dt.Clear();
            dataGridView1.Update();
        }


    }
}
