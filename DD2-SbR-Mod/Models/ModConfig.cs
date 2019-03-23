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
        public bool eliminateModConfig = false;
        public byte eliminateSec = 10;
        public bool surpriseModConfig = false;
        public byte surpriseSec = 10;

        public ModConfig(bool lapModConfig, byte lapLimit, List<Map> MapList, bool eliminateModConfig, byte eliminateSec, bool surpriseModConfig, byte surpriseSec)
        {
            this.lapLimit = lapLimit;
            this.lapModConfig = lapModConfig;
            this.MapList = MapList;

            this.eliminateModConfig = eliminateModConfig;
            this.eliminateSec = eliminateSec;

            this.surpriseModConfig = surpriseModConfig;
            this.surpriseSec = surpriseSec;
        }
    }
}
