// PCIECollection.cpp : ���� DLL Ӧ�ó���ĵ���������
#include "stdafx.h"
#include "PCIECollection.h"
#include "Wd-dask.h"
#include <fstream>
#include<string>
#include<Windows.h>
#include<ctime>
#include<cstdlib>
#define random(a,b) (rand()%(b-a+1)+a)
using namespace std;

void* aligned_malloc(size_t size, int alignment)  
{  
        // �����㹻���ڴ�, ������㷨�ܾ���, ���ڵ�STL��ʹ�õľ�������㷨  
  
        // ������ά��FreeBlockָ��ռ�õ��ڴ��С  
        const int pointerSize = sizeof (void*);  
  
        // alignment - 1 + pointerSize�����FreeBlock�ڴ������Ҫ���ڴ��С  
        // ǰ�������sizeof(T) = 20, __alignof(T) = 16,  
        // g_MaxNumberOfObjectsInPool = 1000  
        // ��ô���ñ���������alignedMalloc(1000 * 20, 16)  
        // ��ôalignment - 1 + pointSize = 19  
        const int requestedSize = size + alignment - 1 + pointerSize;  
  
        // �����ʵ�ʴ�С����20000 + 19 = 20019  
        void* raw = malloc(requestedSize);  
  
        // ����ʵPool����Ϊ����ʵ��������ڴ��ַ  
        uintptr_t start = (uintptr_t) raw + pointerSize;  
		// �����������  
    	// ����һ��, __ALIGN - 1ָ������ʵ���ڴ���������  
    	// ����__ALIGN = 8ʱ, ����ֻ��Ҫ7�Ϳ���ʵ�ʱ�ʾ8����(0~7)  
    	// ��ô~(__ALIGN - 1)���ǽ������������  
    	// ���ǽ�(bytes) + __ALIGN-1)�����Ƚ��н�λ, Ȼ��ض�  
    	// ��ͱ�֤���������������  
    	// ����byte = 100, __ALIGN = 8�����  
    	// ~(__ALIGN - 1) = (1 000)B  
    	// ((bytes) + __ALIGN-1) = (1 101 011)B  
    	// (((bytes) + __ALIGN-1) & ~(__ALIGN - 1)) = (1 101 000 )B = (104)D  
    	// 104 / 8 = 13, ���ʵ������������  
    	// ����byte�պ������ڴ����������, �������byte��С����  
    	// �ǵá�Hacker's Delight����������صļ���  
    	// ������ʽ����������ĵȼ�  
    	// ((((bytes) + _ALIGN - 1) * _ALIGN) / _ALIGN)  
    	// ����SGI STLʹ�õķ���Ч�ʷǳ���   
        void* aligned = (void*) ((start + alignment - 1) & ~(alignment - 1));  
  
        // ����ά��һ��ָ��malloc()����������ڴ�  
        *(void**) ((uintptr_t) aligned - pointerSize) = raw;  
  
        // ����ʵ�����������ĵ�ַ  
        return aligned;  
}  
	// �������ڲ�ά�����ڴ����  
    //                   ���������ڴ����Ҫ��  
    //                             |  
    // ----------------------------------------------------------------------  
    // | �ڴ������� | ά����ָ�� | ����1 | ����2 | ����3 | ...... | ����n |  
    // ----------------------------------------------------------------------  
    // ^                     | ָ��malloc()����ĵ�ַ���  
    // |                     |  
    // -----------------------  
void aligned_free(void* aligned)  
 {  
        // �ͷŲ����ܼ���, �μ���ͼ  
        void* raw = *(void**) ((uintptr_t) aligned - sizeof (void*));  
        free(raw);  
 }  
