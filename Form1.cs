using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing.Imaging;

namespace DinozawrBot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MoveMouse();
            //ProverkaColor();
        }

        static Color GetDesktopColor(int x, int y)
        {
            using (
              Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height,
                PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0,
                      Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                }
                return bmp.GetPixel(x, y);
            }
        }

        void MoveMouse()//int scrW, int scrH)
        {
            IntPtr Dinozaur = FindWindow(null, "T-Rex Game. - Opera");
            Color CactusColor = Color.FromArgb(255, 83, 83, 83);

            if (Dinozaur == IntPtr.Zero)
            {
                MessageBox.Show("Minecraft is not running.");
                return;
            }

            SetForegroundWindow(Dinozaur);

            POINT player = new POINT();
            POINT cactus1 = new POINT();

            //Координаты перезагрузки
            player.x = 945;
            player.y = 342;
            //Координаты кактусов
            cactus1.x = 770;
            cactus1.y = 389;



            ClientToScreen(Dinozaur, ref player);

            while (true)
            {
                if (GetDesktopColor(cactus1.x, cactus1.y) == CactusColor)
                {
                    SendKeys.SendWait(" ");
                }

                SetCursorPos(player.x, player.y);
                MouseClickedd(player.x, player.y);

                if (GetAsyncKeyState(shift) != 0)
                {
                    break;
                }
            }


        }

        //void ProverkaColor()
        //{
        //    POINT obj = new POINT();

        //    IntPtr Dinozaur = FindWindow(null, "T-Rex Game. - Opera");

        //    if (Dinozaur == IntPtr.Zero)
        //    {
        //        MessageBox.Show("Minecraft is not running.");
        //        return;
        //    }

        //    SetForegroundWindow(Dinozaur);

        //    obj.x = 733;
        //    obj.y = 389;

        //    Console.WriteLine(GetDesktopColor(obj.x, obj.y));

        //}

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dsFlags, int dx, int dy, int cButtons, int dsExtraInfo);


        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        public const int MOUSEEVENT_LEFTDOWN = 0x02;
        public const int MOUSEEVENT_LEFTUP = 0x04;

       void MouseClickedd(int x, int y)
        {
            mouse_event(MOUSEEVENT_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENT_LEFTUP, x, y, 0, 0);
        }

        public int shift = 0x10;
        public int plus = 0x6B;

        [DllImport("user32.dll")]
        internal static extern short GetAsyncKeyState(int vKey);

        //private void keyPresser()
        //{
        //    int c = 0;
        //    while (true)
        //    {
        //        if (GetAsyncKeyState(shift) != 0)
        //        {
        //            MessageBox.Show("Клавиши нажаты!!!");
        //            Console.WriteLine(c);
        //            break;
        //        }
        //        c++;
        //    }
        //}
    }
}
