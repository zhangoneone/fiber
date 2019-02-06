// PCIECollection.cpp : 定义 DLL 应用程序的导出函数。
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
        // 分配足够的内存, 这里的算法很经典, 早期的STL中使用的就是这个算法  
  
        // 首先是维护FreeBlock指针占用的内存大小  
        const int pointerSize = sizeof (void*);  
  
        // alignment - 1 + pointerSize这个是FreeBlock内存对齐需要的内存大小  
        // 前面的例子sizeof(T) = 20, __alignof(T) = 16,  
        // g_MaxNumberOfObjectsInPool = 1000  
        // 那么调用本函数就是alignedMalloc(1000 * 20, 16)  
        // 那么alignment - 1 + pointSize = 19  
        const int requestedSize = size + alignment - 1 + pointerSize;  
  
        // 分配的实际大小就是20000 + 19 = 20019  
        void* raw = malloc(requestedSize);  
  
        // 这里实Pool真正为对象实例分配的内存地址  
        uintptr_t start = (uintptr_t) raw + pointerSize;  
		// 向上舍入操作  
    	// 解释一下, __ALIGN - 1指明的是实际内存对齐的粒度  
    	// 例如__ALIGN = 8时, 我们只需要7就可以实际表示8个数(0~7)  
    	// 那么~(__ALIGN - 1)就是进行舍入的粒度  
    	// 我们将(bytes) + __ALIGN-1)就是先进行进位, 然后截断  
    	// 这就保证了我是向上舍入的  
    	// 例如byte = 100, __ALIGN = 8的情况  
    	// ~(__ALIGN - 1) = (1 000)B  
    	// ((bytes) + __ALIGN-1) = (1 101 011)B  
    	// (((bytes) + __ALIGN-1) & ~(__ALIGN - 1)) = (1 101 000 )B = (104)D  
    	// 104 / 8 = 13, 这就实现了向上舍入  
    	// 对于byte刚好满足内存对齐的情况下, 结果保持byte大小不变  
    	// 记得《Hacker's Delight》上面有相关的计算  
    	// 这个表达式与下面给出的等价  
    	// ((((bytes) + _ALIGN - 1) * _ALIGN) / _ALIGN)  
    	// 但是SGI STL使用的方法效率非常高   
        void* aligned = (void*) ((start + alignment - 1) & ~(alignment - 1));  
  
        // 这里维护一个指向malloc()真正分配的内存  
        *(void**) ((uintptr_t) aligned - pointerSize) = raw;  
  
        // 返回实例对象真正的地址  
        return aligned;  
}  
	// 这里是内部维护的内存情况  
    //                   这里满足内存对齐要求  
    //                             |  
    // ----------------------------------------------------------------------  
    // | 内存对齐填充 | 维护的指针 | 对象1 | 对象2 | 对象3 | ...... | 对象n |  
    // ----------------------------------------------------------------------  
    // ^                     | 指向malloc()分配的地址起点  
    // |                     |  
    // -----------------------  
void aligned_free(void* aligned)  
 {  
        // 释放操作很简单了, 参见上图  
        void* raw = *(void**) ((uintptr_t) aligned - sizeof (void*));  
        free(raw);  
 }  
