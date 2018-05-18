// Middleware.cpp : ���� DLL Ӧ�ó���ĵ���������
//

#include "stdafx.h"
#include "Middleware.h"
#include<iostream>
using namespace std;
// ���ǵ���������һ��ʾ��
MIDDLEWARE_API int nMiddleware=0;

// ���ǵ���������һ��ʾ����
MIDDLEWARE_API int fnMiddleware(void)
{
	return 42;
}

extern "C" MIDDLEWARE_API int Command_function(Command *c)
{
	CMiddleware *cm=cm->GetSingleton();//��ȡ����
	switch(c->class_code)
	{
	case Start:cm->Start(c); break;
	case Config:cm->Config(c); break;
	case ParamSet:cm->ParamSet(c); break;
	case CollectionMode:cm->CollectionMode(c); break;
	case AmendMode:cm->AmendMode(c); break;
	case Refer:cm->Refer(c); break;
	case About:cm->About(c); break;
	default:	
		break;
	}
	return 0;
}
int CMiddleware::Start(Command *c)
{
	CMiddleware *cm=cm->GetSingleton();//��ȡ����
	cm->dc=cm->dc->GetSingleton();
	cm->wa=cm->wa->GetSingleton();
	int ret =0;
	switch(c->command_code)
	{
	case Register_Card:
	{
						  if (ret = cm->dc->register_Card() != 0)
							 return ret;
	}break;
	case Logout_Card:
	{
							cm->wa->hThread_stop=0;
							cm->wa->wThread_stop=0;
							cm->dc->cThread_stop=0;
#ifdef DEBUG
							return ret;
#endif
							if ( cm->dc->logout_Card() !=0 )
							return ret; 
	}break;
	default:
		break;
	}
	return 0;
}
int CMiddleware::Config(Command *c)
{
	CMiddleware *cm=cm->GetSingleton();//��ȡ����
	switch(c->command_code)
	{
	case Scan_Interval:
	case Samp_Interval:cm->dc->samptvl = cm->dc->scantlv = c->data_code; break;
	default:
		break;
	}
	return 0;
}
int CMiddleware::ParamSet(Command *c)
{
	CMiddleware *cm=cm->GetSingleton();//��ȡ����
	cm->dc=cm->dc->GetSingleton();
	cm->wa=cm->wa->GetSingleton();
	switch(c->command_code)
	{
	case Collection_Num:cm->dc->buffernum = c->data_code; break;//�������������������ط�Ҫ��
	case Set_K1:dc->Set_K1(c->data_code);break;
	case Set_K2:dc->Set_K2(c->data_code);break;
	case Set_threshold:cm->wa->Set_threshold(c->data_code);break;
	case Set_excepRate:cm->wa->Set_excepRate(c->data_code);break;
	case Set_ackRate:cm->wa->Set_ackRate(c->data_code);break;
	case One_Frame_Data_Tofile://Ӧ����c#����
	case One_Point_Data_Tofile://Ӧ����c#����
	default:
		break;
	}
	return 0;
}
int CMiddleware::CollectionMode(Command *c)
{
	CMiddleware *cm=cm->GetSingleton();//��ȡ����
	switch(c->command_code)
	{
	case Asyn_Collection:cm->dc->start_collection(cm->dc); break;//�����˲ɼ��߳�
	default:
		break;
	}
	return 0;
}
int CMiddleware::AmendMode(Command *c)
{
	CMiddleware *cm=cm->GetSingleton();//��ȡ����
	switch(c->command_code)
	{
	case Exp_Expand:cm->wa->Start_GetAndHandle(); break;//�������߳�get handle����
	default:
		break;
	}
	return 0;
}
int CMiddleware::Refer(Command *c)
{
	CMiddleware *cm=cm->GetSingleton();//��ȡ����
	switch(c->command_code)
	{
	case One_Frame_Data_Refer:
	case One_Point_Data_Refer:
	case Log_Refer://��ЩӦ��c#����
	default:
		break;
	}
	return 0;
}
int CMiddleware::About(Command *c)
{
	CMiddleware *cm=cm->GetSingleton();//��ȡ����
	switch(c->command_code)
	{
	case Help_Doc:
	case Software_Version://��ЩӦ��c#����
	default:
		break;
	}
	return 0;
}
extern "C" MIDDLEWARE_API int Data_function(Frame *d)
{
	CMiddleware *cm=cm->GetSingleton();
	if (cm == (CMiddleware *)NULL)//��ȡ���ɹ�
		goto err;
	CWarningAlgorithm *wa = cm->CallWarningAlgorithmDLL();
	if (wa == (CWarningAlgorithm *)NULL)//��ȡ���ɹ�
		goto err;
	DataCollection *dc = cm->CallPCIECollectionDLL();
	if (dc == (DataCollection *)NULL)//��ȡ���ɹ�
		goto err;

	int  Thread_2=2;
	static int Thread2_location = 0;
	//��ֹ��������룬������user
	if(cm->singlenter_frame==1)
	{
		cm->singlenter_frame=0;
		//��ȡ�����ע��user
		Thread2_location=wa->add_user(Thread_2,One_user);
		//��ȡ��ȡ�����ָ��
		cm->frame_ptr = wa->fp->buffer_Current_Num;
	}
	//�ӿ���֡��ʼ��
	while(wa->fp->frame[cm->frame_ptr].user[Thread2_location].user<=Zero_user)
	{
		cm->frame_ptr++;
		cm->frame_ptr%= (Max_Num);
	}

	d->Seq = wa->fp->frame[cm->frame_ptr].Seq;
	d->time_cost = wa->fp->frame[cm->frame_ptr].time_cost;
	std::memcpy(d->buffer_load,wa->fp->frame[cm->frame_ptr].buffer_load,sizeof(double)*dc->buffernum);
	wa->fp->frame[cm->frame_ptr].user[Thread2_location].user--;
	cm->frame_ptr++;
	cm->frame_ptr%= (Max_Num);
	return 0;
err:
	return -1;;
}
extern "C" MIDDLEWARE_API int Warning_function(WarningInfo *w)
{
	CMiddleware *cm=cm->GetSingleton();//��ȡ����
	cm->dc=cm->dc->GetSingleton();
	if (cm->wa->wq->Empty())
		return -1;
	PointInfo PI = cm->wa->wq->Get();
	w->location =PI.pointNum*cm->dc->d;
	w->loseFramRate=PI.loseFrameRate*100;
	w->warningAckRate=PI.ackRate*100;
	w->warningExecRate=PI.excepRate*100;
	w->warningMax=PI.MaxValue;
	return 0;
}