bool isAligned(void* data, int alignment)   
{  
        // ����һ�������㷨, �μ�<Hacker's Delight>  
	return ((uintptr_t) data & (alignment - 1)) == 0;  
}  
void DataCollection::block_write_lock(int seq)
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
void DataCollection::block_read_lock(int seq,int thread_location)
{
repeatcheck:
	if(fp->frame[seq].user[thread_location].user>Zero_user)
	{
		goto exit;
	}
	else
		goto repeatcheck;
exit:
	;
}
void DataCollection::non_block_write_lock(int seq)
{
	
}
void DataCollection::non_block_read_lock(int seq,int thread_num)
{

}
void inline DataCollection::force_write(int seq)
{
	;
}
int DataCollection::add_user(int thread_num,int user_num)
{
	fp->Current_User[fp->ref_thread].thread_num=thread_num;
	fp->Current_User[fp->ref_thread].user+=user_num;
	fp->ref_thread++;
	return (fp->ref_thread-1);
}
int DataCollection::remove_user(int thread_num,int user_num)
{
	fp->Current_User[thread_num].thread_num=thread_num;
	fp->Current_User[thread_num].user-=user_num;
	fp->ref_thread--;
	return 0;
}
//���캯��
DataCollection::DataCollection()
{
	//�ɼ���������ʼ��
	card_num = 0;
	stopped=0;
	buffernum = 5000;
	samptvl = scantlv = 20;

	//���ܷŴ������ʼ��
	K1 = 1;
	K2 = 0.000015;
	handlebug=1;
	d = (scantlv / 2);
	//֡������ʼ��
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
		fp->frame[i].buffer_load = new double[buffernum];
		fp->frame[i].user = new user_manage[Max_Thread];
		for(int j=0;j<Max_Thread;j++)
		{
			fp->frame[i].user[j].thread_num = -1;
			fp->frame[i].user[j].user=0;
		}
		fp->frame[i].Seq=0;
		fp->frame[i].time_cost=0;
		//֡�غ���0
		memset(fp->frame[i].buffer_load,0,sizeof(double)*buffernum);
	}
	//��ʱ����ʼ��
	timer=new Timer();

	//��д����ʼ��
	InitializeCriticalSection(&bp_cs);
	InitializeSRWLock(&bp_srwLock);

	//����ɼ����ڴ�
//	data_buffer = (short*)aligned_malloc(sizeof(short)*buffernum,16);
	data_buffer = (short*)_aligned_malloc(sizeof(short)*buffernum, 16);
	bool is = isAligned(data_buffer,16);
	for(int i=0;i<buffernum;i++)
	{
		data_buffer[i] = 0;
	}
}
//��������
DataCollection::~DataCollection()
{
	//�ͷŲɼ����ڴ�
	free(data_buffer);

	delete timer;

	//�ͷŻ�����ڴ�
	for(int i=0;i<Max_Num;i++)
	{
		//�ͷ�֡�غ��ڴ�
		delete fp->frame[i].buffer_load;
	}
	delete []fp->frame;
	delete fp;
}
//��ȡ����ָ��
DataCollection *DataCollection::GetSingleton()
	{	
		if(NULL==m_singleton)
		{
			m_singleton=new DataCollection();
		}
		return m_singleton;
	}//����ģʽ
