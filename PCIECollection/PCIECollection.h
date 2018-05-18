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
}Config_Err_Mask;//0x00��ʾ��������

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
	int Seq;//֡���
	user_manage *user;//user
	double time_cost;//֡�ķѵ�ʱ��
	double *buffer_load;//֡�غ�
}Frame;//֡��Ϣ

typedef struct{
	int StartSeq;//֡��ʼ���
	int StopSeq;//֡�������
    int point;//ѡ����
	double *buffer_load;//���غ�
}Point;//���ʱ������

MYDLL_API Frame;
MYDLL_API Point;
//PCIEģ���֡�أ�ֻ��ű�������ݺ����ݲ�����
//Ҫ������
//Seq
//time_cost_array
//container
//buffer_load

typedef struct
{
	int buffer_Current_Num;//��ǰָ֡��
	int ref_thread;
	user_manage *Current_User;//֡user
	Frame frame[Max_Num];//֡��
}Frame_Pool;
MYDLL_API Frame_Pool;//����Frame_Pool

class Timer
{
public:
	LARGE_INTEGER *litmp;
	Timer(){
		litmp=new LARGE_INTEGER();
		QueryPerformanceFrequency(litmp);//���ʱ��Ƶ��
		dff=(double)litmp->QuadPart;
		start_counter = end_counter = dft = dfm = 0;
	}
	~Timer(){delete litmp;}
	void timer_start(){
		QueryPerformanceCounter(litmp);//��ó�ʼֵ
		start_counter=litmp->QuadPart;
	}
	void timer_end(){
		QueryPerformanceCounter(litmp);//�����ֵֹ
		end_counter=litmp->QuadPart;
	}
	double timer_ms(){
		dfm=(double)(end_counter-start_counter)*1000;//ת����ms
		dft=dfm/dff;//��ö�Ӧ��ʱ��ֵ
		return dft;
	}
private:
	LONGLONG start_counter,end_counter;//��ʼ��������������
	double dft;//ʱ����λms
	double dff;//ϵͳӲ��Ƶ��
	double dfm;//������
};//��ʱ��
typedef void(*callback)(void);//�����ص�����ָ��
//DataCollection��,�������༰�����������������c++ģ��ʹ��
class MYDLL_API DataCollection{
public:	
	Frame_Pool *fp;//֡��
	Timer *timer;//��ʱ��
	static HANDLE cThread;//�ɼ��߳̾��
	int  cThread_stop;//�������
	static HANDLE eThread;//�ϴ��߳̾��

	//����ض�д�߳���
	CRITICAL_SECTION bp_cs;
	SRWLOCK          bp_srwLock; 

	int handlebug;//�����һ֡bug���õı���
	//�ɼ�����
	double K1;
	double K2;//���ܷŴ����
	double d;//����
	U32 scantlv;//ɨ����
	U32 samptvl;//�������
	U32 buffernum;//�ɼ�������

	double *exp_result;//Ԥ�ȱ���ָ������Ľ��

	//�෽��
	static DataCollection *GetSingleton();//����ģʽ
	static DataCollection GetInstance(){return *GetSingleton();}
	int register_Card();//ע��ɼ���
	int logout_Card();//ע���ɼ���
	int base_config();//�ɼ�����������
	int re_config(DataCollection &dc);//�ɼ�����������
	int start_collection(DataCollection *dc);//�����ɼ��߳�
	int CollectionData(DataCollection *dc);//����windows api�Ķ��̺߳���
	int CollectionData_debug(DataCollection *dc);//���Ժ���
	int emit(Frame *frame,callback cb,int thread_location);//װ֡���ϴ����ݵ��̺߳���
	int Register_function(DataCollection &dc);
	int Logout_function(DataCollection &dc);
	int Set_function(DataCollection &dc);
	int Get_function(DataCollection *dc);
	int ThreadExitEvent();//�߳��˳��¼�����
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
	I16 card_num;//�ɼ������
	I16 m_dev;//�豸���
	U16 buf_id;//buff��id
	U32 startPos;//buffҪ��
	U32 accessCnt;//buffҪ��
	BOOLEAN stopped;//�첽�ɼ�֪ͨ��־λ
	short *data_buffer;//�ɼ�������
	static DataCollection * m_singleton ;
	//�෽��
	DataCollection();
	DataCollection(const DataCollection &){}//���ƹ��캯��
	~DataCollection(void);//��������
};
DataCollection *DataCollection::m_singleton=NULL;
HANDLE DataCollection::cThread = NULL;//�ɼ��̣߳�����ģʽ
HANDLE DataCollection::eThread = NULL;//�ϴ��̣߳�����ģʽ



//extern "C" MYDLL_API  test proxyx(test *cs);
#endif