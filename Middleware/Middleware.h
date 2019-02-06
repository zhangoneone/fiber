// 下列 ifdef 块是创建使从 DLL 导出更简单的
// 宏的标准方法。此 DLL 中的所有文件都是用命令行上定义的 MIDDLEWARE_EXPORTS
// 符号编译的。在使用此 DLL 的
// 任何其他项目上不应定义此符号。这样，源文件中包含此文件的任何其他项目都会将
// MIDDLEWARE_API 函数视为是从 DLL 导入的，而此 DLL 则将用此宏定义的
// 符号视为是被导出的。
#include<PCIECollection.h>
#include<WarningAlgorithm.h>
#ifndef MIDDLE_H
#define MIDDLE_H
#ifdef MIDDLEWARE_EXPORTS
#define MIDDLEWARE_API __declspec(dllexport)
#else
#define MIDDLEWARE_API __declspec(dllimport)
#endif

#define DEBUG 1

#define buffnum 5000
typedef enum{
	Start=0x00,
	Config=0x01,
	ParamSet=0x02,
	CollectionMode=0x03,
	AmendMode=0x04,
	Refer=0x05,
	About=0x06
}Class_Code;
typedef enum{
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
	Set_K1=0x0e,
	Set_K2=0x0f,
	Set_threshold=0x10,
	Set_excepRate=0x11,
	Set_ackRate=0x12,
	Set_Expand_OnOff=0x13,
	Set_Warning_OnOff=0x14
}Command_Code;

typedef struct{
	short class_code;
	short command_code;
	double data_code;   //可选的附加数据
}Command;//命令码

typedef struct{
	double location;//报警位置
	double warningMax;//报警最大值
	double warningExecRate;//报警异常比
	double warningAckRate;//报警确认比
	double loseFramRate;//丢帧率
}WarningInfo;
// 此类是从 Middleware.dll 导出的
class MIDDLEWARE_API CMiddleware {
public:
	//防止代码重入
	int singlenter_point;
	int singlenter_frame;
	//读取指针++
	int point_ptr;
	int frame_ptr;
	DataCollection *dc;//PCIE采集类的实例
	CWarningAlgorithm *wa;//CWarningAlgorithm类的实例
	static CMiddleware *GetSingleton();//单例模式
	CMiddleware(void);
	DataCollection * CallPCIECollectionDLL();//调用PCIECollection.DLL
//	int CallDataHandleDll();//调用DataHandle.DLL
	CWarningAlgorithm * CallWarningAlgorithmDLL();//调用WarningAlgorithm.DLL
	int Start(Command *c);
	int Config(Command *c);
	int ParamSet(Command *c);
	int CollectionMode(Command *c);
	int AmendMode(Command *c);
	int Refer(Command *c);
	int About(Command *c);
private:
	static CMiddleware * m_singleton ;
};
CMiddleware *CMiddleware::m_singleton=NULL;

extern MIDDLEWARE_API int nMiddleware;

MIDDLEWARE_API int fnMiddleware(void);

extern "C" MIDDLEWARE_API int Command_function(Command *c);
extern "C" MIDDLEWARE_API int Data_function(Frame *d);
extern "C" MIDDLEWARE_API int Warning_function(WarningInfo *w);
extern "C" MIDDLEWARE_API int Point_function(Point *p);
extern "C" MIDDLEWARE_API int ZoomSet_function(ZoomParam *zp,int count);
extern "C" MIDDLEWARE_API int ZoomClear_function();
extern "C" MIDDLEWARE_API int WarningClear_function();
#endif