int DataCollection::register_Card()
{
	int ret = 0;
#if DEBUG
	return Config_OK;
#endif
	m_dev = WD_Register_Card (PCIe_9842, card_num);

	if(m_dev < 0)
	{
		WD_Release_Card(m_dev);//�ͷ��豸
		return Register_card_err;
	}
	ret = base_config();
	if (ret < 0)
	{
		return ret;
	}
	return Config_OK;
}
int DataCollection::logout_Card()
{
	int ret = 0;
	ret = WD_AI_AsyncClear(m_dev, &startPos, &accessCnt);
	ret = WD_Buffer_Free(m_dev, data_buffer);     
    ret = WD_AI_ContBufferReset(m_dev);
    ret = WD_Release_Card(m_dev);
	return ret;
}
int DataCollection::base_config()
{
	int ret=0;
		 	 //-1���������ŵ���0-7�ŵ�
			 //+-5V
	 ret = WD_AI_CH_Config(m_dev,-1,AD_B_1_V);
	 if(ret < 0)
	 {
		 WD_Buffer_Free (m_dev, data_buffer);//�ͷ�buffer
		 WD_Release_Card(m_dev);//�ͷ��豸
		 return AI_CH_Config;
	 }
			//����2 ѡ��ʱ��Դ���˴�Ϊ�ڲ�ʱ��Դ
			//����3 �Ƿ񼤻� ad dutyѭ���ָ�
			//����4 adת��Դ��ѡ��
			//����5 �Ƿ���ad ping pongģʽ
			//����6 ģ��������ɺ��Ƿ�����ģ  ��ai�Ļ���
	ret = WD_AI_Config(m_dev,WD_IntTimeBase, true,WD_AI_ADCONVSRC_TimePacer, false, true);
	if(ret < 0)
	{
		 WD_Buffer_Free (m_dev, data_buffer);//�ͷ�buffer
		 WD_Release_Card(m_dev);//�ͷ��豸
		 return AI_Config;
	}
            //���ô���Դ ģʽ ���ܣ��������κ�ai֮ǰ���øú���
            //2����ģʽֻ��ѡ��delay ����post ����0x00��post 0x03��delay
            //3 ����Դ ��ѡsoft���� �ⲿ���� ssi���������֣� ������Ӧ0 2 3 4
            //4 �������½��ش��� 1������ 0�½���
            //5 �ŵ�ѡ��
            //6 ������ֵѡ�� �����ź�������ֵѡ����0-3.3.Ĭ��1.67
            //7 �������м䴥���������˴���ʱ�䴫������������
            //8 û����
            //9 �¼��������ӳ�x��tick��ִ�У�
            //10 
	ret = WD_AI_Trig_Config(m_dev, 0, 2, 1, 0, 1.0, 0, 0, 0, 1);
    if (ret < 0)
     {
		 WD_Buffer_Free (m_dev, data_buffer);//�ͷ�buffer
		 WD_Release_Card(m_dev);//�ͷ��豸
		 return AI_Trig_Config;
     }
            //ÿ����һ�θú���������һ��ai���棬���������������ϵ�ai���������
            //2.�洢���ݵĻ����׵�ַ���õ�ַ��Ҫ16�ֽڶ��롣
            //3.����Ĵ�С(in sample)
            //4 ��ǰ�����Ļ�������
     ret = WD_AI_ContBufferSetup(m_dev, data_buffer, buffernum,&buf_id);//�Խ�ģ�����ݵ�buffer//Ҫ��data_buffer�ĵ�ַ��16�ֽڶ���
     if (ret < 0)
      {
		  WD_Buffer_Free(m_dev, data_buffer);     //��������ʧ�ܳ��԰�buffer��С���ô�һ��
          WD_AI_ContBufferReset(m_dev);
          WD_Release_Card(m_dev);
          return AI_ContBufferSetup;
      }
	 return Config_OK;
}
int DataCollection::re_config(DataCollection &dc)
{
	int ret = 0;
	return ret;
}
int data_save_tofile(const char *filename,void *buffer)
{
	string tmp_filename=filename;
	tmp_filename+=".txt";
	ofstream out(tmp_filename); 
     if (!out.is_open())   
     {  
		 return Save_tofile_failed;
     }  
	  out << buffer; 
      out.close();  
     return 0;
}
int DataCollection::ThreadExitEvent()
{
	printf("�߳��˳�\n");
	return 0;
}
DWORD WINAPI Fun_Collection(LPVOID lpParamter)
{
	DataCollection *dc=static_cast<DataCollection *>(lpParamter);//ǿתvoidָ��
#if DEBUG
	goto debug;
#endif
	dc->CollectionData(dc);
debug:
	dc->CollectionData_debug(dc);
	return 0;
}
int DataCollection::start_collection(DataCollection *dc)//�����ɼ��߳�
{
	int ret;
	//�ɼ��̶߳��壬������ִ��
	if(cThread==NULL)
	{
		cThread = CreateThread(NULL, 0,Fun_Collection,dc, 0, NULL);//����ɼ��̵߳��ǻ����
	//	CloseHandle(cThread);
	}
	return 0;
}

