using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbr
{
    public interface IMemoryRW
    {
        void SetByte(int offset, byte value);
        int GetByte(int offset);
    }
}
