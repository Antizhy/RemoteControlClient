using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RemoteRunClient
{
    public class Win32Utils
    {
        [DllImport("User32.dll",EntryPoint = "SetCursorPos")]
        private static extern bool SetCursorPos(int x, int y);

        public struct POINT
        {
            public int X;
            public int Y;
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint); //获取光标所在的坐标

        public static bool SetPos(int x, int y)
        {
            return SetCursorPos(x, y);
        }

        public static POINT GetPos()
        {
            GetCursorPos(out POINT lpP);
            return lpP;
        }

        [DllImport("user32.dll", EntryPoint = "ShowCursor", CharSet = CharSet.Auto)]
        private extern static void ShowCursor(int status);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);
        private static IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static void SetWindowFixed(IntPtr hWnd)
        {
            SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 3);
        }

        public static void ShowCur()
        {
            ShowCursor(1);
        }

        public static void HideCur()
        {
            ShowCursor(0);
        }

    }
}
