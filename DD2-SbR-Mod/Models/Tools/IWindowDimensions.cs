using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Point = GameOverlay.Drawing.Point;

namespace Sbr.Models.Tools
{
    public interface IWindowDimensions
    {
        int GetWindowHeight(IntPtr ProcessWindow);
        int GetWindowWidth(IntPtr ProcessWindow);
        Point GetPositionPoint(IntPtr ProcessWindow);
        Point GetLapPoint(IntPtr ProcessWindow);
        Point GetInfoPoint(IntPtr ProcessWindow);
    }
}
