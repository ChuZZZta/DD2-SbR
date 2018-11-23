using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Car
    {
        [JsonProperty("Id")]
        public int id { get; set; }
        [JsonProperty("Name")]
        public string name { get; set; }
        [JsonProperty("Numer")]
        public string number { get; set; }
        [JsonProperty("PicturePath")]
        public string picture { get; set; }
        [JsonProperty("MemoryAddr")]
        public int memoryaddr { get; set; }
        public string processname = "";

        public int lapnumber = 0;
        public int position = 1;
        public int[,] times = new int[2, 100];
        public int besttime = 0;
        public static int[] positions = new int[20];

        public void Update(byte SetLap, bool config)
        {
            MemoryRW rw = new MemoryRW(processname);
            if (config)
            {
                rw.SetByte(memoryaddr, SetLap);
                rw.SetByte(memoryaddr-0x6, 1);
            }
            else
            {
                lapnumber = rw.GetByte(memoryaddr);
                position = rw.GetByte(memoryaddr - 0x6);
                positions[position - 1] = id;
            }
        }

    }
}
