﻿# Fiberoptic
基于凌华PCIE9842采集卡的光纤震动检测软件

本软件使用C#和C++混合编程。由于采集的数据量大，数据处理算法运算量大，对实时性要求高(1ms内完成一次采集处理显示流程),所以采集和处理算法，使用C++编写，并封装成DLL。C#调用C++DLL，负责界面展示和用户交互。
快速使用:
1。软件基于VS2010的环境编写，禁用编译器优化选项，可以避免一些BUG：
(1).预警算法里面奇怪的BUG,导致预警算法无法使用。
(2).线程同步时产生的BUG导致周期性出现错误数据。

2。快捷键CTRL+F，在整个解决方案中:DEBUG 0 替换为 DEBUG 1。其中DEBUG 1代表使用模拟调试环境，不从凌华PCIE9842采集卡获取数据。

3。重新编译解决方案，将DEBUG目录下生成的文件，全部拷贝到启动工程的DEBUG目录下。即可启动软件。

其他：
(1).软件使用环形帧缓冲池和读写锁确保采集卡数据不会丢失。整个解决方案中替换Max_Num宏，改变帧缓冲池的长度。
(2).调用windows API和C#方法，开辟多线程的运行环境。
(3).帧缓冲池的同步锁，使用这样的机制:线程向帧缓冲池注册user，传入线程编号和user数量，返回线程在帧缓冲池的句柄，用来请求帧的读写锁。
(4).Timer类用来测量代码段运行耗时。
(5).使用循环消息队列，作为报警点的信息缓冲，等待上层获取。
(6).编写了中间件，方便C#和C++数据的交互。
(7).使用了Zedgraph开源项目，绘制坐标和演示数据。报警信息使用DataGridView管理