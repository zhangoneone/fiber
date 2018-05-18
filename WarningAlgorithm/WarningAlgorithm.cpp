// WarningAlgorithm.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "WarningAlgorithm.h"
#include <PCIECollection.h>
#include<string>
#include<Windows.h>
using namespace std;

// 这是导出变量的一个示例
WARNINGALGORITHM_API int nWarningAlgorithm=0;

// 这是导出函数的一个示例。
WARNINGALGORITHM_API int fnWarningAlgorithm(void)
{
	return 42;
}

DWORD WINAPI Fun_GetBufferData(LPVOID lpParamter)//执行取数据操作
{
	//获取CWarningAlgorithm单件
	CWarningAlgorithm *wa;
	wa = wa->GetSingleton();
	wa->dc=wa->dc->GetSingleton();
	//获取被取缓冲池当前数据指针
	wa->frame->Seq =0;//wa->dc->fp->buffer_Current_Num;
	while(wa->wThread_stop==1)
	{
		wa->GetData();
		//帧序号自增
		wa->frame->Seq++;
		wa->frame->Seq %= (Max_Num);
	}
	return 0;
}

DWORD WINAPI Fun_HandleBufferData(LPVOID lpParamter)//执行处理数据操作
{
	//获取CWarningAlgorithm单件
	CWarningAlgorithm *wa;
	wa = wa->GetSingleton();
	//执行处理数据的方法
	wa->HandleData();
	return 0;
}

