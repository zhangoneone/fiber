//导入环境配置、链接库里的驱动函数
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace FiberopticServer
{
    static class importdll
    {
         enum Class_Code{
	    Start=0x00,
	    Config=0x01,
	    ParamSet=0x02,
	    CollectionMode=0x03,
	    AmendMode=0x04,
	    Refer=0x05,
	    About=0x06
        };
        enum Command_Code{
	    Register_Card=0x00,
	    Logout_Card=0x01,
	    Scan_Interval=0x02,
	    Samp_Interval=0x03,
	    Collection_Num=0x04,
	    One_Frame_Data_Tofile=0x05,
	    One_Point_Data_Tofile=0x06,
	    Asyn_Collection=0x07,
	    Exp_Expand=0x08,
	    One_Frame_Data_Refer=0x09,
	    One_Point_Data_Refer=0x0a,
	    Log_Refer=0x0b,
	    Help_Doc=0x0c,
	    Software_Version=0x0d,
        Set_K1 = 0x0e,
        Set_K2 = 0x0f,
        Set_threshold = 0x10,
        Set_excepRate = 0x11,
        Set_ackRate = 0x12
        };

        public struct Command{
	    public Int16 class_code;
        public Int16 command_code;
        public double data_code;   //可选的附加数据
        };//命令码
        public struct Frame{
            public Int32 Seq;//帧序号
            public double time_cost;//采集此帧耗费时间，单位ms
            public IntPtr ptr;
            public double[] buffer;
         };//帧
        public struct Point{
            public Int32 StartSeq;//帧起始序号
            public Int32 StopSeq;//帧结束序号
            public Int32 point;//选定点
            public IntPtr ptr;
            public double[] buffer;
            };//点的时移数据
        public struct ShowPoint
        {
            public Int32 pointLength;//点总长
            public Int32 currentLoction;//点尾指针
            public double[]buffer;//点载荷
        }//绘制点
        public struct WarningInfo
        {
	        public  double location;//报警位置
            public	double warningMax;//报警最大值
            public	double warningExecRate;//报警异常比
            public	double warningAckRate;//报警确认比
            public  double loseFramRate;//丢帧率
        };
        [DllImport("Middleware.dll", CharSet = System.Runtime.InteropServices.CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Command_function(ref Command c);
        [DllImport("Middleware.dll", CharSet = System.Runtime.InteropServices.CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Data_function(IntPtr ptr);
        [DllImport("Middleware.dll", CharSet = System.Runtime.InteropServices.CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Point_function(IntPtr ptr);
        [DllImport("Middleware.dll", CharSet = System.Runtime.InteropServices.CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Warning_function(ref WarningInfo w);
    }
}
