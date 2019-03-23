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
        public List<Map> MapList = new List<Map>();

        public ModConfig(bool lapModConfig, byte lapLimit, List<Map> MapList)
        {
            this.lapLimit = lapLimit;
            this.lapModConfig = lapModConfig;
            this.MapList = MapList;
        }
    }
}
