//导入环境配置、链接库里的驱动函数
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
public delegate void CallbackDelegate();
namespace _98421     
{
    public class WD_DASK
    {
        //ADLink PCI Card Type
        public const ushort PCI_9842 = 0x30;
        public const ushort MAX_CARD = 32;
        //Error Number
        public const short NoError = 0;
        public const short ErrorUnknownCardType = -1;
        public const short ErrorInvalidCardNumber = -2;
        public const short ErrorTooManyCardRegistered = -3;
        public const short ErrorCardNotRegistered = -4;
        public const short ErrorFuncNotSupport = -5;
        public const short ErrorInvalidIoChannel = -6;
        public const short ErrorInvalidAdRange = -7;
        public const short ErrorContIoNotAllowed = -8;
        public const short ErrorDiffRangeNotSupport = -9;
        public const short ErrorLastChannelNotZero = -10;
        public const short ErrorChannelNotDescending = -11;
        public const short ErrorChannelNotAscending = -12;
        public const short ErrorOpenDriverFailed = -13;
        public const short ErrorOpenEventFailed = -14;
        public const short ErrorTransferCountTooLarge = -15;
        public const short ErrorNotDoubleBufferMode = -16;
        public const short ErrorInvalidSampleRate = -17;
        public const short ErrorInvalidCounterMode = -18;
        public const short ErrorInvalidCounter = -19;
        public const short ErrorInvalidCounterState = -20;
        public const short ErrorInvalidBinBcdParam = -21;
        public const short ErrorBadCardType = -22;
        public const short ErrorInvalidDaRefVoltage = -23;
        public const short ErrorAdTimeOut = -24;
        public const short ErrorNoAsyncAI = -25;
        public const short ErrorNoAsyncAO = -26;
        public const short ErrorNoAsyncDI = -27;
        public const short ErrorNoAsyncDO = -28;
        public const short ErrorNotInputPort = -29;
        public const short ErrorNotOutputPort = -30;
        public const short ErrorInvalidDioPort = -31;
        public const short ErrorInvalidDioLine = -32;
        public const short ErrorContIoActive = -33;
        public const short ErrorDblBufModeNotAllowed = -34;
        public const short ErrorConfigFailed = -35;
        public const short ErrorInvalidPortDirection = -36;
        public const short ErrorBeginThreadError = -37;
        public const short ErrorInvalidPortWidth = -38;
        public const short ErrorInvalidCtrSource = -39;
        public const short ErrorOpenFile = -40;
        public const short ErrorAllocateMemory = -41;
        public const short ErrorDaVoltageOutOfRange = -42;
        public const short ErrorDaExtRefNotAllowed = -43;
        public const short ErrorInvalidBufferID = -44;
        public const short ErrorInvalidCNTInterval = -45;
        public const short ErrorReTrigModeNotAllowed = -46;
        public const short ErrorResetBufferNotAllowed = -47;
        public const short ErrorAnaTriggerLevel = -48;
        public const short ErrorDAQEvent = -49;
        public const short ErrorInvalidDataSize = -50;
        public const short ErrorOffsetCalibration = -51;
        public const short ErrorGainCalibration = -52;
        public const short ErrorCountOutofSDRAMSize = -53;
        public const short ErrorNotStartTriggerModule = -54;
        public const short ErrorInvalidRouteLine = -55;
        public const short ErrorInvalidSignalCode = -56;
        public const short ErrorInvalidSignalDirection = -57;
        public const short ErrorTRGOSCalibration = -58;
        //Error number for driver API 
        public const short ErrorConfigIoctl = -201;
        public const short ErrorAsyncSetIoctl = -202;
        public const short ErrorDBSetIoctl = -203;
        public const short ErrorDBHalfReadyIoctl = -204;
        public const short ErrorContOPIoctl = -205;
        public const short ErrorContStatusIoctl = -206;
        public const short ErrorPIOIoctl = -207;
        public const short ErrorDIntSetIoctl = -208;
        public const short ErrorWaitEvtIoctl = -209;
        public const short ErrorOpenEvtIoctl = -210;
        public const short ErrorCOSIntSetIoctl = -211;
        public const short ErrorMemMapIoctl = -212;
        public const short ErrorMemUMapSetIoctl = -213;
        public const short ErrorCTRIoctl = -214;
        public const short ErrorGetResIoctl = -215;

