namespace FiberopticServer
{
    partial class ServerForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ui_reflush = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.启动板卡ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.显示实时曲线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.采样点数量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.扫描采样间隔ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.实时曲线数量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox6 = new System.Windows.Forms.ToolStripComboBox();
            this.采集方式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.k1ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox7 = new System.Windows.Forms.ToolStripComboBox();
            this.k2ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox8 = new System.Windows.Forms.ToolStripComboBox();
            this.放大开关ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.报警设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分区报警设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.报警开关ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.历史记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.后向散射曲线记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.实时震动曲线记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.丢帧率记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.格式转换工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运行日志ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.软件版本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.散射数据预约写文件 = new System.Windows.Forms.Timer(this.components);
            this.实时数据预约写文件 = new System.Windows.Forms.Timer(this.components);
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ui_reflush
            // 
            this.ui_reflush.Enabled = true;
            this.ui_reflush.Interval = 200;
            this.ui_reflush.Tick += new System.EventHandler(this.ui_reflush_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.启动板卡ToolStripMenuItem,
            this.参数设置ToolStripMenuItem,
            this.采集方式ToolStripMenuItem,
            this.报警设置ToolStripMenuItem,
            this.历史记录ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1524, 25);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 启动板卡ToolStripMenuItem
            // 
            this.启动板卡ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.显示实时曲线ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.启动板卡ToolStripMenuItem.Name = "启动板卡ToolStripMenuItem";
            this.启动板卡ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.启动板卡ToolStripMenuItem.Text = "开始";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.toolStripMenuItem2.Size = new System.Drawing.Size(145, 22);
            this.toolStripMenuItem2.Text = "运行";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(145, 22);
            this.toolStripMenuItem3.Text = "暂停";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // 显示实时曲线ToolStripMenuItem
            // 
            this.显示实时曲线ToolStripMenuItem.Name = "显示实时曲线ToolStripMenuItem";
            this.显示实时曲线ToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.显示实时曲线ToolStripMenuItem.Text = "实时曲线";
            this.显示实时曲线ToolStripMenuItem.Click += new System.EventHandler(this.显示实时曲线ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 参数设置ToolStripMenuItem
            // 
            this.参数设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.采样点数量ToolStripMenuItem,
            this.扫描采样间隔ToolStripMenuItem,
            this.实时曲线数量ToolStripMenuItem});
            this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
            this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.参数设置ToolStripMenuItem.Text = "参数设置";
            // 
            // 采样点数量ToolStripMenuItem
            // 
            this.采样点数量ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1});
            this.采样点数量ToolStripMenuItem.Name = "采样点数量ToolStripMenuItem";
            this.采样点数量ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.采样点数量ToolStripMenuItem.Text = "采样点数量";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "8000",
            "10000",
            "12000",
            "14000",
            "16000",
            "18000",
            "20000",
            "22000",
            "24000",
            "26000",
            "28000",
            "30000"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // 扫描采样间隔ToolStripMenuItem
            // 
            this.扫描采样间隔ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox2});
            this.扫描采样间隔ToolStripMenuItem.Name = "扫描采样间隔ToolStripMenuItem";
            this.扫描采样间隔ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.扫描采样间隔ToolStripMenuItem.Text = "扫描/采样间隔";
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.Items.AddRange(new object[] {
            "2",
            "4",
            "6",
            "8",
            "10",
            "12",
            "14",
            "16",
            "18",
            "20"});
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox2.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox2_SelectedIndexChanged);
            this.toolStripComboBox2.TextChanged += new System.EventHandler(this.toolStripComboBox2_TextChanged);
            // 
            // 实时曲线数量ToolStripMenuItem
            // 
            this.实时曲线数量ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox6});
            this.实时曲线数量ToolStripMenuItem.Name = "实时曲线数量ToolStripMenuItem";
            this.实时曲线数量ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.实时曲线数量ToolStripMenuItem.Text = "实时曲线数量";
            // 
            // toolStripComboBox6
            // 
            this.toolStripComboBox6.Items.AddRange(new object[] {
            "1000",
            "2000",
            "3000",
            "4000",
            "5000",
            "6000",
            "7000",
            "8000",
            "9000",
            "10000"});
            this.toolStripComboBox6.Name = "toolStripComboBox6";
            this.toolStripComboBox6.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox6.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox6_SelectedIndexChanged);
            this.toolStripComboBox6.TextUpdate += new System.EventHandler(this.toolStripComboBox6_TextUpdate);
            // 
            // 采集方式ToolStripMenuItem
            // 
            this.采集方式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.k1ToolStripMenuItem1,
            this.k2ToolStripMenuItem1,
            this.放大开关ToolStripMenuItem});
            this.采集方式ToolStripMenuItem.Name = "采集方式ToolStripMenuItem";
            this.采集方式ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.采集方式ToolStripMenuItem.Text = "信号放大";
            // 
            // k1ToolStripMenuItem1
            // 
            this.k1ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox7});
            this.k1ToolStripMenuItem1.Name = "k1ToolStripMenuItem1";
            this.k1ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.k1ToolStripMenuItem1.Text = "K1";
            // 
            // toolStripComboBox7
            // 
            this.toolStripComboBox7.Items.AddRange(new object[] {
            "0.5",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.toolStripComboBox7.Name = "toolStripComboBox7";
            this.toolStripComboBox7.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox7.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox7_SelectedIndexChanged);
            this.toolStripComboBox7.TextUpdate += new System.EventHandler(this.toolStripComboBox7_TextUpdate);
            // 
            // k2ToolStripMenuItem1
            // 
            this.k2ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox8});
            this.k2ToolStripMenuItem1.Name = "k2ToolStripMenuItem1";
            this.k2ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.k2ToolStripMenuItem1.Text = "K2";
            // 
            // toolStripComboBox8
            // 
            this.toolStripComboBox8.Items.AddRange(new object[] {
            "0.000005",
            "0.000010",
            "0.000015",
            "0.000020",
            "0.000025",
            "0.000030",
            "0.000035",
            "0.000040",
            "0.000045",
            "0.000050",
            "0.000055",
            "0.000060",
            "0.000065",
            "0.000070",
            "0.000075",
            "0.000080",
            "0.000085",
            "0.000090",
            "0.000095",
            "0.000100"});
            this.toolStripComboBox8.Name = "toolStripComboBox8";
            this.toolStripComboBox8.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox8.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox8_SelectedIndexChanged);
            this.toolStripComboBox8.TextUpdate += new System.EventHandler(this.toolStripComboBox8_TextUpdate);
            // 
            // 放大开关ToolStripMenuItem
            // 
            this.放大开关ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripMenuItem1,
            this.toolStripMenuItem4});
            this.放大开关ToolStripMenuItem.Name = "放大开关ToolStripMenuItem";
            this.放大开关ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.放大开关ToolStripMenuItem.Text = "放大开关";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(85, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(88, 22);
            this.toolStripMenuItem1.Text = "开";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(88, 22);
            this.toolStripMenuItem4.Text = "关";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // 报警设置ToolStripMenuItem
            // 
            this.报警设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.分区报警设置ToolStripMenuItem,
            this.报警开关ToolStripMenuItem});
            this.报警设置ToolStripMenuItem.Name = "报警设置ToolStripMenuItem";
            this.报警设置ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.报警设置ToolStripMenuItem.Text = "报警设置";
            // 
            // 分区报警设置ToolStripMenuItem
            // 
            this.分区报警设置ToolStripMenuItem.Name = "分区报警设置ToolStripMenuItem";
            this.分区报警设置ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.分区报警设置ToolStripMenuItem.Text = "分区报警设置";
            this.分区报警设置ToolStripMenuItem.Click += new System.EventHandler(this.分区报警设置ToolStripMenuItem_Click);
            // 
            // 报警开关ToolStripMenuItem
            // 
            this.报警开关ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.报警开关ToolStripMenuItem.Name = "报警开关ToolStripMenuItem";
            this.报警开关ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.报警开关ToolStripMenuItem.Text = "报警开关";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(88, 22);
            this.toolStripMenuItem5.Text = "开";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(88, 22);
            this.toolStripMenuItem6.Text = "关";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // 历史记录ToolStripMenuItem
            // 
            this.历史记录ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.后向散射曲线记录ToolStripMenuItem,
            this.实时震动曲线记录ToolStripMenuItem,
            this.丢帧率记录ToolStripMenuItem,
            this.格式转换工具ToolStripMenuItem,
            this.运行日志ToolStripMenuItem1});
            this.历史记录ToolStripMenuItem.Name = "历史记录ToolStripMenuItem";
            this.历史记录ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.历史记录ToolStripMenuItem.Text = "数据记录";
            // 
            // 后向散射曲线记录ToolStripMenuItem
            // 
            this.后向散射曲线记录ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem13});
            this.后向散射曲线记录ToolStripMenuItem.Name = "后向散射曲线记录ToolStripMenuItem";
            this.后向散射曲线记录ToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.后向散射曲线记录ToolStripMenuItem.Text = "后向散射曲线记录";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem7.Text = "开";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem8.Text = "关";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.toolStripMenuItem8_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem13.Text = "预约采集";
            this.toolStripMenuItem13.Click += new System.EventHandler(this.toolStripMenuItem13_Click);
            // 
            // 实时震动曲线记录ToolStripMenuItem
            // 
            this.实时震动曲线记录ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem9,
            this.toolStripMenuItem10,
            this.toolStripMenuItem14});
            this.实时震动曲线记录ToolStripMenuItem.Name = "实时震动曲线记录ToolStripMenuItem";
            this.实时震动曲线记录ToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.实时震动曲线记录ToolStripMenuItem.Text = "实时震动曲线记录";
            this.实时震动曲线记录ToolStripMenuItem.Click += new System.EventHandler(this.实时震动曲线记录ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem9.Text = "开";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.toolStripMenuItem9_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem10.Text = "关";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.toolStripMenuItem10_Click);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem14.Text = "预约采集";
            this.toolStripMenuItem14.Click += new System.EventHandler(this.toolStripMenuItem14_Click);
            // 
            // 丢帧率记录ToolStripMenuItem
            // 
            this.丢帧率记录ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem11,
            this.toolStripMenuItem12});
            this.丢帧率记录ToolStripMenuItem.Name = "丢帧率记录ToolStripMenuItem";
            this.丢帧率记录ToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.丢帧率记录ToolStripMenuItem.Text = "报警信息/丢帧率记录";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(88, 22);
            this.toolStripMenuItem11.Text = "开";
            this.toolStripMenuItem11.Click += new System.EventHandler(this.toolStripMenuItem11_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(88, 22);
            this.toolStripMenuItem12.Text = "关";
            this.toolStripMenuItem12.Click += new System.EventHandler(this.toolStripMenuItem12_Click);
            // 
            // 格式转换工具ToolStripMenuItem
            // 
            this.格式转换工具ToolStripMenuItem.Name = "格式转换工具ToolStripMenuItem";
            this.格式转换工具ToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.格式转换工具ToolStripMenuItem.Text = "格式转换工具";
            this.格式转换工具ToolStripMenuItem.Click += new System.EventHandler(this.格式转换工具ToolStripMenuItem_Click);
            // 
            // 运行日志ToolStripMenuItem1
            // 
            this.运行日志ToolStripMenuItem1.Name = "运行日志ToolStripMenuItem1";
            this.运行日志ToolStripMenuItem1.Size = new System.Drawing.Size(189, 22);
            this.运行日志ToolStripMenuItem1.Text = "运行日志";
            this.运行日志ToolStripMenuItem1.Click += new System.EventHandler(this.运行日志ToolStripMenuItem1_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.软件版本ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // 软件版本ToolStripMenuItem
            // 
            this.软件版本ToolStripMenuItem.Name = "软件版本ToolStripMenuItem";
            this.软件版本ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.软件版本ToolStripMenuItem.Text = "软件版本";
            this.软件版本ToolStripMenuItem.Click += new System.EventHandler(this.软件版本ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.帮助ToolStripMenuItem.Text = "帮助文档";
            this.帮助ToolStripMenuItem.Click += new System.EventHandler(this.帮助ToolStripMenuItem_Click);
            // 
            // 散射数据预约写文件
            // 
            this.散射数据预约写文件.Interval = 1000;
            this.散射数据预约写文件.Tick += new System.EventHandler(this.散射数据预约写文件_Tick);
            // 
            // 实时数据预约写文件
            // 
            this.实时数据预约写文件.Interval = 1000;
            this.实时数据预约写文件.Tick += new System.EventHandler(this.实时数据预约写文件_Tick);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControl1.AutoSize = true;
            this.zedGraphControl1.Location = new System.Drawing.Point(5, 49);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(737, 395);
            this.zedGraphControl1.TabIndex = 9;
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // zedGraphControl2
            // 
            this.zedGraphControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControl2.AutoScroll = true;
            this.zedGraphControl2.AutoSize = true;
            this.zedGraphControl2.IsAutoScrollRange = true;
            this.zedGraphControl2.Location = new System.Drawing.Point(5, 49);
            this.zedGraphControl2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.zedGraphControl2.Name = "zedGraphControl2";
            this.zedGraphControl2.ScrollGrace = 0D;
            this.zedGraphControl2.ScrollMaxX = 20000D;
            this.zedGraphControl2.ScrollMaxY = 900D;
            this.zedGraphControl2.ScrollMaxY2 = 1800D;
            this.zedGraphControl2.ScrollMinX = 0D;
            this.zedGraphControl2.ScrollMinY = -1800D;
            this.zedGraphControl2.ScrollMinY2 = -900D;
            this.zedGraphControl2.Size = new System.Drawing.Size(743, 395);
            this.zedGraphControl2.TabIndex = 10;
            this.zedGraphControl2.Load += new System.EventHandler(this.zedGraphControl2_Load);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(590, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 22);
            this.label1.TabIndex = 12;
            this.label1.Text = "0.00 us";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 22);
            this.label2.TabIndex = 13;
            this.label2.Text = "0.00 ms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 22);
            this.label3.TabIndex = 14;
            this.label3.Text = "UI刷新时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(510, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 22);
            this.label4.TabIndex = 15;
            this.label4.Text = "采集周期";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(510, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 22);
            this.label5.TabIndex = 16;
            this.label5.Text = "采集个数";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(601, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 22);
            this.label6.TabIndex = 17;
            this.label6.Text = "0";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1524, 25);
            this.toolStrip1.TabIndex = 18;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.ForeColor = System.Drawing.Color.DarkTurquoise;
            this.button1.Location = new System.Drawing.Point(659, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 33);
            this.button1.TabIndex = 19;
            this.button1.Text = "ON/OFF";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.ForeColor = System.Drawing.Color.DarkTurquoise;
            this.button2.Location = new System.Drawing.Point(665, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 33);
            this.button2.TabIndex = 20;
            this.button2.Text = "ON/OFF";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 506);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Red;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1518, 271);
            this.dataGridView1.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.zedGraphControl1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(12, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(747, 447);
            this.panel1.TabIndex = 21;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.zedGraphControl2);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(765, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(753, 447);
            this.panel2.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label7.Location = new System.Drawing.Point(641, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(303, 28);
            this.label7.TabIndex = 23;
            this.label7.Text = "分 布 式 光 纤 振 动 传 感 系 统";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem15,
            this.toolStripMenuItem16});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem15.Text = "复位报警";
            this.toolStripMenuItem15.Click += new System.EventHandler(this.toolStripMenuItem15_Click);
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem16.Text = "取消";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1524, 776);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "ServerForm";
            this.Tag = "";
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.ResizeEnd += new System.EventHandler(this.ServerForm_ResizeEnd);
            this.Resize += new System.EventHandler(this.ServerForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 启动板卡ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 参数设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 采集方式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 历史记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 采样点数量ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripMenuItem 扫描采样间隔ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox2;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 运行日志ToolStripMenuItem1;
        private System.Windows.Forms.Timer 散射数据预约写文件;
        private System.Windows.Forms.Timer 实时数据预约写文件;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private ZedGraph.ZedGraphControl zedGraphControl2;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示实时曲线ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem 实时曲线数量ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox6;
        private System.Windows.Forms.ToolStripMenuItem k1ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox7;
        private System.Windows.Forms.ToolStripMenuItem k2ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox8;
        private System.Windows.Forms.ToolStripMenuItem 放大开关ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem 报警设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分区报警设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报警开关ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem 后向散射曲线记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 实时震动曲线记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 丢帧率记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem 软件版本ToolStripMenuItem;
        private System.Windows.Forms.Timer ui_reflush;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem 格式转换工具ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem15;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem16;
    }
}

