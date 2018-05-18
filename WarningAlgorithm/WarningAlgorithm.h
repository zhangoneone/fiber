// ���� ifdef ���Ǵ���ʹ�� DLL �������򵥵�
// ��ı�׼�������� DLL �е������ļ��������������϶���� WARNINGALGORITHM_EXPORTS
// ���ű���ġ���ʹ�ô� DLL ��
// �κ�������Ŀ�ϲ�Ӧ����˷��š�������Դ�ļ��а������ļ����κ�������Ŀ���Ὣ
// WARNINGALGORITHM_API ������Ϊ�Ǵ� DLL ����ģ����� DLL ���ô˺궨���
// ������Ϊ�Ǳ������ġ�
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
	int pointNum;//����
	double MaxValue;//�õ����ֵ
	int excep;//�쳣����,���ÿһ���ɼ���
	int nexcep;//��������
	double excepRate;//�쳣��
	int ack;//����ȷ�ϴ���
	int nack;//�������ϴ���
	int ackRate;//����ȷ����
	short warning;//�õ��Ƿ񱨾� 1 ���� 0 ������

	int frame_Num;//��֡��
	int loseFrame;//��֡��
	double loseFrameRate;//��֡��
}PointInfo;

typedef struct
{
	int Que_Max;//���г���
	PointInfo *p;//����Ԫ������,�ͳ���
	int Rpt,Wpt;//���еĶ�дָ��
//	Queue *next;//������
}Queue;
class WarnMessageQue{//������Ϣ����
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
// �����Ǵ� WarningAlgorithm.dll �����ġ�ȥ���˵�����־WARNINGALGORITHM_API
class CWarningAlgorithm {
public:
	WARNINGALGORITHM_API static CWarningAlgorithm * GetSingleton();//��ȡ����
	Frame *frame; 
	Frame_Pool *fp;//֡��
	DataCollection *dc;//PCIE�ɼ����ʵ��
	double setthreshold;//�쳣����ֵ
	double setexcepRate;//Ԥ�������쳣��ֵ
	double setackRate;//������Ԥ������ֵ
	WARNINGALGORITHM_API int Start_GetAndHandle();//��ȡ����������
	WarnMessageQue *wq;//������Ϣ����
	void GetData();//�����ߺ���
	volatile void HandleData();//��Ϊ������
	WARNINGALGORITHM_API int add_user(int thread_num,int user_num);
	WARNINGALGORITHM_API int remove_user(int thread_num,int user_num);
	WARNINGALGORITHM_API void block_write_lock(int seq);
	WARNINGALGORITHM_API void block_read_lock(int seq,int thread_location);
	WARNINGALGORITHM_API void inline force_write(int seq);
	WARNINGALGORITHM_API void force_read(int seq,int thread_num);
	WARNINGALGORITHM_API int Set_threshold(double x);
	WARNINGALGORITHM_API int Set_excepRate(double x);
	WARNINGALGORITHM_API int Set_ackRate(double x);
	//����ض�д�߳���
	CRITICAL_SECTION bp_cs;
	SRWLOCK          bp_srwLock; 

	static HANDLE wThread;//��ȡ�����̣߳����
	int wThread_Num;
	int wThred_location;
	int wThread_stop;
	static HANDLE hThread;//���������̣߳����
	int hThread_Num;
	int hThred_location;
	int hThread_stop;
private:
	static CWarningAlgorithm *m_singleton;
	CWarningAlgorithm(void);
};
CWarningAlgorithm *CWarningAlgorithm::m_singleton = NULL;
HANDLE CWarningAlgorithm::wThread = NULL;//�̣߳�����ģʽ
HANDLE CWarningAlgorithm::hThread = NULL;//�̣߳�����ģʽ
extern WARNINGALGORITHM_API int nWarningAlgorithm;

WARNINGALGORITHM_API int fnWarningAlgorithm(void);
#endif
