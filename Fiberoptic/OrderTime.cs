using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FiberopticServer
{
    public partial class OrderTime : Form
    {
        string startTime = "";
        string stopTime = "";
        string s = "";//标志构造类的预约任务类型
        public OrderTime(string s)
        {
            InitializeComponent();
            this.s = s;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           startTime = dateTimePicker1.Value.ToString();
           stopTime = dateTimePicker2.Value.ToString();
           if (s == "散射采集")
           {
               button1.BeginInvoke(Program.lgin.ser.TimeSetEnd0, null);
           }
           else if (s == "实时采集")
           {
               button1.BeginInvoke(Program.lgin.ser.TimeSetEnd1, null);
           }
        }
        public string RetStartTime()
        {
            return startTime;
        }
        public string RetStopTime()
        {
            return stopTime;
        }
    }
}
