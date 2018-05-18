// WarningAlgorithm.cpp : ���� DLL Ӧ�ó���ĵ���������
//

#include "stdafx.h"
#include "WarningAlgorithm.h"
#include <PCIECollection.h>
#include<string>
#include<Windows.h>
using namespace std;

// ���ǵ���������һ��ʾ��
WARNINGALGORITHM_API int nWarningAlgorithm=0;

// ���ǵ���������һ��ʾ����
WARNINGALGORITHM_API int fnWarningAlgorithm(void)
{
	return 42;
}

DWORD WINAPI Fun_GetBufferData(LPVOID lpParamter)//ִ��ȡ���ݲ���
{
	//��ȡCWarningAlgorithm����
	CWarningAlgorithm *wa;
	wa = wa->GetSingleton();
	wa->dc=wa->dc->GetSingleton();
	//��ȡ��ȡ����ص�ǰ����ָ��
	wa->frame->Seq =0;//wa->dc->fp->buffer_Current_Num;
	while(wa->wThread_stop==1)
	{
		wa->GetData();
		//֡�������
		wa->frame->Seq++;
		wa->frame->Seq %= (Max_Num);
	}
	return 0;
}

DWORD WINAPI Fun_HandleBufferData(LPVOID lpParamter)//ִ�д������ݲ���
{
	//��ȡCWarningAlgorithm����
	CWarningAlgorithm *wa;
	wa = wa->GetSingleton();
	//ִ�д������ݵķ���
	wa->HandleData();
	return 0;
}

void cb()//�ص����
{
	CWarningAlgorithm *wa;
	wa = wa->GetSingleton();
	wa->dc = wa->dc->GetSingleton();
	//����д
	wa->block_write_lock(wa->frame->Seq);
	//ǿ��д��
	//wa->force_write(wa->frame->Seq);
	//����֡�غ�
	for (int i = 0; i <wa->dc->buffernum; i++)
	{
		wa->fp->frame[wa->frame->Seq].buffer_load[i] = wa->frame->buffer_load[i];
	}
	//copy user
	for(int i=0;i<wa->fp->ref_thread;i++)
	{
		wa->fp->frame[wa->frame->Seq].user[i].thread_num=wa->fp->Current_User[i].thread_num;
		wa->fp->frame[wa->frame->Seq].user[i].user = wa->fp->Current_User[i].user;
	}
	//����֡������Ϣ
	wa->fp->frame[wa->frame->Seq].Seq = wa->frame->Seq;
	wa->fp->frame[wa->frame->Seq].time_cost = wa->frame->time_cost;
	//�ƶ�֡��ָ��
	wa->fp->buffer_Current_Num++;
	wa->fp->buffer_Current_Num %= (Max_Num );
	return;
}

void CWarningAlgorithm::block_write_lock(int seq)
{
repeat:
	int i = 0;
	while(i< fp->ref_thread)
	{
		if(fp->frame[seq].user[i].user>Zero_user)
			goto repeat;
		else
			i++;
	}
}

void CWarningAlgorithm::block_read_lock(int seq,int thread_location)
{
repeatcheck:
	if(fp->frame[seq].user[thread_location].user>Zero_user||(wThread_stop==0&&hThread_stop==0))
	{
		goto exit;
	}
	else
		goto repeatcheck;
exit:
	;
}
void inline CWarningAlgorithm::force_write(int seq)
{
	;
}
int CWarningAlgorithm::add_user(int thread_num,int user_num)
{
	fp->Current_User[fp->ref_thread].thread_num=thread_num;
	fp->Current_User[fp->ref_thread].user+=user_num;
	fp->ref_thread++;
	return (fp->ref_thread-1);
}

