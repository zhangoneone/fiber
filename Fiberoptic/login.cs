using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace FiberopticServer
{
    public class login
    {
       public ServerForm ser;
       public login()
        {

        }
        public void run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ser = new ServerForm();
            Application.Run(ser);
        }
    }
}