bool isAligned(void* data, int alignment)   
{  
        // 又是一个经典算法, 参见<Hacker's Delight>  
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
//构造函数
DataCollection::DataCollection()
{
	//采集卡参数初始化
	card_num = 0;
	stopped=0;
	buffernum = 5000;
	samptvl = scantlv = 20;

	//保密放大参数初始化
	K1 = 1;
	K2 = 0.000015;
	handlebug=1;
	d = (scantlv / 2);
	//帧参数初始化
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
		fp->frame[i].buffer_load = new double[buffernum];
		fp->frame[i].user = new user_manage[Max_Thread];
		for(int j=0;j<Max_Thread;j++)
		{
			fp->frame[i].user[j].thread_num = -1;
			fp->frame[i].user[j].user=0;
		}
		fp->frame[i].Seq=0;
		fp->frame[i].time_cost=0;
		//帧载荷清0
		memset(fp->frame[i].buffer_load,0,sizeof(double)*buffernum);
	}
	//计时器初始化
	timer=new Timer();

	//读写锁初始化
	InitializeCriticalSection(&bp_cs);
	InitializeSRWLock(&bp_srwLock);

	//申请采集卡内存
//	data_buffer = (short*)aligned_malloc(sizeof(short)*buffernum,16);
	data_buffer = (short*)_aligned_malloc(sizeof(short)*buffernum, 16);
	bool is = isAligned(data_buffer,16);
	for(int i=0;i<buffernum;i++)
	{
		data_buffer[i] = 0;
	}
}
//析构函数
DataCollection::~DataCollection()
{
	//释放采集卡内存
	free(data_buffer);

	delete timer;

	//释放缓冲池内存
	for(int i=0;i<Max_Num;i++)
	{
		//释放帧载荷内存
		delete fp->frame[i].buffer_load;
	}
	delete []fp->frame;
	delete fp;
}
//获取单例指针
DataCollection *DataCollection::GetSingleton()
	{	
		if(NULL==m_singleton)
		{
			m_singleton=new DataCollection();
		}
		return m_singleton;
	}//单件模式
