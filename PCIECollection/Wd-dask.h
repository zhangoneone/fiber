#ifndef		_WD_DASK_H
#define		_WD_DASK_H

#ifdef __cplusplus
extern "C" {
#endif

//DASK Data Types
typedef unsigned char   U8;
typedef short           I16;
typedef unsigned short  U16;
typedef long            I32;
typedef unsigned long   U32;
typedef float           F32;
typedef double          F64;

//ADLink PCI Card Type
//PCI/PXI-9820
#define PCI_9820        0x1
//PXI 98x6 devices
#define PXI_9816D       0x2
#define PXI_9826D       0x3
#define PXI_9846D       0x4
#define PXI_9846DW      0x4
#define PXI_9816H       0x5
#define PXI_9826H       0x6
#define PXI_9846H       0x7
#define PXI_9846HW      0x7
#define PXI_9816V       0x8
#define PXI_9826V       0x9
#define PXI_9846V       0xa
#define PXI_9846VW      0xa
#define PXI_9846VID     0xb
//PCI 98x6 devices
#define PCI_9816D       0x12
#define PCI_9826D       0x13
#define PCI_9846D       0x14
#define PCI_9846DW      0x14
#define PCI_9816H       0x15
#define PCI_9826H       0x16
#define PCI_9846H       0x17
#define PCI_9846HW      0x17
#define PCI_9816V       0x18
#define PCI_9826V       0x19
#define PCI_9846V       0x1a
#define PCI_9846VW      0x1a
//PCIe 98x6 devices
#define PCIe_9816D      0x22
#define PCIe_9826D      0x23
#define PCIe_9846D      0x24
#define PCIe_9846DW     0x24
#define PCIe_9816H      0x25
#define PCIe_9826H      0x26
#define PCIe_9846H      0x27
#define PCIe_9846HW     0x27
#define PCIe_9816V      0x28
#define PCIe_9826V      0x29
#define PCIe_9846V      0x2a
#define PCIe_9846VW     0x2a
//PCIe/PXIe-9842
#define PCIe_9842       0x30
//PXIe-9848
#define PXIe_9848       0x32
//PCIe-9852
#define PCIe_9852       0x33
//PXIe-9852
#define PXIe_9852       0x34
//obsolete
#define PCI_9816        PXI_9816D
#define PCI_9826        PXI_9826D
#define PCI_9846        PXI_9846D

#define MAX_CARD        32

//Error Number
#define NoError                       0
#define ErrorUnknownCardType         -1
#define ErrorInvalidCardNumber       -2
#define ErrorTooManyCardRegistered   -3
#define ErrorCardNotRegistered       -4
#define ErrorFuncNotSupport          -5
#define ErrorInvalidIoChannel        -6
#define ErrorInvalidAdRange          -7
#define ErrorContIoNotAllowed        -8
#define ErrorDiffRangeNotSupport     -9
#define ErrorLastChannelNotZero      -10
#define ErrorChannelNotDescending    -11
#define ErrorChannelNotAscending     -12
#define ErrorOpenDriverFailed        -13
#define ErrorOpenEventFailed         -14
#define ErrorTransferCountTooLarge   -15
#define ErrorNotDoubleBufferMode     -16
#define ErrorInvalidSampleRate       -17
#define ErrorInvalidCounterMode      -18
#define ErrorInvalidCounter          -19
#define ErrorInvalidCounterState     -20
#define ErrorInvalidBinBcdParam      -21
#define ErrorBadCardType             -22
#define ErrorInvalidDaRefVoltage     -23
#define ErrorAdTimeOut               -24
#define ErrorNoAsyncAI               -25
#define ErrorNoAsyncAO               -26
#define ErrorNoAsyncDI               -27
#define ErrorNoAsyncDO               -28
#define ErrorNotInputPort            -29
#define ErrorNotOutputPort           -30
#define ErrorInvalidDioPort          -31
#define ErrorInvalidDioLine          -32
#define ErrorContIoActive            -33
#define ErrorDblBufModeNotAllowed    -34
#define ErrorConfigFailed            -35
#define ErrorInvalidPortDirection    -36
#define ErrorBeginThreadError        -37
#define ErrorInvalidPortWidth        -38
#define ErrorInvalidCtrSource        -39
#define ErrorOpenFile                -40
#define ErrorAllocateMemory          -41
#define ErrorDaVoltageOutOfRange     -42
#define ErrorDaExtRefNotAllowed      -43
#define ErrorInvalidBufferID         -44
#define ErrorInvalidCNTInterval	     -45
#define ErrorReTrigModeNotAllowed    -46
#define ErrorResetBufferNotAllowed   -47
#define ErrorAnaTriggerLevel         -48
#define ErrorDAQEvent		     -49
#define ErrorInvalidDataSize         -50 
#define ErrorOffsetCalibration       -51
#define ErrorGainCalibration         -52
#define ErrorCountOutofSDRAMSize     -53
#define ErrorNotStartTriggerModule   -54
#define ErrorInvalidRouteLine        -55
#define ErrorInvalidSignalCode       -56
#define ErrorInvalidSignalDirection  -57
#define ErrorTRGOSCalibration        -58

#define ErrorNoSDRAM                 -59
#define ErrorIntegrationGain         -60
#define ErrorAcquisitionTiming       -61
#define ErrorIntegrationTiming       -62
#define ErrorInvalidTraceCnt         -63
#define ErrorTriggerSource           -64
#define ErrorInvalidTimeBase         -70
#define ErrorUndefinedParameter	     -71

#define ErrorNotDAQSteppedMode       -80

#define ErrorBufAddrNotQuadDWordAlignment   -90

//Error number for calibration API
#define ErrorCalAddress		     -110
#define ErrorInvalidCalBank	     -111
//Error number for driver API 
#define ErrorConfigIoctl	     -201
#define ErrorAsyncSetIoctl	     -202
#define ErrorDBSetIoctl		     -203
#define ErrorDBHalfReadyIoctl	     -204
#define ErrorContOPIoctl	     -205
#define ErrorContStatusIoctl	     -206
#define ErrorPIOIoctl		     -207
#define ErrorDIntSetIoctl	     -208
#define ErrorWaitEvtIoctl	     -209
#define ErrorOpenEvtIoctl	     -210
#define ErrorCOSIntSetIoctl	     -211
#define ErrorMemMapIoctl	     -212
#define ErrorMemUMapSetIoctl	     -213
#define ErrorCTRIoctl		     -214
#define ErrorGetResIoctl	     -215

#define TRUE    1
#define FALSE   0

//Synchronous Mode
#define SYNCH_OP        1
#define ASYNCH_OP       2

//AD Range
#define AD_B_10_V       1
#define AD_B_5_V        2
#define AD_B_2_5_V      3
#define AD_B_1_25_V     4
#define AD_B_0_625_V    5
#define AD_B_0_3125_V   6
#define AD_B_0_5_V      7
#define AD_B_0_05_V     8
#define AD_B_0_005_V    9
#define AD_B_1_V       10
#define AD_B_0_1_V     11
#define AD_B_0_01_V    12
#define AD_B_0_001_V   13
#define AD_U_20_V      14
#define AD_U_10_V      15
#define AD_U_5_V       16
#define AD_U_2_5_V     17
#define AD_U_1_25_V    18
#define AD_U_1_V       19
#define AD_U_0_1_V     20
#define AD_U_0_01_V    21
#define AD_U_0_001_V   22
#define AD_B_2_V       23
#define AD_B_0_25_V    24
#define AD_B_0_2_V     25
#define AD_U_4_V       26
#define AD_U_2_V       27
#define AD_U_0_5_V     28
#define AD_U_0_4_V     29
#define AD_B_1_5_V     30
#define AD_B_0_2145_V  31

#define All_Channels   -1

#define WD_AI_ADCONVSRC_TimePacer 0

#define WD_AI_TRGSRC_SOFT      0x00   
#define WD_AI_TRGSRC_ANA       0x01   
#define WD_AI_TRGSRC_ExtD      0x02   
#define WD_AI_TRSRC_SSI_1      0x03
#define WD_AI_TRSRC_SSI_2      0x04
#define WD_AI_TRSRC_PXIStar    0x05
#define WD_AI_TRSRC_PXIeStar   0x06            
#define WD_AI_TRGMOD_POST      0x00   //Post Trigger Mode
#define WD_AI_TRGMOD_PRE       0x01   //Pre-Trigger Mode
#define WD_AI_TRGMOD_MIDL      0x02   //Middle Trigger Mode
#define WD_AI_TRGMOD_DELAY     0x03   //Delay Trigger Mode
#define WD_AI_TrgPositive      0x1
#define WD_AI_TrgNegative      0x0

//obsolete
#define WD_AI_TRSRC_PXIStart   0x05

#define WD_AIEvent_Manual      0x80   //AI event manual reset

/* define analog trigger Dedicated Channel */
#define CH0ATRIG	   0x00
#define CH1ATRIG	   0x01
#define CH2ATRIG	   0x02
#define CH3ATRIG	   0x03
#define CH4ATRIG	   0x04
#define CH5ATRIG	   0x05
#define CH6ATRIG	   0x06
#define CH7ATRIG	   0x07

/* Time Base */
#define WD_ExtTimeBase		  0x0
#define WD_SSITimeBase		  0x1
#define	WD_StarTimeBase		  0x2
#define WD_IntTimeBase		  0x3
#define	WD_PXI_CLK10		  0x4
#define	WD_PLL_REF_PXICLK10	  0x4
#define	WD_PLL_REF_EXT10	  0x5
#define	WD_PXIe_CLK100		  0x6
#define WD_PLL_REF_PXIeCLK100	  0x6

//SSI signal codes
#define SSI_TIME        15
#define SSI_TRIG_SRC1   7
#define SSI_TRIG_SRC2   5
#define SSI_TRIG_SRC2_S 5
#define SSI_TRIG_SRC2_T 6 
#define SSI_PRE_DATA_RDY 0x10
// signal lines
#define PXI_TRIG_0      0
#define PXI_TRIG_1      1
#define PXI_TRIG_2      2
#define PXI_TRIG_3      3
#define PXI_TRIG_4      4
#define PXI_TRIG_5      5
#define PXI_TRIG_6      6
#define PXI_TRIG_7      7
#define PXI_STAR_TRIG   8
#define TRG_IO		9

//SSI cable lines
#define SSI_LINE_0      0
#define SSI_LINE_1      1
#define SSI_LINE_2      2
#define SSI_LINE_3      3
#define SSI_LINE_4      4
#define SSI_LINE_5      5
#define SSI_LINE_6      6
#define SSI_LINE_7      7

//obsolete
#define PXI_START_TRIG  8

//Software trigger op code
#define SOFTTRIG_AI	 0x1
#define SOFTTRIG_AI_OUT	 0x2
//DAQ Event type for the event message  
#define DAQEnd   0
#define DBEvent  1
#define TrigEvent  2
//DAQ advanced mode  
#define DAQSTEPPED    0x1   
#define RestartEn     0x2
#define DualBufEn     0x4
#define ManualSoftTrg 0x40
#define DMASTEPPED    0x80
#define AI_AVE		 		0x8
#define AI_AVE_32		 	0x10

//define ai channel parameter
#define AI_RANGE	0
#define AI_IMPEDANCE	1
#define ADC_DITHER	2
#define AI_COUPLING	3
#define ADC_Bandwidth	4

//define ai channel parameter value
#define IMPEDANCE_50Ohm 0
#define IMPEDANCE_HI	1

#define ADC_DITHER_DIS	0	
#define ADC_DITHER_EN	1

#define DC_Coupling	0	
#define AC_Coupling	1

#define BANDWIDTH_DEVICE_DEFAULT 	0	
#define BANDWIDTH_20M	20
#define BANDWIDTH_100M	100

//ai trigger out channel
#define AITRIGOUT_CH0	0
#define AITRIGOUT_PXI	2
#define AITRIGOUT_PXI_TRIG_0	2
#define AITRIGOUT_PXI_TRIG_1	3
#define AITRIGOUT_PXI_TRIG_2	4
#define AITRIGOUT_PXI_TRIG_3	5
#define AITRIGOUT_PXI_TRIG_4	6
#define AITRIGOUT_PXI_TRIG_5	7
#define AITRIGOUT_PXI_TRIG_6	8
#define AITRIGOUT_PXI_TRIG_7	9

//DIO Port Direction
#define INPUT_PORT      1
#define OUTPUT_PORT     2
//DIO Line Direction
#define INPUT_LINE      1
#define OUTPUT_LINE     2
//DIO mode
#define SDI_En      0
#define SDI_Dis     1

//Calibration Action
#define CalLoad		0
#define AutoCal		1
#define CalCopy		2

//TrigOUT Config
#define WD_OutTrgPWidth_50ns   0x0
#define WD_OutTrgPWidth_100ns  0x1
#define WD_OutTrgPWidth_150ns  0x2
#define WD_OutTrgPWidth_200ns  0x3
#define WD_OutTrgPWidth_500ns  0x4
#define WD_OutTrgPWidth_1us    0x5
#define WD_OutTrgPWidth_2us    0x6
#define WD_OutTrgPWidth_5us    0x7
#define WD_OutTrgPWidth_10us   0x8

//TrigOUT SRC/POL Config
#define WD_OutTrgSrcAuto   0x0
#define WD_OutTrgSrcManual 0x1
#define WD_OutTrg_Rising   0x0
#define WD_OutTrg_Fall     0x10

//device enum struct
typedef struct
{
U16 wModuleType;
U16 wCardID;
U16 geo_addr;
U16 Reserved;
char dispname[64];
} WD_DEVICE, *PWD_DEVICE; 

/*------------------------------------------------------------------
** PCIS-DASK Function prototype
------------------------------------------------------------------*/
I16 __stdcall WD_Register_Card (U16 CardType, U16 card_num);
I16 __stdcall WD_Register_Card_By_PXISlot_GA (U16 CardType, U16 ga);
I16 __stdcall WD_Get_SDRAMSize  (U16 CardNumber, U32 *sdramsize);
I16 __stdcall WD_SoftTriggerGen(U16 wCardNumber, U8 op);
I16 __stdcall WD_Release_Card  (U16 CardNumber);
I16 __stdcall WD_GetActualRate(U16 wCardNumber, BOOLEAN fdir, F64 Rate, U32* interval, F64 *ActualRate);
I16 __stdcall WD_GetPXIGeographAddr (U16 wCardNumber, U8* geo_addr);
I16 __stdcall WD_GetBaseAddr(U16 wCardNumber, U32 *BaseAddr, U32 *BaseAddr2);
void * __stdcall WD_Buffer_Alloc (U16 wCardNumber, U32 dwSize);
BOOL __stdcall WD_Buffer_Free (U16 wCardNumber, void *buf);
I16 __stdcall WD_Device_Scan(WD_DEVICE* AvailModules, U16 ncount, U16* retcnt); 
I16 __stdcall WD_GetFPGAVersion(USHORT wCardNumber, U32* version);
/*---------------------------------------------------------------------------*/
I16 __stdcall WD_AI_Config (U16 wCardNumber, U16 TimeBase, BOOLEAN adDutyRestore, U16 ConvSrc, BOOLEAN doubleEdged, BOOLEAN AutoResetBuf);
I16 __stdcall WD_AI_Trig_Config (U16 wCardNumber, U16 trigMode, U16 trigSrc, U16 trigPol, U16 anaTrigchan, F64 anaTriglevel, U32 postTrigScans, U32 preTrigScans, U32 trigDelayTicks, U32 reTrgCnt);
I16 __stdcall WD_OutTrig_Config (U16 wCardNumber, U16 trig_Ch, U16 trig_conf, U16 trig_width);
I16 __stdcall WD_AI_Set_Mode (U16 wCardNumber, U16 modeCtrl, U16 wIter);
I16 __stdcall WD_AI_CH_Config (U16 wCardNumber, U16 wChannel, U16 wAdRange);
I16 __stdcall WD_AI_CH_ChangeParam (U16 wCardNumber, U16 wChannel, U16 wParam, U16 wValue);
I16 __stdcall WD_AI_InitialMemoryAllocated (U16 CardNumber, U32 *MemSize);
I16 __stdcall WD_AI_VoltScale (U16 CardNumber, U16 AdRange, I16 reading, F64 *voltage);
I16 __stdcall WD_AI_VoltScale32 (U16 wCardNumber, U16 adRange, I32 reading, F64 *voltage);
I16 __stdcall WD_AI_ContReadChannel (U16 CardNumber, U16 Channel,
               U16 BufId, U32 ReadScans, U32 ScanIntrv, U32 SampIntrv, U16 SyncMode);
I16 __stdcall WD_AI_ContReadMultiChannels (U16 CardNumber, U16 NumChans, U16 *Chans,
               U16 BufId, U32 ReadScans, U32 ScanIntrv, U32 SampIntrv, U16 SyncMode);
I16 __stdcall WD_AI_ContScanChannels (U16 CardNumber, U16 Channel,
               U16 BufId, U32 ReadScans, U32 ScanIntrv, U32 SampIntrv, U16 SyncMode);
I16 __stdcall WD_AI_ContReadChannelToFile (U16 CardNumber, U16 Channel, U16 BufId,
               U8 *FileName, U32 ReadScans, U32 ScanIntrv, U32 SampIntrv, U16 SyncMode);
I16 __stdcall WD_AI_ContReadMultiChannelsToFile (U16 CardNumber, U16 NumChans, U16 *Chans,
               U16 BufId, U8 *FileName, U32 ReadScans, U32 ScanIntrv, U32 SampIntrv, U16 SyncMode);
I16 __stdcall WD_AI_ContScanChannelsToFile (U16 CardNumber, U16 Channel, U16 BufId,
               U8 *FileName, U32 ReadScans, U32 ScanIntrv, U32 SampIntrv, U16 SyncMode);
I16 __stdcall WD_AI_ContStatus (U16 CardNumber, U32 *Status);
I16 __stdcall WD_AI_ContVScale (U16 wCardNumber, U16 adRange, void *readingArray, F64 *voltageArray, I32 count);
I16 __stdcall WD_AI_ContVScaleEx (U16 wCardNumber, U16 adRange, U16 width, void *readingArray, F64 *voltageArray, I32 count);
I16 __stdcall WD_AI_AsyncCheck (U16 CardNumber, BOOLEAN *Stopped, U32 *AccessCnt);
I16 __stdcall WD_AI_AsyncClear (U16 CardNumber, U32 *StartPos, U32 *AccessCnt);
I16 __stdcall WD_AI_AsyncClearEx (U16 CardNumber, U32 *StartPos, U32 *AccessCnt, U32 NoWait);
I16 __stdcall WD_AI_AsyncDblBufferHalfReady (U16 CardNumber, BOOLEAN *HalfReady, BOOLEAN *StopFlag);
I16 __stdcall WD_AI_AsyncDblBufferMode (U16 CardNumber, BOOLEAN Enable);
I16 __stdcall WD_AI_AsyncDblBufferToFile (U16 CardNumber);
I16 __stdcall WD_AI_ContBufferSetup (U16 wCardNumber, void *pwBuffer, U32 dwReadCount, U16 *BufferId);
I16 __stdcall WD_AI_ContBufferSetup32 (U16 wCardNumber, void *pwBuffer, U32 dwReadCount, U16 *BufferId);
I16 __stdcall WD_AI_ContBufferReset (U16 wCardNumber);
I16 __stdcall WD_AI_DMA_Transfer (U16 wCardNumber, U16 BufId);
I16 __stdcall WD_AI_ConvertCheck (U16 wCardNumber, BOOLEAN *bStopped);
I16 __stdcall WD_AI_AsyncReStartNextReady (U16 wCardNumber, BOOLEAN *bReady, BOOLEAN *StopFlag, U16 *RdyDaqCnt);
I16 __stdcall WD_AI_EventCallBack (U16 wCardNumber, I16 mode, I16 EventType, U32 callbackAddr);
I16 __stdcall WD_AI_DMA_TransferBySize (U16 wCardNumber, F32 timeLimit,  U16 BufId, U32 dwReadCount, U32 *numRead, U32 *dataNotTransferred, U8 *complete);
I16 __stdcall WD_AI_AsyncDblBufferToFileBySize (U16 wCardNumber, U16 bufid, U32 cnt);
I16 __stdcall WD_AI_ContBufferLock (U16 wCardNumber, void *pwBuffer, U32 dwReadCount, U16 *BufferId);
I16 __stdcall WD_AI_SetTimeout (U16 wCardNumber, U32 msec);

I16 __stdcall WD_AI_AsyncDblBufferOverrun (U16 wCardNumber, U16 op, U16 *overrunFlag);
I16 __stdcall WD_AI_AsyncDblBufferHandled (U16 wCardNumber);
I16 __stdcall WD_AI_AsyncReTrigNextReady (U16 wCardNumber, BOOLEAN *bReady, BOOLEAN *StopFlag, U32 *RdyTrigCnt);
/*---------------------------------------------------------------------------*/
//SSI
I16 __stdcall WD_SSI_SourceConn (U16 wCardNumber, U16 sigCode);
I16 __stdcall WD_SSI_SourceDisConn (U16 wCardNumber, U16 sigCode);
I16 __stdcall WD_SSI_SourceClear (U16 wCardNumber);
I16 __stdcall WD_Route_Signal (U16 wCardNumber, U16 signal, U16 Line, U16 dir);
I16 __stdcall WD_Signal_DisConn (U16 wCardNumber, U16 signal, U16 Line);
/*---------------------------------------------------------------------------*/
//calibration
I16 __stdcall WD_AD_Auto_Calibration_ALL(U16 wCardNumber);
I16 __stdcall WD_EEPROM_CAL_Constant_Update(U16 wCardNumber, U16 bank);
I16 __stdcall WD_Load_CAL_Data(U16 wCardNumber, U16 bank);
I16 __stdcall WD_Get_Default_Load_Area (U16 wCardNumber);
I16 __stdcall WD_Set_Default_Load_Area(U16 wCardNumber, U16 bank);
I16 __stdcall WD_AD_Calibration_Action (U16 wCardNumber, U16 calop, U16 srcLoc, U16 destLoc);
/*---------------------------------------------------------------------------*/
//DIO
I16 __stdcall WD_DI_ReadLine (U16 CardNumber, U16 Port, U16 Line, U16 *State);
I16 __stdcall WD_DI_ReadPort (U16 CardNumber, U16 Port, U32 *Value);
/*---------------------------------------------------------------------------*/
I16 __stdcall WD_DO_WriteLine (U16 CardNumber, U16 Port, U16 Line, U16 Value);
I16 __stdcall WD_DO_WritePort (U16 CardNumber, U16 Port, U32 Value);
I16 __stdcall WD_DO_ReadLine (U16 CardNumber, U16 Port, U16 Line, U16 *Value);
I16 __stdcall WD_DO_ReadPort (U16 CardNumber, U16 Port, U32 *Value);
/*---------------------------------------------------------------------------*/
I16 __stdcall WD_DIO_PortConfig (U16 CardNumber, U16 Port, U16 mode, U16 Direction);
I16 __stdcall WD_DIO_LineConfig (U16 CardNumber, U16 Port, U32 Line, U16 Direction);
I16 __stdcall WD_DIO_LinesConfig (U16 CardNumber, U16 Port, U16 mode, U32 Linesdirmap);
/*---------------------------------------------------------------------------*/

#ifdef __cplusplus
}
#endif

#endif		//_WD_DASK_H
