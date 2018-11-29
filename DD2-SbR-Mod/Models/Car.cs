using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbr
{
    public class Car
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Numer")]
        public string Number { get; set; }
        [JsonProperty("PicturePath")]
        public string Picture { get; set; }
        [JsonProperty("MemoryAddr")]
        public int MemoryAddr { get; set; }
        public string Processname { get => processname; set => processname = value; }
        private string processname = "";

        public int lapnumber = 0;
        public int position = 1;
        List<int> laptimes = new List<int>();

        public void Update(byte SetLap, bool config, Map map)
        {
            IMemoryRW rw = new MemoryRW(Processname);
            if (config)
            {
                position = rw.GetByte(MemoryAddr - 0x6);
                if( rw.GetByte(MemoryAddr) > 1 && lapnumber != SetLap)
                    {
                        lapnumber++;
                        rw.SetByte(MemoryAddr, 1);
                    }
                if(lapnumber == SetLap)
                    {
                        rw.SetByte(MemoryAddr, (byte)map.LapsNumber);
                    }
            }
            else
            {
                lapnumber = rw.GetByte(MemoryAddr);
                position = rw.GetByte(MemoryAddr - 0x6);
            }
        }

    }
}
