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
        //json data
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Numer")]
        public string Number { get; set; }
        [JsonProperty("PicturePath")]
        public string Picture { get; set; }
        [JsonProperty("RaceMemoryAddress")]
        public int RaceMemoryAddress { get; set; }
        [JsonProperty("ChempionshipMemoryAddress")]
        public int ChempionshipMemoryAddress { get; set; }
        
        public string Processname { get; set; }
        public int LapNumber { get; set; } = 1;
        public int Position { get; set; } = 1;
        public int Distance { get; set; } = 1;

        //variables
        public int positionread = 0;


        /* Addreses in memmory:
         *  RaceMemoryAddress          lap count
         *  RaceMemoryAddress - 0x6    postion
         *  RaceMemoryAddress + 0x2    lap distance
         * 
         */


        public void Update(byte SetLap, bool config, Map map)
        {
            IMemoryRW rw = new MemoryRW(Processname);
            positionread = rw.GetByte(RaceMemoryAddress - 0x6);
            Distance = rw.GetByte(RaceMemoryAddress + 0x2);
            if (config)
            {
                if( rw.GetByte(RaceMemoryAddress) > 1 && LapNumber != SetLap)
                    {
                        LapNumber++;
                        rw.SetByte(RaceMemoryAddress, 1);
                    }
                if(LapNumber == SetLap)
                    {
                        rw.SetByte(RaceMemoryAddress, (byte)map.LapsNumber);
                    }
            }
            else
            {
                LapNumber = rw.GetByte(RaceMemoryAddress);
            }
        }
        public override string ToString()
        {
            return Name+" " + Number + ", lap number: "+LapNumber;
        }

    }
}
