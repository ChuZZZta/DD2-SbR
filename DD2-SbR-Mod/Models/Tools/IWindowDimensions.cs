using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sbr.Models.Tools
{
    public interface IWindowDimensions
    {
        int GetWindowHeight(string processname);
        int GetWindowWidth(string processname);
        Point GetPositionPoint(string processname);
        Point GetLapPoint(string processname);
    }
}
