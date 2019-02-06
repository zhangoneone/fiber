// ���� ifdef ���Ǵ���ʹ�� DLL �������򵥵�
// ��ı�׼�������� DLL �е������ļ��������������϶���� MIDDLEWARE_EXPORTS
// ���ű���ġ���ʹ�ô� DLL ��
// �κ�������Ŀ�ϲ�Ӧ����˷��š�������Դ�ļ��а������ļ����κ�������Ŀ���Ὣ
// MIDDLEWARE_API ������Ϊ�Ǵ� DLL ����ģ����� DLL ���ô˺궨���
// ������Ϊ�Ǳ������ġ�
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
	double data_code;   //��ѡ�ĸ�������
}Command;//������

typedef struct{
	double location;//����λ��
	double warningMax;//�������ֵ
	double warningExecRate;//�����쳣��
	double warningAckRate;//����ȷ�ϱ�
	double loseFramRate;//��֡��
}WarningInfo;
// �����Ǵ� Middleware.dll ������
class MIDDLEWARE_API CMiddleware {
public:
	//��ֹ��������
	int singlenter_point;
	int singlenter_frame;
	//��ȡָ��++
	int point_ptr;
	int frame_ptr;
	DataCollection *dc;//PCIE�ɼ����ʵ��
	CWarningAlgorithm *wa;//CWarningAlgorithm���ʵ��
	static CMiddleware *GetSingleton();//����ģʽ
	CMiddleware(void);
	DataCollection * CallPCIECollectionDLL();//����PCIECollection.DLL
//	int CallDataHandleDll();//����DataHandle.DLL
	CWarningAlgorithm * CallWarningAlgorithmDLL();//����WarningAlgorithm.DLL
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
