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
            System.Windows.Forms.Timer ui_reflush;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.数据写文件间隔ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox3 = new System.Windows.Forms.ToolStripComboBox();
            this.异常点记录间隔ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox4 = new System.Windows.Forms.ToolStripComboBox();
            this.k1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.k2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.阈值ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox3 = new System.Windows.Forms.ToolStripComboBox();
            this.异常比ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox4 = new System.Windows.Forms.ToolStripComboBox();
            this.确认比ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox5 = new System.Windows.Forms.ToolStripComboBox();
            this.实时曲线数量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox6 = new System.Windows.Forms.ToolStripComboBox();
            this.采集方式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.异步采集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.回调采集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.同步采集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.历史记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据写文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.异常点记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭写数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止记录异常点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.异常点查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运行日志ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1data写文件 = new System.Windows.Forms.Timer(this.components);
            this.timer2point写文件 = new System.Windows.Forms.Timer(this.components);
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ui_reflush = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ui_reflush
            // 
            ui_reflush.Enabled = true;
            ui_reflush.Interval = 200;
            ui_reflush.Tick += new System.EventHandler(this.ui_reflush_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.启动板卡ToolStripMenuItem,
            this.参数设置ToolStripMenuItem,
            this.采集方式ToolStripMenuItem,
            this.历史记录ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1541, 28);
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
            this.启动板卡ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.启动板卡ToolStripMenuItem.Text = "开始";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.toolStripMenuItem2.Size = new System.Drawing.Size(200, 24);
            this.toolStripMenuItem2.Text = "注册9842";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(200, 24);
            this.toolStripMenuItem3.Text = "注册9852";
            // 
            // 显示实时曲线ToolStripMenuItem
            // 
            this.显示实时曲线ToolStripMenuItem.Name = "显示实时曲线ToolStripMenuItem";
            this.显示实时曲线ToolStripMenuItem.Size = new System.Drawing.Size(200, 24);
            this.显示实时曲线ToolStripMenuItem.Text = "实时曲线";
            this.显示实时曲线ToolStripMenuItem.Click += new System.EventHandler(this.显示实时曲线ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(200, 24);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 参数设置ToolStripMenuItem
            // 
            this.参数设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.采样点数量ToolStripMenuItem,
            this.扫描采样间隔ToolStripMenuItem,
            this.数据写文件间隔ToolStripMenuItem,
            this.异常点记录间隔ToolStripMenuItem,
            this.k1ToolStripMenuItem,
            this.k2ToolStripMenuItem,
            this.阈值ToolStripMenuItem,
            this.异常比ToolStripMenuItem,
            this.确认比ToolStripMenuItem,
            this.实时曲线数量ToolStripMenuItem});
            this.参数设置ToolStripMenuItem.Enabled = false;
            this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
            this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.参数设置ToolStripMenuItem.Text = "参数设置";
            // 
            // 采样点数量ToolStripMenuItem
            // 
            this.采样点数量ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1});
            this.采样点数量ToolStripMenuItem.Name = "采样点数量ToolStripMenuItem";
            this.采样点数量ToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
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
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 28);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // 扫描采样间隔ToolStripMenuItem
            // 
            this.扫描采样间隔ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox2});
            this.扫描采样间隔ToolStripMenuItem.Name = "扫描采样间隔ToolStripMenuItem";
            this.扫描采样间隔ToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.扫描采样间隔ToolStripMenuItem.Text = "扫描/采样间隔";
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(121, 28);
            this.toolStripComboBox2.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox2_SelectedIndexChanged);
            // 
            // 数据写文件间隔ToolStripMenuItem
            // 
            this.数据写文件间隔ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox3});
            this.数据写文件间隔ToolStripMenuItem.Name = "数据写文件间隔ToolStripMenuItem";
            this.数据写文件间隔ToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.数据写文件间隔ToolStripMenuItem.Text = "散射数据写文件时长(s)";
            // 
            // toolStripComboBox3
            // 
            this.toolStripComboBox3.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8",
            "16",
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024"});
            this.toolStripComboBox3.Name = "toolStripComboBox3";
            this.toolStripComboBox3.Size = new System.Drawing.Size(121, 28);
            this.toolStripComboBox3.Tag = "200";
            this.toolStripComboBox3.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox3_SelectedIndexChanged);
            // 
            // 异常点记录间隔ToolStripMenuItem
            // 
            this.异常点记录间隔ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox4});
            this.异常点记录间隔ToolStripMenuItem.Name = "异常点记录间隔ToolStripMenuItem";
            this.异常点记录间隔ToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.异常点记录间隔ToolStripMenuItem.Text = "实时数据写文件时长(s)";
            // 
            // toolStripComboBox4
            // 
            this.toolStripComboBox4.Items.AddRange(new object[] {
            "200",
            "400",
            "600",
            "800",
            "1000",
            "2000",
            "4000",
            "8000"});
            this.toolStripComboBox4.Name = "toolStripComboBox4";
            this.toolStripComboBox4.Size = new System.Drawing.Size(121, 28);
            this.toolStripComboBox4.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox4_SelectedIndexChanged);
            // 
            // k1ToolStripMenuItem
            // 
            this.k1ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1});
            this.k1ToolStripMenuItem.Name = "k1ToolStripMenuItem";
            this.k1ToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.k1ToolStripMenuItem.Text = "K1";
            this.k1ToolStripMenuItem.TextChanged += new System.EventHandler(this.k1ToolStripMenuItem_TextChanged);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Items.AddRange(new object[] {
            "0.5",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(152, 28);
            this.toolStripTextBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripTextBox1_SelectedIndexChanged);
            this.toolStripTextBox1.TextUpdate += new System.EventHandler(this.toolStripTextBox1_TextUpdate);
            // 
            // k2ToolStripMenuItem
            // 
            this.k2ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox2});
            this.k2ToolStripMenuItem.Name = "k2ToolStripMenuItem";
            this.k2ToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.k2ToolStripMenuItem.Text = "K2";
            this.k2ToolStripMenuItem.TextChanged += new System.EventHandler(this.k2ToolStripMenuItem_TextChanged);
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.Items.AddRange(new object[] {
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
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(100, 28);
            this.toolStripTextBox2.SelectedIndexChanged += new System.EventHandler(this.toolStripTextBox2_SelectedIndexChanged);
            this.toolStripTextBox2.TextUpdate += new System.EventHandler(this.toolStripTextBox2_TextUpdate);
            // 
            // 阈值ToolStripMenuItem
            // 
            this.阈值ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox3});
            this.阈值ToolStripMenuItem.Name = "阈值ToolStripMenuItem";
            this.阈值ToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.阈值ToolStripMenuItem.Text = "阈值(+ -)";
            this.阈值ToolStripMenuItem.TextChanged += new System.EventHandler(this.阈值ToolStripMenuItem_TextChanged);
            // 
            // toolStripTextBox3
            // 
            this.toolStripTextBox3.Items.AddRange(new object[] {
            "100",
            "200",
            "300",
            "400",
            "500",
            "600",
            "700",
            "800",
            "900",
            "1000",
            "1100",
            "1200",
            "1300",
            "1400",
            "1500",
            "1600",
            "1700",
            "1800",
            "1900",
            "2000"});
            this.toolStripTextBox3.Name = "toolStripTextBox3";
            this.toolStripTextBox3.Size = new System.Drawing.Size(100, 28);
            this.toolStripTextBox3.SelectedIndexChanged += new System.EventHandler(this.toolStripTextBox3_SelectedIndexChanged);
            this.toolStripTextBox3.TextUpdate += new System.EventHandler(this.toolStripTextBox3_TextUpdate);
            // 
            // 异常比ToolStripMenuItem
            // 
            this.异常比ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox4});
            this.异常比ToolStripMenuItem.Name = "异常比ToolStripMenuItem";
            this.异常比ToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.异常比ToolStripMenuItem.Text = "异常比%";
            this.异常比ToolStripMenuItem.TextChanged += new System.EventHandler(this.异常比ToolStripMenuItem_TextChanged);
            // 
            // toolStripTextBox4
            // 
            this.toolStripTextBox4.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55",
            "60",
            "65",
            "70",
            "75",
            "80",
            "85",
            "90",
            "95",
            "100"});
            this.toolStripTextBox4.Name = "toolStripTextBox4";
            this.toolStripTextBox4.Size = new System.Drawing.Size(100, 28);
            this.toolStripTextBox4.SelectedIndexChanged += new System.EventHandler(this.toolStripTextBox4_SelectedIndexChanged);
            this.toolStripTextBox4.TextUpdate += new System.EventHandler(this.toolStripTextBox4_TextUpdate);
            // 
            // 确认比ToolStripMenuItem
            // 
            this.确认比ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox5});
            this.确认比ToolStripMenuItem.Name = "确认比ToolStripMenuItem";
            this.确认比ToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.确认比ToolStripMenuItem.Text = "确认比%";
            // 
            // toolStripComboBox5
            // 
            this.toolStripComboBox5.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55",
            "60",
            "65",
            "70",
            "75",
            "80",
            "85",
            "90",
            "95",
            "100"});
            this.toolStripComboBox5.Name = "toolStripComboBox5";
            this.toolStripComboBox5.Size = new System.Drawing.Size(121, 28);
            this.toolStripComboBox5.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox5_SelectedIndexChanged);
            this.toolStripComboBox5.TextUpdate += new System.EventHandler(this.toolStripComboBox5_TextUpdate);
            // 
            // 实时曲线数量ToolStripMenuItem
            // 
            this.实时曲线数量ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox6});
            this.实时曲线数量ToolStripMenuItem.Name = "实时曲线数量ToolStripMenuItem";
            this.实时曲线数量ToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
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
            this.toolStripComboBox6.Size = new System.Drawing.Size(121, 28);
            this.toolStripComboBox6.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox6_SelectedIndexChanged);
            this.toolStripComboBox6.TextUpdate += new System.EventHandler(this.toolStripComboBox6_TextUpdate);
            // 
            // 采集方式ToolStripMenuItem
            // 
            this.采集方式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.异步采集ToolStripMenuItem,
            this.回调采集ToolStripMenuItem,
            this.同步采集ToolStripMenuItem});
            this.采集方式ToolStripMenuItem.Enabled = false;
            this.采集方式ToolStripMenuItem.Name = "采集方式ToolStripMenuItem";
            this.采集方式ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.采集方式ToolStripMenuItem.Text = "采集方式";
            // 
            // 异步采集ToolStripMenuItem
            // 
            this.异步采集ToolStripMenuItem.Name = "异步采集ToolStripMenuItem";
            this.异步采集ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.异步采集ToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.异步采集ToolStripMenuItem.Text = "异步采集";
            this.异步采集ToolStripMenuItem.Click += new System.EventHandler(this.异步采集ToolStripMenuItem_Click);
            // 
            // 回调采集ToolStripMenuItem
            // 
            this.回调采集ToolStripMenuItem.Name = "回调采集ToolStripMenuItem";
            this.回调采集ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.回调采集ToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.回调采集ToolStripMenuItem.Text = "回调采集";
            this.回调采集ToolStripMenuItem.Click += new System.EventHandler(this.回调采集ToolStripMenuItem_Click);
            // 
            // 同步采集ToolStripMenuItem
            // 
            this.同步采集ToolStripMenuItem.Name = "同步采集ToolStripMenuItem";
            this.同步采集ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.同步采集ToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.同步采集ToolStripMenuItem.Text = "同步采集";
            // 
            // 历史记录ToolStripMenuItem
            // 
            this.历史记录ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据写文件ToolStripMenuItem,
            this.异常点记录ToolStripMenuItem,
            this.关闭写数据ToolStripMenuItem,
            this.停止记录异常点ToolStripMenuItem,
            this.数据查看ToolStripMenuItem,
            this.异常点查看ToolStripMenuItem,
            this.运行日志ToolStripMenuItem1});
            this.历史记录ToolStripMenuItem.Name = "历史记录ToolStripMenuItem";
            this.历史记录ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.历史记录ToolStripMenuItem.Text = "记录";
            this.历史记录ToolStripMenuItem.Click += new System.EventHandler(this.历史记录ToolStripMenuItem_Click);
            // 
            // 数据写文件ToolStripMenuItem
            // 
            this.数据写文件ToolStripMenuItem.Name = "数据写文件ToolStripMenuItem";
            this.数据写文件ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.数据写文件ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.数据写文件ToolStripMenuItem.Text = "数据写文件";
            this.数据写文件ToolStripMenuItem.Click += new System.EventHandler(this.数据写文件ToolStripMenuItem_Click);
            // 
            // 异常点记录ToolStripMenuItem
            // 
            this.异常点记录ToolStripMenuItem.Name = "异常点记录ToolStripMenuItem";
            this.异常点记录ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.异常点记录ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.异常点记录ToolStripMenuItem.Text = "异常点记录";
            this.异常点记录ToolStripMenuItem.Click += new System.EventHandler(this.异常点记录ToolStripMenuItem_Click);
            // 
            // 关闭写数据ToolStripMenuItem
            // 
            this.关闭写数据ToolStripMenuItem.Enabled = false;
            this.关闭写数据ToolStripMenuItem.Name = "关闭写数据ToolStripMenuItem";
            this.关闭写数据ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.关闭写数据ToolStripMenuItem.Text = "关闭写数据";
            this.关闭写数据ToolStripMenuItem.Click += new System.EventHandler(this.关闭写数据ToolStripMenuItem_Click);
            // 
            // 停止记录异常点ToolStripMenuItem
            // 
            this.停止记录异常点ToolStripMenuItem.Enabled = false;
            this.停止记录异常点ToolStripMenuItem.Name = "停止记录异常点ToolStripMenuItem";
            this.停止记录异常点ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.停止记录异常点ToolStripMenuItem.Text = "停止记录异常点";
            this.停止记录异常点ToolStripMenuItem.Click += new System.EventHandler(this.停止记录异常点ToolStripMenuItem_Click);
            // 
            // 数据查看ToolStripMenuItem
            // 
            this.数据查看ToolStripMenuItem.Name = "数据查看ToolStripMenuItem";
            this.数据查看ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.数据查看ToolStripMenuItem.Text = "数据查看";
            this.数据查看ToolStripMenuItem.Click += new System.EventHandler(this.数据查看ToolStripMenuItem_Click);
            // 
            // 异常点查看ToolStripMenuItem
            // 
            this.异常点查看ToolStripMenuItem.Name = "异常点查看ToolStripMenuItem";
            this.异常点查看ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.异常点查看ToolStripMenuItem.Text = "异常点查看";
            this.异常点查看ToolStripMenuItem.Click += new System.EventHandler(this.异常点查看ToolStripMenuItem_Click);
            // 
            // 运行日志ToolStripMenuItem1
            // 
            this.运行日志ToolStripMenuItem1.Name = "运行日志ToolStripMenuItem1";
            this.运行日志ToolStripMenuItem1.Size = new System.Drawing.Size(210, 24);
            this.运行日志ToolStripMenuItem1.Text = "运行日志";
            this.运行日志ToolStripMenuItem1.Click += new System.EventHandler(this.运行日志ToolStripMenuItem1_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.帮助ToolStripMenuItem});
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.帮助ToolStripMenuItem.Text = "帮助";
            this.帮助ToolStripMenuItem.Click += new System.EventHandler(this.帮助ToolStripMenuItem_Click);
            // 
            // timer1data写文件
            // 
            this.timer1data写文件.Interval = 1000;
            this.timer1data写文件.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2point写文件
            // 
            this.timer2point写文件.Interval = 1000;
            this.timer2point写文件.Tick += new System.EventHandler(this.timer2point写文件_Tick);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.AutoSize = true;
            this.zedGraphControl1.Location = new System.Drawing.Point(0, 31);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(730, 432);
            this.zedGraphControl1.TabIndex = 9;
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // zedGraphControl2
            // 
            this.zedGraphControl2.AutoScroll = true;
            this.zedGraphControl2.AutoSize = true;
            this.zedGraphControl2.IsAutoScrollRange = true;
            this.zedGraphControl2.Location = new System.Drawing.Point(739, 31);
            this.zedGraphControl2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.zedGraphControl2.Name = "zedGraphControl2";
            this.zedGraphControl2.ScrollGrace = 0D;
            this.zedGraphControl2.ScrollMaxX = 20000D;
            this.zedGraphControl2.ScrollMaxY = 900D;
            this.zedGraphControl2.ScrollMaxY2 = 1800D;
            this.zedGraphControl2.ScrollMinX = 0D;
            this.zedGraphControl2.ScrollMinY = -1800D;
            this.zedGraphControl2.ScrollMinY2 = -900D;
            this.zedGraphControl2.Size = new System.Drawing.Size(788, 432);
            this.zedGraphControl2.TabIndex = 10;
            this.zedGraphControl2.Load += new System.EventHandler(this.zedGraphControl2_Load);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 472);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Red;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1527, 266);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1379, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 27);
            this.label1.TabIndex = 12;
            this.label1.Text = "0.00 us";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(883, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 27);
            this.label2.TabIndex = 13;
            this.label2.Text = "0.00 ms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(763, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 27);
            this.label3.TabIndex = 14;
            this.label3.Text = "UI刷新时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1277, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 27);
            this.label4.TabIndex = 15;
            this.label4.Text = "采集周期";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1277, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 27);
            this.label5.TabIndex = 16;
            this.label5.Text = "采集个数";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1389, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 27);
            this.label6.TabIndex = 17;
            this.label6.Text = "0";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1541, 739);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.zedGraphControl2);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "ServerForm";
            this.Tag = "";
            this.Text = "光纤数据采集";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem 异步采集ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 回调采集ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 采样点数量ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripMenuItem 扫描采样间隔ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox2;
        private System.Windows.Forms.ToolStripMenuItem 数据写文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 异常点记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 数据查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 异常点查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 运行日志ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 数据写文件间隔ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox3;
        private System.Windows.Forms.ToolStripMenuItem 异常点记录间隔ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox4;
        private System.Windows.Forms.Timer timer1data写文件;
        private System.Windows.Forms.Timer timer2point写文件;
        private System.Windows.Forms.ToolStripMenuItem 关闭写数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止记录异常点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 同步采集ToolStripMenuItem;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private ZedGraph.ZedGraphControl zedGraphControl2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示实时曲线ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem k1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem k2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 阈值ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 异常比ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripComboBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripComboBox toolStripTextBox3;
        private System.Windows.Forms.ToolStripComboBox toolStripTextBox4;
        private System.Windows.Forms.ToolStripMenuItem 确认比ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox5;
        private System.Windows.Forms.ToolStripMenuItem 实时曲线数量ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox6;
    }
}

