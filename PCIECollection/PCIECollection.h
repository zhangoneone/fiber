#ifndef PCIE_COLLECTION
#define PCIE_COLLECTION
#include "stdafx.h"
#include "Wd-dask.h"
#include <windows.h>
#include<process.h>
#ifndef MYDLL_EXPORTS
#define MYDLL_API __declspec(dllexport)
#else
#define MYDLL_API __declspec(dllimport)
#endif

#define DEBUG 1

typedef enum{
	Config_OK=0x00,
	Register_card_err=0x01,
	AI_CH_Config=0x02,
	AI_Config=0x03,
	AI_Trig_Config=0x04,
	AI_ContBufferSetup=0x05,
	Save_tofile_failed=0x06
}Config_Err_Mask;//0x00表示正常返回

#define Max_Num    655
#define Zero_user  0
#define One_user   1
#define Two_user   2

#define Max_Thread 10
typedef struct
{
	int thread_num;
	int user;
}user_manage;

typedef struct
{
	int Seq;//帧序号
	user_manage *user;//user
	double time_cost;//帧耗费的时间
	double *buffer_load;//帧载荷
}Frame;//帧信息

typedef struct{
	int StartSeq;//帧起始序号
	int StopSeq;//帧结束序号
    int point;//选定点
	double *buffer_load;//点载荷
}Point;//点的时移数据

MYDLL_API Frame;
MYDLL_API Point;
//PCIE模块的帧池，只存放必需的数据和数据参数。
//要填充的项
//Seq
//time_cost_array
//container
//buffer_load

typedef struct
{
	int buffer_Current_Num;//当前帧指针
	int ref_thread;
	user_manage *Current_User;//帧user
	Frame frame[Max_Num];//帧数
}Frame_Pool;
MYDLL_API Frame_Pool;//导出Frame_Pool

class Timer
{
public:
	LARGE_INTEGER *litmp;
	Timer(){
		litmp=new LARGE_INTEGER();
		QueryPerformanceFrequency(litmp);//获得时钟频率
		dff=(double)litmp->QuadPart;
		start_counter = end_counter = dft = dfm = 0;
	}
	~Timer(){delete litmp;}
	void timer_start(){
		QueryPerformanceCounter(litmp);//获得初始值
		start_counter=litmp->QuadPart;
	}
	void timer_end(){
		QueryPerformanceCounter(litmp);//获得终止值
		end_counter=litmp->QuadPart;
	}
	double timer_ms(){
		dfm=(double)(end_counter-start_counter)*1000;//转换成ms
		dft=dfm/dff;//获得对应的时间值
		return dft;
	}
private:
	LONGLONG start_counter,end_counter;//开始计数、结束计数
	double dft;//时间差，单位ms
	double dff;//系统硬件频率
	double dfm;//计数差
};//计时器
typedef void(*callback)(void);//声明回调函数指针
//DataCollection类,导出此类及其变量方法，给其他c++模块使用
class MYDLL_API DataCollection{
public:	
	Frame_Pool *fp;//帧池
	Timer *timer;//计时器
	static HANDLE cThread;//采集线程句柄
	int  cThread_stop;//结束标记
	static HANDLE eThread;//上传线程句柄

	//缓冲池读写线程锁
	CRITICAL_SECTION bp_cs;
	SRWLOCK          bp_srwLock; 

	int handlebug;//解决第一帧bug设置的变量
	//采集参数
	double K1;
	double K2;//保密放大参数
	double d;//距离
	U32 scantlv;//扫描间隔
	U32 samptvl;//采样间隔
	U32 buffernum;//采集点数量

	double *exp_result;//预先保存指数计算的结果

	//类方法
	static DataCollection *GetSingleton();//单例模式
	static DataCollection GetInstance(){return *GetSingleton();}
	int register_Card();//注册采集卡
	int logout_Card();//注销采集卡
	int base_config();//采集卡基本配置
	int re_config(DataCollection &dc);//采集卡重新配置
	int start_collection(DataCollection *dc);//创建采集线程
	int CollectionData(DataCollection *dc);//调用windows api的多线程函数
	int CollectionData_debug(DataCollection *dc);//调试函数
	int emit(Frame *frame,callback cb,int thread_location);//装帧、上传数据的线程函数
	int Register_function(DataCollection &dc);
	int Logout_function(DataCollection &dc);
	int Set_function(DataCollection &dc);
	int Get_function(DataCollection *dc);
	int ThreadExitEvent();//线程退出事件处理
	void block_write_lock(int seq);
	void block_read_lock(int seq,int thread_num);
	void non_block_write_lock(int seq);
	void non_block_read_lock(int seq,int thread_num);
	void inline force_write(int seq);
	void force_read(int seq,int thread_num);
	int add_user(int thread_num,int user_num);
	int remove_user(int thread_num,int user_num);
	int Set_K1(double K);
	int Set_K2(double K);
private:
	I16 card_num;//采集卡编号
	I16 m_dev;//设备编号
	U16 buf_id;//buff的id
	U32 startPos;//buff要用
	U32 accessCnt;//buff要用
	BOOLEAN stopped;//异步采集通知标志位
	short *data_buffer;//采集卡数据
	static DataCollection * m_singleton ;
	//类方法
	DataCollection();
	DataCollection(const DataCollection &){}//复制构造函数
	~DataCollection(void);//析构函数
};
DataCollection *DataCollection::m_singleton=NULL;
HANDLE DataCollection::cThread = NULL;//采集线程，单例模式
HANDLE DataCollection::eThread = NULL;//上传线程，单例模式



//extern "C" MYDLL_API  test proxyx(test *cs);
#endif