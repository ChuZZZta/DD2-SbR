using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Point = GameOverlay.Drawing.Point;

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

        public int GetWindowHeight(IntPtr ProcessWindow)
        {
            RectangleStruct Window = new RectangleStruct();
            GetWindowRect(ProcessWindow, ref Window);
            return Math.Abs(Window.Bottom - Window.Top);
        }

        public int GetWindowWidth(IntPtr ProcessWindow)
        {
            RectangleStruct Window = new RectangleStruct();
            GetWindowRect(ProcessWindow, ref Window);
            return Math.Abs(Window.Right - Window.Left);
        }

        public Point GetLapPoint(IntPtr ProcessWindow)
        {
            Point LapPos = new Point(Convert.ToInt32(GetWindowHeight(ProcessWindow) * 0.19), Convert.ToInt32(GetWindowWidth(ProcessWindow) * 0.63));
            return LapPos;
        }

        public Point GetPositionPoint(IntPtr ProcessWindow)
        {
            Point PositionPos = new Point(Convert.ToInt32(GetWindowHeight(ProcessWindow) * 0.12), Convert.ToInt32(GetWindowWidth(ProcessWindow) * 0.45));

            return PositionPos;
        }
        public Point GetInfoPoint(IntPtr ProcessWindow)
        {
            Point InfoPos = new Point(Convert.ToInt32(GetWindowHeight(ProcessWindow) * 0.91), Convert.ToInt32(GetWindowWidth(ProcessWindow) * 0.10));
            return InfoPos;
        }
    }
}