int CWarningAlgorithm::remove_user(int thread_num,int user_num)
{
	fp->Current_User[thread_num].thread_num=thread_num;//wrong+++++++++++++
	fp->Current_User[thread_num].user-=user_num;
	fp->ref_thread--;
	return 0;
}
// �����ѵ�����Ĺ��캯����
// �й��ඨ�����Ϣ������� WarningAlgorithm.h
CWarningAlgorithm::CWarningAlgorithm()
{
	dc = dc->GetSingleton();
	//ʵ����֡
	frame = new Frame();
	frame->buffer_load = new double[dc->buffernum];
	std::memset(frame->buffer_load,0,sizeof(double)*dc->buffernum);

	//Ԥ���㷨��������
	setthreshold=60;//�쳣����ֵ
	setexcepRate=0.5;//Ԥ�������쳣��ֵ
	setackRate=0.2;//������Ԥ������ֵ

	wThread_Num=0;
	hThread_Num=1;

	wThread_stop=1;
	hThread_stop=1;
		//ʵ����֡��
	fp = new Frame_Pool();
	fp->buffer_Current_Num = 0;
	fp->ref_thread=0;
	fp->Current_User=new user_manage[Max_Thread];
	for(int i=0;i<Max_Thread;i++)
	{
		fp->Current_User[i].thread_num=-1;
		fp->Current_User[i].user=0;
	}
	//����֡�ڴ�
	for(int i=0;i<Max_Num;i++)
	{
		//����֡�غ��ڴ�
		fp->frame[i].buffer_load = new double[dc->buffernum];
		fp->frame[i].user = new user_manage[Max_Thread];
		for(int j=0;j<Max_Thread;j++)
		{
			fp->frame[i].user[j].thread_num = -1;
			fp->frame[i].user[j].user=0;
		}
		fp->frame[i].Seq=0;
		fp->frame[i].time_cost=0;
		//֡�غ���0
		memset(fp->frame[i].buffer_load,0,sizeof(double)*dc->buffernum);
	}
	wq = new WarnMessageQue();

	//��ʼ����д���͹ؼ���
	InitializeCriticalSection(&bp_cs);
	InitializeSRWLock(&bp_srwLock);
	return;
}

int CWarningAlgorithm::Start_GetAndHandle()
{
	//ȡ�����̶߳��壬������ִ��
	if(wThread==NULL)
	{
		//��ȡ�Ļ����ע��user
		dc = dc->GetSingleton();
		wThred_location = dc->add_user(wThread_Num,Two_user);
		//�����̲߳�ִ��
		wThread = CreateThread(NULL, 0,Fun_GetBufferData,NULL, 0, NULL);
	//	CloseHandle(wThread);
	}
	//���������̶߳��壬������ִ��
	if(hThread==NULL)
	{
		//��ȡ�Ļ����ע��user
		hThred_location = add_user(hThread_Num,One_user);
		//�����̲߳�ִ��
		hThread = CreateThread(NULL, 0,Fun_HandleBufferData,NULL, 0, NULL);
	//	CloseHandle(hThread);
	}
	return 0;
}

CWarningAlgorithm *CWarningAlgorithm::GetSingleton()
{	
		if(NULL==m_singleton)
		{
			m_singleton = new CWarningAlgorithm();
		}
		return m_singleton;
}

