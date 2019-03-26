using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sbr.Models.Tools
{
    class WindowDimensions : IWindowDimensions
    {
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref RectangleStruct rectangle);

        public struct RectangleStruct
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        public int GetWindowHeight(string processname)
        {
            RectangleStruct Window = new RectangleStruct();
            GetWindowRect(Process.GetProcessesByName(processname)[0].MainWindowHandle, ref Window);
            return Math.Abs(Window.Bottom - Window.Top);
        }

        public int GetWindowWidth(string processname)
        {
            RectangleStruct Window = new RectangleStruct();
            GetWindowRect(Process.GetProcessesByName(processname)[0].MainWindowHandle, ref Window);
            return Math.Abs(Window.Right - Window.Left);
        }

        public Point GetLapPoint(string processname)
        {
            Point LapPos = new Point();
            return LapPos;
        }

        public Point GetPositionPoint(string processname)
        {
            Point PositionPos = new Point();

            return PositionPos;
        }
    }
}
