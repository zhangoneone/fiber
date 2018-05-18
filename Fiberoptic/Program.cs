using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace FiberopticServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
        //    importdll.CollectionSession cs=new importdll.CollectionSession();
       //     cs = importdll.proxy(ref cs);
      //      importdll.test te = new importdll.test();
       //     te = importdll.proxyx(ref te);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ServerForm ser = new ServerForm();
            Application.Run(ser);
        }
    }
}