int DataCollection::register_Card()
{
	int ret = 0;
#if DEBUG
	return Config_OK;
#endif
	m_dev = WD_Register_Card (PCIe_9842, card_num);

	if(m_dev < 0)
	{
		WD_Release_Card(m_dev);//释放设备
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
		 	 //-1代表所有信道，0-7信道
			 //+-5V
	 ret = WD_AI_CH_Config(m_dev,-1,AD_B_1_V);
	 if(ret < 0)
	 {
		 WD_Buffer_Free (m_dev, data_buffer);//释放buffer
		 WD_Release_Card(m_dev);//释放设备
		 return AI_CH_Config;
	 }
			//参数2 选择时钟源，此处为内部时钟源
			//参数3 是否激活 ad duty循环恢复
			//参数4 ad转换源的选择
			//参数5 是否开启ad ping pong模式
			//参数6 模拟输入完成后，是否重置模  拟ai的缓存
	ret = WD_AI_Config(m_dev,WD_IntTimeBase, true,WD_AI_ADCONVSRC_TimePacer, false, true);
	if(ret < 0)
	{
		 WD_Buffer_Free (m_dev, data_buffer);//释放buffer
		 WD_Release_Card(m_dev);//释放设备
		 return AI_Config;
	}
            //设置触发源 模式 性能，必须在任何ai之前调用该函数
            //2触发模式只能选择delay 或者post 触发0x00是post 0x03是delay
            //3 触发源 可选soft触发 外部触发 ssi触发（两种） 参数对应0 2 3 4
            //4 上升或下降沿触发 1上升沿 0下降沿
            //5 信道选择
            //6 触发阈值选择 数字信号输入阈值选择是0-3.3.默认1.67
            //7 仅用于中间触发。表明了触发时间传递来的数据量
            //8 没看懂
            //9 事件触发后，延迟x个tick后执行？
            //10 
	ret = WD_AI_Trig_Config(m_dev, 0, 2, 1, 0, 1.0, 0, 0, 0, 1);
    if (ret < 0)
     {
		 WD_Buffer_Free (m_dev, data_buffer);//释放buffer
		 WD_Release_Card(m_dev);//释放设备
		 return AI_Trig_Config;
     }
            //每调用一次该函数，产生一个ai缓存，用来保存连续不断的ai。最多两个
            //2.存储数据的缓存首地址，该地址需要16字节对齐。
            //3.缓存的大小(in sample)
            //4 当前建立的缓存索引
     ret = WD_AI_ContBufferSetup(m_dev, data_buffer, buffernum,&buf_id);//对接模拟数据的buffer//要求data_buffer的地址是16字节对齐
     if (ret < 0)
      {
		  WD_Buffer_Free(m_dev, data_buffer);     //这里设置失败尝试把buffer大小设置大一点
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
	printf("线程退出\n");
	return 0;
}
DWORD WINAPI Fun_Collection(LPVOID lpParamter)
{
	DataCollection *dc=static_cast<DataCollection *>(lpParamter);//强转void指针
#if DEBUG
	goto debug;
#endif
	dc->CollectionData(dc);
debug:
	dc->CollectionData_debug(dc);
	return 0;
}
int DataCollection::start_collection(DataCollection *dc)//创建采集线程
{
	int ret;
	//采集线程定义，并立即执行
	if(cThread==NULL)
	{
		cThread = CreateThread(NULL, 0,Fun_Collection,dc, 0, NULL);//传入采集线程的是缓冲池
	//	CloseHandle(cThread);
	}
	return 0;
}

int DataCollection::CollectionData_debug(DataCollection *dc)//完成从采集卡采集数据的过程
{
    int ret = 0;
	srand((unsigned)time(NULL));
	static int jishu=0;
	do
	{
		Sleep(0.8);
		timer->timer_start();
		//查询user,阻塞写
		block_write_lock(dc->fp->buffer_Current_Num);
		//强制写
		//force_write(dc->fp->buffer_Current_Num);
		for(int i=0;i<dc->buffernum;i++)
		{
			dc->fp->frame[dc->fp->buffer_Current_Num].buffer_load[i] = random(2900,3000);//jishu++;//
		}
		//更改帧user
		for(int i=0;i<dc->fp->ref_thread;i++)
		{
			dc->fp->frame[dc->fp->buffer_Current_Num].user[i].thread_num = dc->fp->Current_User[i].thread_num;
			dc->fp->frame[dc->fp->buffer_Current_Num].user[i].user = dc->fp->Current_User[i].user;
		}
		//解决第一帧的bug
		for(int i=0;i<dc->fp->ref_thread;i++)
		{
			if(dc->fp->buffer_Current_Num==0 &&  handlebug==1)
				dc->fp->frame[dc->fp->buffer_Current_Num].user[i].user = One_user;//0 hao xian cheng
			handlebug=0;
		}
		//记录帧序号
		dc->fp->frame[dc->fp->buffer_Current_Num].Seq = dc->fp->buffer_Current_Num;
		//记录耗费时间
		timer->timer_end();
		dc->fp->frame[dc->fp->buffer_Current_Num].time_cost = timer->timer_ms();
		//循环填充缓冲池
		dc->fp->buffer_Current_Num++;
		dc->fp->buffer_Current_Num %= Max_Num;
	} while (true);
err:
	//此处应添加回调函数，通知线程退出事件
	ThreadExitEvent();
	return ret;
}
//此函数从采集卡读取数据，并写入缓冲池
int DataCollection::CollectionData(DataCollection *dc)//完成从采集卡采集数据的过程
{
    int ret = 0;
    if( (ret = WD_AI_ContReadChannel(m_dev, 0, buf_id, buffernum, scantlv, samptvl,ASYNCH_OP) ) <0 )
		goto err;
	do
	{
		//此函数消耗时间约0.006ms,此函数调用一次，只做一次检测
        //参数2 true时，代表异步模拟输入结束或发生错误，可以取数据了。false代表异步输入还没有结束
		ret = WD_AI_AsyncCheck(m_dev,&stopped,&accessCnt);//stop为true代表异步模拟输入结束,执行异步操作
		if (ret < 0)
			goto err;
		if (stopped == 1)//异步输入结束,处理数据
		{
			timer->timer_start();
			//请求写者锁
			//	AcquireSRWLockExclusive(&dc->bp_srwLock);
			//查询user,阻塞写
			block_write_lock(dc->fp->buffer_Current_Num);
			//强制写入
			//force_write(dc->fp->buffer_Current_Num);
			for(int i=0;i<dc->buffernum;i++)
			{
				dc->fp->frame[dc->fp->buffer_Current_Num].buffer_load[i] = dc->data_buffer[i];
			}
		//更改帧user
			for(int i=0;i<dc->fp->ref_thread;i++)
			{
				dc->fp->frame[dc->fp->buffer_Current_Num].user[i].thread_num = dc->fp->Current_User[i].thread_num;
				dc->fp->frame[dc->fp->buffer_Current_Num].user[i].user = dc->fp->Current_User[i].user;
			}
		//解决第一帧的bug
			for(int i=0;i<dc->fp->ref_thread;i++)
			{
				if(dc->fp->buffer_Current_Num==0 &&  handlebug==1)
					dc->fp->frame[dc->fp->buffer_Current_Num].user[i].user = One_user;//0 hao xian cheng
				handlebug=0;
			}
			//记录帧序号
			dc->fp->frame[dc->fp->buffer_Current_Num].Seq = dc->fp->buffer_Current_Num;
			//记录耗费时间
			timer->timer_end();
			dc->fp->frame[dc->fp->buffer_Current_Num].time_cost = timer->timer_ms();
			//释放写者锁
		//	ReleaseSRWLockExclusive(&dc->bp_srwLock);
			//循环填充缓冲池
			dc->fp->buffer_Current_Num++;
			dc->fp->buffer_Current_Num %= Max_Num;
			//完成装帧，开启接收下一轮数据
			if ((ret = WD_AI_ContReadChannel(m_dev, 0, buf_id, buffernum, scantlv, samptvl, ASYNCH_OP)) <0)
				goto err;
		}
		//在指定的频道以接近指定的速率，执行连续不断的ad转换，双缓存模式的连续不断的ad转换仅仅支持post触发和延时触发模式
        //参数1 执行该操作的卡 id
        //参数2 模拟频道id
        //参数3 由buffersetup函数返回的一个参数，id索引的缓存数组，包含了捕获的数据
        //参数4 扫描的总个数，应该是8的倍数
        //参数5 扫描间隔的长度/计数值 1-65535
        //参数6 采样间隔的长度/计数值 1-65535
       //参数7 声明同步或者异步执行。打开pre-/middle trigger时，该函数是异步执行的
       //同步转换时，函数会阻塞，直到ad转换完成。异步转换时，函数正常返回
	//	if ((ret = WD_AI_ContReadChannel(m_dev, 0, buf_id, buffernum, scantlv, samptvl, ASYNCH_OP)) <0)
	//		goto err;
		stopped = 0;//置0
	} while (true);
err:
	//此处应添加回调函数，通知线程退出事件
	ThreadExitEvent();
	return ret;
}
//此函数由上层调用，从缓冲池读取数据，装帧、并向上层传递数据
int DataCollection::emit(Frame *frame,callback cb,int thread_location,int ExpandOrNot)
{
	block_read_lock(frame->Seq,thread_location);
	block_read_lock((frame->Seq+1)%Max_Num,thread_location);
	//在缓冲池中，后帧减前帧，填充待发数据帧
	for(int i =0 ;i<buffernum;i++)
	{
		frame->buffer_load[i] = fp->frame[(frame->Seq+1)%Max_Num].buffer_load[i] - fp->frame[frame->Seq].buffer_load[i];
		if(frame->buffer_load[i]==-3270000)
		{printf("此处i是%d\n序号是%d值是%d\n",i,frame->Seq,fp->frame[(frame->Seq+1)%Max_Num].buffer_load[i]);}
		else{}
	}
	//填充耗时
	frame->time_cost = fp->frame[frame->Seq].time_cost;
	//user自减
	fp->frame[(frame->Seq+1)%Max_Num].user[thread_location].user--;
	fp->frame[frame->Seq].user[thread_location].user--;
	//timer->timer_start();
	//放大判断,再发送
	if(ExpandOrNot==1)
	{
		for(int i =0 ;i<buffernum;i++)
		{
			frame->buffer_load[i] = ( frame->buffer_load[i]*K1) * std::pow(10,(i+1)*K2*d );
		}
	}
	//timer->timer_end();
	//double time = timer->timer_ms();
	//回调
CallReturn:
	cb();
	return 0;
}
int DataCollection::Register_function(DataCollection &dc)
{
    //注册消息，需完成采集卡注册、配置等任务
	int ret = 0;
	ret = dc.register_Card();//注册
	ret = dc.base_config();//默认配置
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
	//上传线程定义，并立即执行
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
