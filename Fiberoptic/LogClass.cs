using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiberopticServer.common
{
    //LogClass类完成文件的读写与数据存储
    class LogClass
    {
        public static void WriteInforLog(string log,int i)
        {
            /**/
            ///指定日志文件的目录
            ///
            
            string fname = Directory.GetCurrentDirectory() + "\\LogInfor"+ i+" .txt";
            
          //  Directory.CreateDirectory(newfname);

            WriteLogFile(log, fname);
        }
        public static void WriteInforLog(string log, string s)
        {
            /**/
            ///指定日志文件的目录
            ///

            string fname = Directory.GetCurrentDirectory() + "\\" + s + " .txt";

            //  Directory.CreateDirectory(newfname);

            WriteLogFiletime(log, fname);
        }
        public static void WriteInforLognotime(string log, string s)
        {
            /**/
            ///指定日志文件的目录
            ///

            string fname = Directory.GetCurrentDirectory() + "\\" + s + ".txt";

            //  Directory.CreateDirectory(newfname);

            WriteLogFile(log, fname);
        }
        public static void WriteErrorLog(string log)
        {
            /**/
            ///指定日志文件的目录
            string fname = Directory.GetCurrentDirectory() + "\\运行日志.txt";
           

            WriteLogFiletime(log, fname);
        }
       
         /**//// <summary>
         /// 写入日志文件
         /// </summary>
         /// <param name="input"></param>
         public static void WriteLogFile(string input,string fname)
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
                    /**/
                    ///根据上面创建的文件流创建写数据流
                    StreamWriter w = new StreamWriter(fs);

                    /**/
                    ///设置写数据流的起始位置为文件流的末尾
                    w.BaseStream.Seek(0, SeekOrigin.End);

                    /**/
                    ///写入“Log Entry : ”
                    // w.Write("\n\rLog Entry : ");

                    /**/
                    ///写入当前系统时间并换行
                    //               w.Write("{0} \n\r", DateTime.Now.ToString("HH:mm:ss.fff"));

                    /**/
                    ///写入日志内容并换行
                    w.Write(input + "\n\r");
                    w.Write("\n\r");
                    /**/
                    ///写入----------------------“并换行


                    /**/
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
         private static void WriteLogFiletime(string input, string fname)
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


             using (FileStream fs = finfo.OpenWrite())
             {
                 /**/
                 ///根据上面创建的文件流创建写数据流
                 StreamWriter w = new StreamWriter(fs);

                 /**/
                 ///设置写数据流的起始位置为文件流的末尾
                 w.BaseStream.Seek(0, SeekOrigin.End);

                 /**/
                 ///写入当前系统时间并换行
                 w.Write("{0} \n\r", DateTime.Now.ToString("HH:mm:ss.fff"));

                 /**/
                 ///写入日志内容并换行
                 w.Write(input + "\n\r");
                 w.Write("\n\r");
                 /**/
                 ///清空缓冲区内容，并把缓冲区内容写入基础流
                 w.Flush();

                 /**/
                 ///关闭写数据流
                 w.Close();
             }

         }

         public static void scatterWrite(string filename, byte[] data)
         {
             //   FileStream.Write
             string filePath = Directory.GetCurrentDirectory() + filename + ".txt";
             if (File.Exists(filePath))
                 File.Delete(filePath);
             FileStream fs = new FileStream(filePath, FileMode.Create);
             //开始写入
             fs.Write(data, 0, data.Length);
             //清空缓冲区、关闭流
             fs.Flush();
             fs.Close();
         }
    }
}
