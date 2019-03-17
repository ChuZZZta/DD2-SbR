using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbr.Models
{
    public class ModConfig
    {
        //lapmode
        public bool lapModConfig = false;
        public byte lapLimit = 0;
        public Map map;

        public ModConfig(byte lapLimit, bool lapModConfig, Map map)
        {
            this.lapLimit = lapLimit;
            this.lapModConfig = lapModConfig;
            this.map = map;
        }
    }
}