        public const ushort TRUE=1;
        public const ushort FALSE=0;
       //Synchronous Mode
        public const ushort SYNCH_OP = 1;
        public const ushort ASYNCH_OP = 2;
        //AD Range
        public const ushort AD_B_10_V = 1;
        public const ushort AD_B_5_V = 2;
        public const ushort AD_B_2_5_V = 3;
        public const ushort AD_B_1_25_V = 4;
        public const ushort AD_B_0_625_V = 5;
        public const ushort AD_B_0_3125_V = 6;
        public const ushort AD_B_0_5_V = 7;
        public const ushort AD_B_0_05_V = 8;
        public const ushort AD_B_0_005_V = 9;
        public const ushort AD_B_1_V = 10;
        public const ushort AD_B_0_1_V = 11;
        public const ushort AD_B_0_01_V = 12;
        public const ushort AD_B_0_001_V = 13;
        public const ushort AD_U_20_V = 14;
        public const ushort AD_U_10_V = 15;
        public const ushort AD_U_5_V = 16;
        public const ushort AD_U_2_5_V = 17;
        public const ushort AD_U_1_25_V = 18;
        public const ushort AD_U_1_V = 19;
        public const ushort AD_U_0_1_V = 20;
        public const ushort AD_U_0_01_V = 21;
        public const ushort AD_U_0_001_V = 22;
        public const short All_Channels = -1;
        public const ushort WD_AI_ADCONVSRC_TimePacer = 0;
        public const ushort WD_AI_TRGSRC_SOFT = 0x00;
        public const ushort WD_AI_TRGSRC_ANA = 0x01;
        public const ushort WD_AI_TRGSRC_ExtD = 0x02;
        public const ushort WD_AI_TRSRC_SSI_1 = 0x03;
        public const ushort WD_AI_TRSRC_SSI_2 = 0x04;
        public const ushort WD_AI_TRSRC_PXIStart = 0x05;
        public const ushort WD_AI_TRGMOD_POST = 0x00;   //Post Trigger Mode
        public const ushort WD_AI_TRGMOD_PRE = 0x01;   //Pre-Trigger Mode
        public const ushort WD_AI_TRGMOD_MIDL = 0x02;   //Middle Trigger Mode
        public const ushort WD_AI_TRGMOD_DELAY = 0x03;   //Delay Trigger Mode
        public const ushort WD_AI_TrgPositive = 0x1;
        public const ushort WD_AI_TrgNegative = 0x0;
        public const ushort WD_AIEvent_Manual = 0x80;   //AI event manual reset
        // define analog trigger Dedicated Channel 
        public const ushort CH0ATRIG = 0x00;
        public const ushort CH1ATRIG = 0x01;
        // Time Base 
        public const ushort WD_ExtTimeBase = 0x0;
        public const ushort WD_SSITimeBase = 0x1;
        public const ushort WD_StarTimeBase = 0x2;
        public const ushort WD_IntTimeBase = 0x3;
        public const ushort WD_PLL_REF_PXICLK10 = 0x4;
        public const ushort WD_PLL_REF_EXT10 = 0x5;
        //SSI signal codes
        public const ushort SSI_TIME = 15;
        public const ushort SSI_TRIG_SRC1 = 7;
        public const ushort SSI_TRIG_SRC2 = 5;
        public const ushort SSI_TRIG_SRC2_S = 5;
        public const ushort SSI_TRIG_SRC2_T = 6;
        // signal lines
        public const ushort PXI_TRIG_0 = 0;
        public const ushort PXI_TRIG_1 = 1;
        public const ushort PXI_TRIG_2 = 2;
        public const ushort PXI_TRIG_3 = 3;
        public const ushort PXI_TRIG_4 = 4;
        public const ushort PXI_TRIG_5 = 5;
        public const ushort PXI_TRIG_6 = 6;
        public const ushort PXI_TRIG_7 = 7;
        public const ushort PXI_START_TRIG = 8;
        public const ushort TRG_IO = 9;
        //Software trigger op code
        public const byte SOFTTRIG_AI = 0x1;
        //DAQ Event type for the event message  
        public const ushort DAQEnd = 0;
        public const ushort DBEvent = 1;
        //DAQ advanced mode  
        public const ushort DAQSTEPPED = 0x1;
        public const ushort RestartEn = 0x2;
        public const ushort DualBufEn = 0x4;
        public const ushort ManualSoftTrg = 0x40;
        /*------------------------------------------------------------------
        ** PCIS-DASK Function prototype
        ------------------------------------------------------------------*/
        [DllImport("WD-Dask64.dll")]
        public static extern short WD_AI_EventCallBack_x64(ushort wCardNumber, short mode, short EventType, MulticastDelegate callbackAddr);
        [DllImport("WD-Dask64.dll")]
        public static extern short WD_AI_SetTimeout(ushort wCardNumber, uint msec);//设置超时返回时间
       [DllImport("WD-Dask64.dll")]
        public static extern IntPtr WD_Buffer_Alloc(ushort wCardNumber, uint dwSize);
        [DllImport("WD-Dask.dll")]
        public static extern bool WD_Buffer_Free(ushort wCardNumber, short[] buf);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_Register_Card(ushort CardType, ushort card_num);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_Get_SDRAMSize(ushort CardNumber, out uint sdramsize);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_SoftTriggerGen(ushort wCardNumber, byte op);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_Release_Card(ushort CardNumber);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_GetActualRate(ushort wCardNumber, bool fdir, double Rate, out uint interval, out double ActualRate);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_GetPXIGeographAddr(ushort wCardNumber, out byte geo_addr);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_GetBaseAddr(ushort wCardNumber, out uint BaseAddr, out uint BaseAddr2);
        /*---------------------------------------------------------------------------*/
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_Config(ushort wCardNumber, ushort TimeBase, bool adDutyRestore, ushort ConvSrc, bool doubleEdged, bool AutoResetBuf);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_Trig_Config(ushort wCardNumber, ushort trigMode, ushort trigSrc, ushort trigPol, ushort anaTrigchan, double anaTriglevel, uint postTrigScans, uint preTrigScans, uint trigDelayTicks, uint reTrgCnt);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_Set_Mode(ushort wCardNumber, ushort modeCtrl, ushort wIter);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_CH_Config(ushort wCardNumber, short wChannel, ushort wAdRange);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_InitialMemoryAllocated(ushort CardNumber, out uint MemSize);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_VoltScale(ushort CardNumber, ushort AdRange, short reading, out double voltage);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_ContReadChannel(ushort CardNumber, ushort Channel,
          ushort BufId, uint ReadScans, uint ScanIntrv, uint SampIntrv, ushort SyncMode);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_ContReadMultiChannels(ushort CardNumber, ushort NumChans, ushort[] Chans,
          ushort BufId, uint ReadScans, uint ScanIntrv, uint SampIntrv, ushort SyncMode);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_ContScanChannels(ushort CardNumber, ushort Channel,
          ushort BufId, uint ReadScans, uint ScanIntrv, uint SampIntrv, ushort SyncMode);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_ContReadChannelToFile(ushort CardNumber, ushort Channel, ushort BufId,
          string FileName, uint ReadScans, uint ScanIntrv, uint SampIntrv, ushort SyncMode);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_ContReadMultiChannelsToFile(ushort CardNumber, ushort NumChans, ushort[] Chans,
          ushort BufId, string FileName, uint ReadScans, uint ScanIntrv, uint SampIntrv, ushort SyncMode);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_ContScanChannelsToFile(ushort CardNumber, ushort Channel, ushort BufId,
          string FileName, uint ReadScans, uint ScanIntrv, uint SampIntrv, ushort SyncMode);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_ContStatus(ushort CardNumber, out uint Status);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_ContVScale(ushort wCardNumber, ushort adRange, short[] readingArray, double[] voltageArray, int count);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_AsyncCheck(ushort CardNumber, out byte Stopped, out uint AccessCnt);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_AsyncClear(ushort CardNumber, out uint StartPos, out uint AccessCnt);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_AsyncDblBufferHalfReady(ushort CardNumber, out char HalfReady, out char StopFlag);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_AsyncDblBufferMode(ushort CardNumber, bool Enable);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_AsyncDblBufferToFile(ushort CardNumber);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_ContBufferSetup(ushort wCardNumber, short[] pwBuffer, uint dwReadCount, out ushort BufferId);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_ContVScale(ushort wCardNumber, ushort adRange, IntPtr readingArray, double[] voltageArray, int count);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_ContBufferSetup(ushort wCardNumber, IntPtr pwBuffer, uint dwReadCount, out ushort BufferId);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_ContBufferReset(ushort wCardNumber);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_DMA_Transfer(ushort wCardNumber, ushort BufId);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_ConvertCheck(ushort wCardNumber, out char bStopped);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_AsyncReStartNextReady(ushort wCardNumber, out char bReady, out char StopFlag, out ushort RdyDaqCnt);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AI_EventCallBack(ushort wCardNumber, short mode, short EventType, MulticastDelegate callbackAddr);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_SSI_SourceConn(ushort wCardNumber, ushort sigCode);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_SSI_SourceDisConn(ushort wCardNumber, ushort sigCode);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_SSI_SourceClear(ushort wCardNumber);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_Route_Signal(ushort wCardNumber, ushort signal, ushort Line, ushort dir);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_Signal_DisConn(ushort wCardNumber, ushort signal, ushort Line);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_AD_Auto_Calibration_ALL(ushort wCardNumber);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_EEPROM_CAL_Constant_Update(ushort wCardNumber, ushort bank);
        [DllImport("WD-Dask.dll")]
        public static extern short WD_Load_CAL_Data(ushort wCardNumber, ushort bank);
    }
}