void CWarningAlgorithm::GetData()
{
	//��ȡ��ָ��
	dc = dc->GetSingleton();
	dc->emit(frame,cb,wThred_location);//����ص�,����ȫ��Ψһ��dc
}
volatile void CWarningAlgorithm::HandleData()//��������
{
	//��ȡCWarningAlgorithm����
	CWarningAlgorithm *wa;
	wa = wa->GetSingleton();
	wa->dc = dc->GetSingleton();
	int circle_num=500;//��100֡��Ϊһ��ͳ��
	int circle_ack=5;//��5����Ϊ����ȷ��
	Frame *tmp_frame =new Frame();
	tmp_frame->buffer_load = new double[dc->buffernum];
	PointInfo *pointinfo = new PointInfo[dc->buffernum];
	std::memset(pointinfo,0,sizeof(PointInfo)*dc->buffernum);
	//��ȡ��ȡ����ص�ǰ����ָ��
	int Seq = wa->fp->buffer_Current_Num;
while(hThread_stop==1)
{
	while(circle_ack--)
	{
	  while(circle_num--)
	  {
		//����
		  wa->block_read_lock(Seq,hThred_location);
		//֡�غɿ���
		std::memcpy(tmp_frame->buffer_load,wa->fp->frame[Seq].buffer_load,sizeof(wa->fp->frame[Seq].buffer_load[0])*wa->dc->buffernum);
		//������Ϣ����
		tmp_frame->Seq = wa->fp->frame[Seq].Seq;
		tmp_frame->time_cost = wa->fp->frame[Seq].time_cost;
		//user�Լ�
		wa->fp->frame[Seq].user[hThred_location].user--;
		//ͳ����֡����֡����֡��/��ô���е��˷ѿռ�
		pointinfo->frame_Num++;
		pointinfo->frame_Num%=4294967295;
		if (tmp_frame->time_cost>1)
		{
			pointinfo[0].loseFrame++;
			pointinfo[0].loseFrame%=4294967295;
		}
		//ͳ���쳣
		for(int i=0;i<dc->buffernum;i++)
		{
			//�������ֵ
			if(pointinfo[i].MaxValue< tmp_frame->buffer_load[i])
			{
				pointinfo[i].MaxValue= tmp_frame->buffer_load[i];
			}
			//�ж���ֵ
			if(tmp_frame->buffer_load[i] >= setthreshold||tmp_frame->buffer_load[i]<=(-setthreshold))
			{
				pointinfo[i].excep++;
			}
			else
			{
				pointinfo[i].nexcep++;
			}
		}
		Seq++;
		Seq %= (Max_Num ) ;
readblock:
		;//�ظ���ѯSeq֡��user
	 }
	  //һ���ִν�����ͳ��Ԥ����ȷ�ϴ���
	  circle_num=500;
	  for(int i=0;i<dc->buffernum;i++)
	  {
		  //�����쳣��
		  pointinfo[i].excepRate= pointinfo[i].excep / ( pointinfo[i].excep + pointinfo[i].nexcep+0.1-0.1 );
		 //�����쳣�ʣ�ͳ��Ԥ����ȷ�ϴ���
		 if(pointinfo[i].excepRate >= setexcepRate)
		 {
			pointinfo[i].ack++;
		 }
		 else
		 {
			pointinfo[i].nack++;
		 }
	  }
	}
	//����ִν�����ͳ�ƽ��
	circle_ack=5;//���¸�ֵ��������һ�ֱ�������
		for(int i = 0;i<dc->buffernum;i++)
		{
			//����Ԥ������
			pointinfo[i].ackRate = pointinfo[i].ack / (pointinfo[i].ack + pointinfo[i].nack+0.1-0.1);
			//�����Ƿ񱨾�
			if(pointinfo[i].ackRate >= setackRate)
			{
				//���ɱ�����Ϣ
				pointinfo[i].pointNum = i;
				pointinfo[i].warning = 1;
				pointinfo->loseFrameRate = pointinfo[0].loseFrameRate;
				//ֵ���ݵ���Ϣ����
				wa->wq->Add(pointinfo[i]);
			}
			else
				pointinfo[i].warning = 0;
		}
}
}

int CWarningAlgorithm::Set_threshold(double x)
{
	setthreshold=x;
	return 0;
}
int CWarningAlgorithm::Set_excepRate(double x)
{
	setexcepRate=x;
	return 0;
}
int CWarningAlgorithm::Set_ackRate(double x)
{
	setackRate=x;
	return 0;
}



int WarnMessageQue::Empty()
{
	if(que->Rpt==que->Wpt)
		return 1;
	else 
		return 0;
}

int WarnMessageQue::Full()
{
	que->Wpt %= que->Que_Max;
	que->Rpt %= que->Que_Max;
	if(que->Wpt == que->Rpt-1)//дָ������һ��
		return 1;
	else
		return 0;
}

int WarnMessageQue::CleanQue()
{
	que->Rpt=que->Wpt=0;
	memset(que,0,sizeof(Queue)*que->Que_Max);
	return 0;
}

int WarnMessageQue::Add(PointInfo p)
{
	if(!Full())
	{
		std::memcpy(&que->p[que->Wpt],&p,sizeof(PointInfo) );
		que->Wpt++;
		que->Wpt %= que->Que_Max;
	}
	else
	{return -1;}
	return 0;
}
PointInfo WarnMessageQue::Get()
{
	if(!Empty())
	{
		que->Rpt++;
		que->Rpt %= que->Que_Max;
		return que->p[que->Rpt];
	}
	else
		return *(PointInfo *)NULL;
}