void cb()//回调入口
{
	CWarningAlgorithm *wa;
	wa = wa->GetSingleton();
	wa->dc = wa->dc->GetSingleton();
	//阻塞写
	wa->block_write_lock(wa->frame->Seq);
	//强制写入
	//wa->force_write(wa->frame->Seq);
	//复制帧载荷
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
	//复制帧其他信息
	wa->fp->frame[wa->frame->Seq].Seq = wa->frame->Seq;
	wa->fp->frame[wa->frame->Seq].time_cost = wa->frame->time_cost;
	//移动帧池指针
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
// 这是已导出类的构造函数。
// 有关类定义的信息，请参阅 WarningAlgorithm.h
CWarningAlgorithm::CWarningAlgorithm()
{
	dc = dc->GetSingleton();
	//实例化帧
	frame = new Frame();
	frame->buffer_load = new double[dc->buffernum];
	std::memset(frame->buffer_load,0,sizeof(double)*dc->buffernum);

	//预警算法参数设置
	setthreshold=60;//异常的阈值
	setexcepRate=0.5;//预报警的异常阈值
	setackRate=0.2;//报警的预报警阈值

	wThread_Num=0;
	hThread_Num=1;

	wThread_stop=1;
	hThread_stop=1;
		//实例化帧池
	fp = new Frame_Pool();
	fp->buffer_Current_Num = 0;
	fp->ref_thread=0;
	fp->Current_User=new user_manage[Max_Thread];
	for(int i=0;i<Max_Thread;i++)
	{
		fp->Current_User[i].thread_num=-1;
		fp->Current_User[i].user=0;
	}
	//申请帧内存
	for(int i=0;i<Max_Num;i++)
	{
		//申请帧载荷内存
		fp->frame[i].buffer_load = new double[dc->buffernum];
		fp->frame[i].user = new user_manage[Max_Thread];
		for(int j=0;j<Max_Thread;j++)
		{
			fp->frame[i].user[j].thread_num = -1;
			fp->frame[i].user[j].user=0;
		}
		fp->frame[i].Seq=0;
		fp->frame[i].time_cost=0;
		//帧载荷清0
		memset(fp->frame[i].buffer_load,0,sizeof(double)*dc->buffernum);
	}
	wq = new WarnMessageQue();

	//初始化读写锁和关键段
	InitializeCriticalSection(&bp_cs);
	InitializeSRWLock(&bp_srwLock);
	return;
}

int CWarningAlgorithm::Start_GetAndHandle()
{
	//取数据线程定义，并立即执行
	if(wThread==NULL)
	{
		//向被取的缓冲池注册user
		dc = dc->GetSingleton();
		wThred_location = dc->add_user(wThread_Num,Two_user);
		//创建线程并执行
		wThread = CreateThread(NULL, 0,Fun_GetBufferData,NULL, 0, NULL);
	//	CloseHandle(wThread);
	}
	//处理数据线程定义，并立即执行
	if(hThread==NULL)
	{
		//向被取的缓冲池注册user
		hThred_location = add_user(hThread_Num,One_user);
		//创建线程并执行
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
	//获取类指针
	dc = dc->GetSingleton();
	dc->emit(frame,cb,wThred_location);//传入回调,引用全局唯一的dc
}
volatile void CWarningAlgorithm::HandleData()//处理数据
{
	//获取CWarningAlgorithm单件
	CWarningAlgorithm *wa;
	wa = wa->GetSingleton();
	wa->dc = dc->GetSingleton();
	int circle_num=500;//以100帧作为一轮统计
	int circle_ack=5;//以5轮作为报警确认
	Frame *tmp_frame =new Frame();
	tmp_frame->buffer_load = new double[dc->buffernum];
	PointInfo *pointinfo = new PointInfo[dc->buffernum];
	std::memset(pointinfo,0,sizeof(PointInfo)*dc->buffernum);
	//获取被取缓冲池当前数据指针
	int Seq = wa->fp->buffer_Current_Num;
while(hThread_stop==1)
{
	while(circle_ack--)
	{
	  while(circle_num--)
	  {
		//读锁
		  wa->block_read_lock(Seq,hThred_location);
		//帧载荷拷贝
		std::memcpy(tmp_frame->buffer_load,wa->fp->frame[Seq].buffer_load,sizeof(wa->fp->frame[Seq].buffer_load[0])*wa->dc->buffernum);
		//其他信息拷贝
		tmp_frame->Seq = wa->fp->frame[Seq].Seq;
		tmp_frame->time_cost = wa->fp->frame[Seq].time_cost;
		//user自减
		wa->fp->frame[Seq].user[hThred_location].user--;
		//统计总帧、丢帧，丢帧率/这么做有点浪费空间
		pointinfo->frame_Num++;
		pointinfo->frame_Num%=4294967295;
		if (tmp_frame->time_cost>1)
		{
			pointinfo[0].loseFrame++;
			pointinfo[0].loseFrame%=4294967295;
		}
		//统计异常
		for(int i=0;i<dc->buffernum;i++)
		{
			//保存最大值
			if(pointinfo[i].MaxValue< tmp_frame->buffer_load[i])
			{
				pointinfo[i].MaxValue= tmp_frame->buffer_load[i];
			}
			//判断阈值
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
		;//重复查询Seq帧的user
	 }
	  //一个轮次结束，统计预报警确认次数
	  circle_num=500;
	  for(int i=0;i<dc->buffernum;i++)
	  {
		  //计算异常率
		  pointinfo[i].excepRate= pointinfo[i].excep / ( pointinfo[i].excep + pointinfo[i].nexcep+0.1-0.1 );
		 //根据异常率，统计预报警确认次数
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
	//多个轮次结束，统计结果
	circle_ack=5;//重新赋值，进行下一轮报警测试
		for(int i = 0;i<dc->buffernum;i++)
		{
			//计算预报警率
			pointinfo[i].ackRate = pointinfo[i].ack / (pointinfo[i].ack + pointinfo[i].nack+0.1-0.1);
			//决定是否报警
			if(pointinfo[i].ackRate >= setackRate)
			{
				//生成报警信息
				pointinfo[i].pointNum = i;
				pointinfo[i].warning = 1;
				pointinfo->loseFrameRate = pointinfo[0].loseFrameRate;
				//值传递到消息队列
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
	if(que->Wpt == que->Rpt-1)//写指针少用一个
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


