using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbr.Models.Tools
{
    public interface IMemoryRW
    {
        void SetByte(int offset, int value);
        int GetByte(int offset);
    }
}
