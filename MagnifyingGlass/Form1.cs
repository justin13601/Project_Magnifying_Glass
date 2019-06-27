using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagnifyingGlass
{
    public partial class Form1 : Form
    {

        public int width = Screen.PrimaryScreen.Bounds.Width;
        public int height = Screen.PrimaryScreen.Bounds.Height;


        public int destinationWidth = 350;
        public int destinationHight = 217;

        public bool isStart = true;
        public Bitmap btm = null;
        public Bitmap newbtm = null;


        public int timeValue = 4;

        private Thread t;

        public Form1()
        {

            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Start();
        }
        private void StartDetecting()
        {
            isStart = true;
            t = new Thread(new ThreadStart(Detect));
            t.Start();
        }
        private void Detect()
        {
            while (isStart)
            {
                Thread.Sleep(10);
                this.Invoke(new MyDelegate(DetectAction), isStart);
            }

        }
        delegate void MyDelegate(bool iss);
        private void DetectAction(bool iss)
        {
            pBMenu.Image = null;
            if (btm != null)
                btm.Dispose();
            if (newbtm != null)
                newbtm.Dispose();
            if (iss)
            {
                btm = new Bitmap(destinationWidth, destinationHight);

                using (Graphics g = Graphics.FromImage(btm))
                {
                    g.CopyFromScreen(Control.MousePosition.X - destinationWidth / 4, Control.MousePosition.Y - destinationHight / 4, 0, 0, Screen.AllScreens[0].Bounds.Size);
                    g.Dispose();
                    //pictureBox1.Image = btm;
                }
                int newDW = destinationWidth * timeValue;
                int newDH = destinationHight * timeValue;
                newbtm = new Bitmap(newDW, newDH);
                using (Graphics g2 = Graphics.FromImage(newbtm))
                {
                    g2.DrawImage(btm, 0 - (newDW - destinationWidth) / 4, 0 - (newDH - destinationHight) / 4, newDW, newDH);
                    g2.Dispose();
                    pBMenu.Image = newbtm;
                }

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {

            Exit();

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Start();
        }


        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Start();
        }
        private void Start()
        {
            //Optional because code only runs when program is minimized to tray
            if (this.WindowState == FormWindowState.Minimized)
            {
                //Returns program to normal state
                this.WindowState = FormWindowState.Normal;
            }
            this.Left = 0;
            this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            this.Activate();
            StartDetecting();
        }
        private void Exit()
        {
            isStart = false;
            t.Interrupt();
            Application.ExitThread();
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }

      
    }
}
