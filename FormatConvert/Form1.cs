using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FormatConvert
{
    public partial class Form1 : Form
    {
        string ConvertType = "";
        string fileName = "";
        Stream stm;//读文件流
        FileStream fs;//写文件流
        public Form1()
        {
            InitializeComponent();
        }

        public void WriteFile(string input, string fname)
        {

            /**/
            ///定义文件信息对象

            FileInfo finfo = new FileInfo(fname);

            if (!finfo.Exists)
            {
                FileStream fs;
                fs = File.Create(fname);
                fs.Close();
                finfo = new FileInfo(fname);
            }
            try
            {
                using (FileStream fs = finfo.OpenWrite())
                {
                    //根据上面创建的文件流创建写数据流
                    StreamWriter w = new StreamWriter(fs);
                    //设置写数据流的起始位置为文件流的末尾
                    w.BaseStream.Seek(0, SeekOrigin.End);
                    //写入日志内容并换行
                    w.Write(input + "\r\n");
                    ///清空缓冲区内容，并把缓冲区内容写入基础流
                    w.Flush();

                    /**/
                    ///关闭写数据流
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                return;
            }

        }


        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            fileName = comboBox1.Text = openFileDialog1.SafeFileName;
            stm = openFileDialog1.OpenFile();
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConvertType=comboBox2.SelectedItem.ToString();
        }
        public void Convert2Scatter()
        {
            //设置待写入的文件
            byte[] bytes = new byte[8];
            int length = 1024*1024;//一次多处理一些,8*1K*1K,8M
            double[] value = new double[length];
            byte [] tmp=new byte[4];//凑回帧序号
            int j = 0;
            string filePath = "./" + fileName + ".conv";
            string fname = fileName + ".conv";
            /**/
            ///定义文件信息对象
            FileInfo finfo = new FileInfo(fname);
            FileStream fs;
            if (!finfo.Exists)
            {
                fs = File.Create(fname);
                fs.Close();
                finfo = new FileInfo(fname);
            }
            fs = finfo.OpenWrite();
            //根据上面创建的文件流创建写数据流
            StreamWriter w = new StreamWriter(fs);
            //读数据
            for (int i = 0; i < stm.Length; i += 8)//每次读8字节
            {
                //设置文件的读取位置
                stm.Position = i;
                stm.Read(bytes, 0, 8);
                //判定帧头
                if (bytes[0] == 88 && bytes[1] == 88 && bytes[6] == 88 && bytes[7] == 88)
                {
                    bytes[0] = bytes[1] = bytes[6] = bytes[7] = 0;
                    //写入换行回车
                    value[j] = 0x0a;
                    j++;
                    value[j] = 0x0d;
                    j++;
                    //写入帧序号
                    tmp[0] = bytes[2];
                    tmp[1] = bytes[3];
                    tmp[2] = bytes[4];
                    tmp[3] = bytes[5];
                    value[j] = BitConverter.ToInt32(tmp, 0);//凑回int型
                    j++;
                    //写入换行回车
                    value[j] = 0x0a;
                    j++;
                    value[j] = 0x0d;
                    j++;
                }
                else
                {
                    value[j] = BitConverter.ToDouble(bytes, 0);//凑回double型
                    j++;
                }
                if (j == length)//存满了一个double数组了，写回文件
                {
                    j %= length;
                    for (int k = 0; k < length; k++)
                    {
                        //设置写数据流的起始位置为文件流的末尾
                        w.BaseStream.Seek(0, SeekOrigin.End);
                        //写入日志内容并换行
                        w.Write(value[k].ToString() + "\r\n");
                     // WriteFile(value[k].ToString(), fileName + ".conv");
                    }
                }
            }
            //不足一个double数组的，也要写入文件
            for (int k = 0; k < j; k++)
            {
                //设置写数据流的起始位置为文件流的末尾
                w.BaseStream.Seek(0, SeekOrigin.End);
                //写入日志内容并换行
                w.Write(value[k].ToString() + "\r\n");
            //   WriteFile(value[k].ToString(), fileName + ".conv");
            }
            ///清空缓冲区内容，并把缓冲区内容写入基础流
            w.Flush();
            ///关闭写数据流
            w.Close();
            MessageBox.Show("后向散射解析完成");
        }
        public void Convert2Real()
        {
            //设置待写入的文件
            byte[] bytes=new byte[8];
            int length=1024*1024;
            double[] value = new double[length];
            int j = 0;
            string filePath = "./" + fileName + ".conv";
            string fname = fileName + ".conv";
            /**/
            ///定义文件信息对象
            FileInfo finfo = new FileInfo(fname);
            FileStream fs;
            if (!finfo.Exists)
            {
                fs = File.Create(fname);
                fs.Close();
                finfo = new FileInfo(fname);
            }
            fs = finfo.OpenWrite();
            //根据上面创建的文件流创建写数据流
            StreamWriter w = new StreamWriter(fs);
            for (int i = 0; i < stm.Length; i+=8)//每次读8字节
            {
                //设置文件的读取位置
                stm.Position = i;
                stm.Read(bytes, 0, 8);
                value[j] = BitConverter.ToDouble(bytes, 0);//凑回double型
                j++;
                if (j == length)//存满了一个double数组了，写回文件
                {
                    j %= length;
                    for (int k = 0; k < length; k++)
                    {
                        //设置写数据流的起始位置为文件流的末尾
                        w.BaseStream.Seek(0, SeekOrigin.End);
                        //写入日志内容并换行
                        w.Write(value[k].ToString() + "\r\n");
                      //  WriteFile(value[k].ToString(), fileName + ".conv");
                    }
                }
            }
            //不足一个double数组的，也要写入文件
            for (int k = 0; k < j; k++)
            {
                //设置写数据流的起始位置为文件流的末尾
                w.BaseStream.Seek(0, SeekOrigin.End);
                //写入日志内容并换行
                w.Write(value[k].ToString() + "\r\n");
               // WriteFile(value[k].ToString(), fileName + ".conv");
            }
            ///清空缓冲区内容，并把缓冲区内容写入基础流
            w.Flush();
            ///关闭写数据流
            w.Close();
            MessageBox.Show("实时震动数据解析完成");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.SafeFileName == "")
            {
                MessageBox.Show("请先选择文件！");
                goto errorHandle;
            }
            if (ConvertType == "")
            {
                MessageBox.Show("请先选择转换类型！");
                goto errorHandle;
            }
            switch (ConvertType)
            {
                case "后向散射转换": Convert2Scatter(); break;
                case "实时震动转换": Convert2Real(); break;
                default: break;

            }
        errorHandle: ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Reset();
            }
            catch { }
            try
            {
                comboBox1.Text = "";
            }
            catch { }
            try
            {
                comboBox2.SelectedItem = null;
            }
            catch { }
            try
            {
                stm.Close();
            }
            catch { }
        }
    }
}