int DataCollection::CollectionData_debug(DataCollection *dc)//��ɴӲɼ����ɼ����ݵĹ���
{
    int ret = 0;
	srand((unsigned)time(NULL));
	static int jishu=0;
	do
	{
		Sleep(0.8);
		timer->timer_start();
		//��ѯuser,����д
		block_write_lock(dc->fp->buffer_Current_Num);
		//ǿ��д
		//force_write(dc->fp->buffer_Current_Num);
		for(int i=0;i<dc->buffernum;i++)
		{
			dc->fp->frame[dc->fp->buffer_Current_Num].buffer_load[i] = random(2900,3000);//jishu++;//
		}
		//����֡user
		for(int i=0;i<dc->fp->ref_thread;i++)
		{
			dc->fp->frame[dc->fp->buffer_Current_Num].user[i].thread_num = dc->fp->Current_User[i].thread_num;
			dc->fp->frame[dc->fp->buffer_Current_Num].user[i].user = dc->fp->Current_User[i].user;
		}
		//�����һ֡��bug
		for(int i=0;i<dc->fp->ref_thread;i++)
		{
			if(dc->fp->buffer_Current_Num==0 &&  handlebug==1)
				dc->fp->frame[dc->fp->buffer_Current_Num].user[i].user = One_user;//0 hao xian cheng
			handlebug=0;
		}
		//��¼֡���
		dc->fp->frame[dc->fp->buffer_Current_Num].Seq = dc->fp->buffer_Current_Num;
		//��¼�ķ�ʱ��
		timer->timer_end();
		dc->fp->frame[dc->fp->buffer_Current_Num].time_cost = timer->timer_ms();
		//ѭ����仺���
		dc->fp->buffer_Current_Num++;
		dc->fp->buffer_Current_Num %= Max_Num;
	} while (true);
err:
	//�˴�Ӧ��ӻص�������֪ͨ�߳��˳��¼�
	ThreadExitEvent();
	return ret;
}
//�˺����Ӳɼ�����ȡ���ݣ���д�뻺���
int DataCollection::CollectionData(DataCollection *dc)//��ɴӲɼ����ɼ����ݵĹ���
{
    int ret = 0;
    if( (ret = WD_AI_ContReadChannel(m_dev, 0, buf_id, buffernum, scantlv, samptvl,ASYNCH_OP) ) <0 )
		goto err;
	do
	{
		//�˺�������ʱ��Լ0.006ms,�˺�������һ�Σ�ֻ��һ�μ��
        //����2 trueʱ�������첽ģ����������������󣬿���ȡ�����ˡ�false�����첽���뻹û�н���
		ret = WD_AI_AsyncCheck(m_dev,&stopped,&accessCnt);//stopΪtrue�����첽ģ���������,ִ���첽����
		if (ret < 0)
			goto err;
		if (stopped == 1)//�첽�������,��������
		{
			timer->timer_start();
			//����д����
			//	AcquireSRWLockExclusive(&dc->bp_srwLock);
			//��ѯuser,����д
			block_write_lock(dc->fp->buffer_Current_Num);
			//ǿ��д��
			//force_write(dc->fp->buffer_Current_Num);
			for(int i=0;i<dc->buffernum;i++)
			{
				dc->fp->frame[dc->fp->buffer_Current_Num].buffer_load[i] = dc->data_buffer[i];
			}
		//����֡user
			for(int i=0;i<dc->fp->ref_thread;i++)
			{
				dc->fp->frame[dc->fp->buffer_Current_Num].user[i].thread_num = dc->fp->Current_User[i].thread_num;
				dc->fp->frame[dc->fp->buffer_Current_Num].user[i].user = dc->fp->Current_User[i].user;
			}
		//�����һ֡��bug
			for(int i=0;i<dc->fp->ref_thread;i++)
			{
				if(dc->fp->buffer_Current_Num==0 &&  handlebug==1)
					dc->fp->frame[dc->fp->buffer_Current_Num].user[i].user = One_user;//0 hao xian cheng
				handlebug=0;
			}
			//��¼֡���
			dc->fp->frame[dc->fp->buffer_Current_Num].Seq = dc->fp->buffer_Current_Num;
			//��¼�ķ�ʱ��
			timer->timer_end();
			dc->fp->frame[dc->fp->buffer_Current_Num].time_cost = timer->timer_ms();
			//�ͷ�д����
		//	ReleaseSRWLockExclusive(&dc->bp_srwLock);
			//ѭ����仺���
			dc->fp->buffer_Current_Num++;
			dc->fp->buffer_Current_Num %= Max_Num;
			//���װ֡������������һ������
			if ((ret = WD_AI_ContReadChannel(m_dev, 0, buf_id, buffernum, scantlv, samptvl, ASYNCH_OP)) <0)
				goto err;
		}
		//��ָ����Ƶ���Խӽ�ָ�������ʣ�ִ���������ϵ�adת����˫����ģʽ���������ϵ�adת������֧��post��������ʱ����ģʽ
        //����1 ִ�иò����Ŀ� id
        //����2 ģ��Ƶ��id
        //����3 ��buffersetup�������ص�һ��������id�����Ļ������飬�����˲��������
        //����4 ɨ����ܸ�����Ӧ����8�ı���
        //����5 ɨ�����ĳ���/����ֵ 1-65535
        //����6 ��������ĳ���/����ֵ 1-65535
       //����7 ����ͬ�������첽ִ�С���pre-/middle triggerʱ���ú������첽ִ�е�
       //ͬ��ת��ʱ��������������ֱ��adת����ɡ��첽ת��ʱ��������������
	//	if ((ret = WD_AI_ContReadChannel(m_dev, 0, buf_id, buffernum, scantlv, samptvl, ASYNCH_OP)) <0)
	//		goto err;
		stopped = 0;//��0
	} while (true);
err:
	//�˴�Ӧ��ӻص�������֪ͨ�߳��˳��¼�
	ThreadExitEvent();
	return ret;
}
//�˺������ϲ���ã��ӻ���ض�ȡ���ݣ�װ֡�������ϲ㴫������
int DataCollection::emit(Frame *frame,callback cb,int thread_location,int ExpandOrNot)
{
	block_read_lock(frame->Seq,thread_location);
	block_read_lock((frame->Seq+1)%Max_Num,thread_location);
	//�ڻ�����У���֡��ǰ֡������������֡
	for(int i =0 ;i<buffernum;i++)
	{
		frame->buffer_load[i] = fp->frame[(frame->Seq+1)%Max_Num].buffer_load[i] - fp->frame[frame->Seq].buffer_load[i];
		if(frame->buffer_load[i]==-3270000)
		{printf("�˴�i��%d\n�����%dֵ��%d\n",i,frame->Seq,fp->frame[(frame->Seq+1)%Max_Num].buffer_load[i]);}
		else{}
	}
	//����ʱ
	frame->time_cost = fp->frame[frame->Seq].time_cost;
	//user�Լ�
	fp->frame[(frame->Seq+1)%Max_Num].user[thread_location].user--;
	fp->frame[frame->Seq].user[thread_location].user--;
	//timer->timer_start();
	//�Ŵ��ж�,�ٷ���
	if(ExpandOrNot==1)
	{
		for(int i =0 ;i<buffernum;i++)
		{
			frame->buffer_load[i] = ( frame->buffer_load[i]*K1) * std::pow(10,(i+1)*K2*d );
		}
	}
	//timer->timer_end();
	//double time = timer->timer_ms();
	//�ص�
CallReturn:
	cb();
	return 0;
}
int DataCollection::Register_function(DataCollection &dc)
{
    //ע����Ϣ������ɲɼ���ע�ᡢ���õ�����
	int ret = 0;
	ret = dc.register_Card();//ע��
	ret = dc.base_config();//Ĭ������
	return ret;
}
int DataCollection::Logout_function(DataCollection &dc)
{
	int ret = 0;
	ret = dc.logout_Card();
	return ret;
}
int DataCollection::Set_function(DataCollection &dc)
{
	int ret=0;
	ret = re_config(dc);
	return ret;
}
int DataCollection::Get_function(DataCollection *dc)
{
	//�ϴ��̶߳��壬������ִ��
	//if(eThread==NULL)
	//{
	//	eThread = CreateThread(NULL, 0,Fun_Emit,dc, 0, NULL);
	//	CloseHandle(cThread);
	//}
	return 0;
}
int DataCollection::Set_K1(double K)
{
	K1=K;
	return 0;
}
int DataCollection::Set_K2(double K)
{
	K2=K;
	return 0;
}