extern "C" MIDDLEWARE_API int Point_function(Point *p)
{
	CMiddleware *cm=cm->GetSingleton();
	if (cm == (CMiddleware *)NULL)//��ȡ���ɹ�
		goto err;
	CWarningAlgorithm *wa = cm->CallWarningAlgorithmDLL();
	if (wa == (CWarningAlgorithm *)NULL)//��ȡ���ɹ�
		goto err;
	DataCollection *dc = cm->CallPCIECollectionDLL();
	if (dc == (DataCollection *)NULL)//��ȡ���ɹ�
		goto err;
	int  Thread_3=3;
	static int Thread3_location =0;
	//��ֹ��������룬������user
	if(cm->singlenter_point==1)
	{
		cm->singlenter_point=0;
		//��ȡ�����ע��user
		Thread3_location=wa->add_user(Thread_3,One_user);
		//��ȡ��ȡ����ص�ǰָ��
		cm->point_ptr = wa->fp->buffer_Current_Num;
	}
	int i =cm->point_ptr;
	int j = 0;
	p->StartSeq=j;
	//�ҵ����õ㿪ʼ��
	while(wa->fp->frame[i].user[Thread3_location].user<=Zero_user)
	{
		i++;
		i%=(Max_Num);
	}
	while(wa->fp->frame[i].user[Thread3_location].user>Zero_user)
	{
		p->buffer_load[j]=wa->fp->frame[i].buffer_load[p->point];
		wa->fp->frame[i].user[Thread3_location].user--;
		i++;
		i %= (Max_Num);
		j++;
	}
	p->StopSeq = j;
	cm->point_ptr = i;//���¶�pointָ��
	return 0;
err:
	return -1;
}

CMiddleware *CMiddleware::GetSingleton()
	{	
		if(NULL==m_singleton)
		{
			m_singleton=new CMiddleware();
		}
		return m_singleton;
	}//����ģʽ

DataCollection * CMiddleware::CallPCIECollectionDLL()
{
	dc = dc->GetSingleton();
	//dc->start_collection(dc);
	return dc;
}

CWarningAlgorithm * CMiddleware::CallWarningAlgorithmDLL()
{
	wa = wa->GetSingleton();
	return wa;
}

// �����ѵ�����Ĺ��캯����
// �й��ඨ�����Ϣ������� Middleware.h
CMiddleware::CMiddleware()
{
	dc=CallPCIECollectionDLL();
	wa=CallWarningAlgorithmDLL();
	singlenter_point=1;
	singlenter_frame=1;
	point_ptr=0;
	frame_ptr=0;
	return;
}
