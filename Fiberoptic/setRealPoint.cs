using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
namespace FiberopticServer
{
    public partial class setRealPoint : Form
    {
        int setPoint = 0;
        
        public setRealPoint()
        {
            InitializeComponent();
        }

        private void setRealPoint_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            setPoint = Convert.ToInt32(textBox1.Text);
            ServerForm.setPoint = GetPoint();
            ServerForm.setrealpoint.Close();
        }

        public int GetPoint()
        {
            return setPoint;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ServerForm.setrealpoint.Close();
        }
    }
}
