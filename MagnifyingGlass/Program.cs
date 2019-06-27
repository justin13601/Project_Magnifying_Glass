using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagnifyingGlass
{
    static class Program
    {
  
        [STAThread]                 //Calls CoInitializeEx function.
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());    //Runs program without winform.
        }
    }
}
