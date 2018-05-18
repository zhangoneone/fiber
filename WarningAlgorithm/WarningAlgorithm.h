// 下列 ifdef 块是创建使从 DLL 导出更简单的
// 宏的标准方法。此 DLL 中的所有文件都是用命令行上定义的 WARNINGALGORITHM_EXPORTS
// 符号编译的。在使用此 DLL 的
// 任何其他项目上不应定义此符号。这样，源文件中包含此文件的任何其他项目都会将
// WARNINGALGORITHM_API 函数视为是从 DLL 导入的，而此 DLL 则将用此宏定义的
// 符号视为是被导出的。
#ifndef WARNING_H
#define WARNING_H
#include<PCIECollection.h>
#include<Windows.h>
#ifdef WARNINGALGORITHM_EXPORTS
#define WARNINGALGORITHM_API __declspec(dllexport)
#else
#define WARNINGALGORITHM_API __declspec(dllimport)
#endif

#define DEBUG 1
#define QueLen 4000
#define Max_Num 655

typedef struct{
	int pointNum;//点编号
	double MaxValue;//该点最大值
	int excep;//异常次数,针对每一个采集点
	int nexcep;//正常次数
	double excepRate;//异常率
	int ack;//报警确认次数
	int nack;//报警否认次数
	int ackRate;//报警确认率
	short warning;//该点是否报警 1 报警 0 不报警

	int frame_Num;//总帧数
	int loseFrame;//丢帧数
	double loseFrameRate;//丢帧率
}PointInfo;

typedef struct
{
	int Que_Max;//队列长度
	PointInfo *p;//队列元素类型,和长度
	int Rpt,Wpt;//队列的读写指针
//	Queue *next;//链接域
}Queue;
class WarnMessageQue{//报警消息队列
public:
	Queue *que;
	WarnMessageQue()
	{
		que = new Queue();
		que->Que_Max=QueLen;
		que->p=new PointInfo[que->Que_Max];
		que->Rpt=que->Wpt=0;
	}
	int Add(PointInfo p);
	WARNINGALGORITHM_API PointInfo Get();
	WARNINGALGORITHM_API int Full();
	WARNINGALGORITHM_API int Empty();
private:
	int CleanQue();
};
// 此类是从 WarningAlgorithm.dll 导出的。去掉了导出标志WARNINGALGORITHM_API
class CWarningAlgorithm {
public:
	WARNINGALGORITHM_API static CWarningAlgorithm * GetSingleton();//获取单例
	Frame *frame; 
	Frame_Pool *fp;//帧池
	DataCollection *dc;//PCIE采集类的实例
	double setthreshold;//异常的阈值
	double setexcepRate;//预报警的异常阈值
	double setackRate;//报警的预报警阈值
	WARNINGALGORITHM_API int Start_GetAndHandle();//获取、处理数据
	WarnMessageQue *wq;//报警消息队列
	void GetData();//调用者函数
	volatile void HandleData();//此为处理函数
	WARNINGALGORITHM_API int add_user(int thread_num,int user_num);
	WARNINGALGORITHM_API int remove_user(int thread_num,int user_num);
	WARNINGALGORITHM_API void block_write_lock(int seq);
	WARNINGALGORITHM_API void block_read_lock(int seq,int thread_location);
	WARNINGALGORITHM_API void inline force_write(int seq);
	WARNINGALGORITHM_API void force_read(int seq,int thread_num);
	WARNINGALGORITHM_API int Set_threshold(double x);
	WARNINGALGORITHM_API int Set_excepRate(double x);
	WARNINGALGORITHM_API int Set_ackRate(double x);
	//缓冲池读写线程锁
	CRITICAL_SECTION bp_cs;
	SRWLOCK          bp_srwLock; 

	static HANDLE wThread;//获取数据线程，句柄
	int wThread_Num;
	int wThred_location;
	int wThread_stop;
	static HANDLE hThread;//处理数据线程，句柄
	int hThread_Num;
	int hThred_location;
	int hThread_stop;
private:
	static CWarningAlgorithm *m_singleton;
	CWarningAlgorithm(void);
};
CWarningAlgorithm *CWarningAlgorithm::m_singleton = NULL;
HANDLE CWarningAlgorithm::wThread = NULL;//线程，单例模式
HANDLE CWarningAlgorithm::hThread = NULL;//线程，单例模式
extern WARNINGALGORITHM_API int nWarningAlgorithm;

WARNINGALGORITHM_API int fnWarningAlgorithm(void);
#endif
