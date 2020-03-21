using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace cViP.AA
{
    class MouseHelper
    {//dwFlags参数定义
        internal const int MOUSEEVENTF_MOVE = 0x0001;     // 移动鼠标 
        internal const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下 
        internal const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起 
        internal const int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下 
        internal const int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起 
        internal const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;// 模拟鼠标中键按下 
        internal const int MOUSEEVENTF_MIDDLEUP = 0x0040; //模拟鼠标中键抬起 
        internal const int MOUSEEVENTF_WHEEL = 0x800;
        internal const int MOUSEEVENTF_ABSOLUTE = 0x8000; //标示是否采用绝对坐标。不采用绝对坐标的话（0,0）表示的是该鼠标当时的位置
        //是鼠标自动移动到某个位置
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        public extern static bool SetCursorPos(int x, int y);
        //获取鼠标当前的位置
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }
        [DllImport("user32.dll")]
        public extern static bool GetCursorPos(out POINT p);
        //是否显示鼠标箭头
        [DllImport("user32.dll")]
        public extern static int ShowCursor(bool bShow);
        //调用系统函数 模拟鼠标事件函数
        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        internal static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

    }
}
