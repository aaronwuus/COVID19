using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static cViP.AA.MouseHelper;

namespace cViP.AA
{
    class Program
    {
        static bool dir = false;
        static KeybdHelper k_hook = new KeybdHelper();
        static System.Timers.Timer timer;
        #region test
        //private const int WH_KEYBOARD_LL = 13;
        //private const int WM_KEYDOWN = 0x0100;
        //private static LowLevelKeyboardProc _proc = HookCallback;
        //private static IntPtr _hookID = IntPtr.Zero;

        //private static IntPtr SetHook(LowLevelKeyboardProc proc)
        //{
        //    using (Process curProcess = Process.GetCurrentProcess())
        //    using (ProcessModule curModule = curProcess.MainModule)
        //    {
        //        return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
        //            GetModuleHandle(curModule.ModuleName), 0);
        //    }
        //}

        //private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        //private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        //{
        //    if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        //    {
        //        int vkCode = Marshal.ReadInt32(lParam);
        //        Console.WriteLine((Keys)vkCode);
        //    }

        //    return CallNextHookEx(_hookID, nCode, wParam, lParam);
        //}

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr GetModuleHandle(string lpModuleName);
        #endregion test
        static void Main(string[] args)
        {
            Console.WriteLine("To Start press shift + alt + S");
            Console.WriteLine("To Pause press shift + alt + C");
            timer = new System.Timers.Timer()
            {
                Enabled = true,
                Interval = 5000,
                AutoReset = true
            };
            timer.Elapsed += _timer_Elapsed;
            KeybdhookProc();
            Application.Run();
            
        }
        private static void KeybdhookProc()
        {
            //安装键盘钩子
            k_hook.KeyDownEvent += new KeyEventHandler(hook_KeyDown);//钩住键按下
            k_hook.Start();//安装键盘钩子
        }
        private static void hook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && (Control.ModifierKeys & Keys.Shift) == Keys.Shift && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                timer.Start();
                Console.WriteLine("You just Started, to pause please press shift + alt + c");
            }
            if (e.KeyValue == (int)Keys.C && (Control.ModifierKeys & Keys.Shift) == Keys.Shift && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                timer.Stop();
                Console.WriteLine("You just Started, to pause please press shift + alt + s");
            }
        }

        private static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            MouseActive();
            dir = !dir;
            //Console.WriteLine("mouse actived, dir is " +dir);
        }

        private static void MouseActive()
        {
            //是否显示鼠标（true、false）
            ShowCursor(false);
            //移动鼠标到屏幕的（0,0）
            mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, 0, 0, 0, 0);
            //鼠标滚轮上滚10，
            if (dir)
                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, 10, 0);
            else
                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -10, 0);
        }
    }
}
