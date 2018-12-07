using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbr.Models
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
        public int positionread = 0;
        public int LapNumber { get; set; } = 1;
        public int Position { get; set; } = 1;
        List<int> laptimes = new List<int>();

        public void Update(byte SetLap, bool config, Map map)
        {
            IMemoryRW rw = new MemoryRW(Processname);
            if (config)
            {
                positionread = rw.GetByte(MemoryAddr - 0x6);
                if( rw.GetByte(MemoryAddr) > 1 && LapNumber != SetLap)
                    {
                        LapNumber++;
                        rw.SetByte(MemoryAddr, 1);
                    }
                if(LapNumber == SetLap)
                    {
                        rw.SetByte(MemoryAddr, (byte)map.LapsNumber);
                    }
            }
            else
            {
                LapNumber = rw.GetByte(MemoryAddr);
                positionread = rw.GetByte(MemoryAddr - 0x6);
            }
        }
        public override string ToString()
        {
            return Name+" " + Number + ", lap number: "+LapNumber;
        }

    }
}
