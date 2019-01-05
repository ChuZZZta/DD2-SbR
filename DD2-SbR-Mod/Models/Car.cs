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

        public static bool isNewSeason = false;

        private static List<int> ScorePoints = new List<int>() { 999,100,75,50,40,35,30,25,20,15,10,9,8,7,6,5,4,3,2,1,0};

        //memories variables
        public int LapNumber { get; set; } = 1;
        public int Distance { get; set; } = 1;
        public int PositionRead { get; set; } = 1;
        public int Position { get; set; } = 1;
        public int TotalChempScore { get; set; } = 1;
        public int PrevChempScore { get; set; } = 1;
        public int CurrChempScore { get; set; } = 1;
        public int FutureChempScore { get; set; } = 1;

        /* Addreses in memmory:
         *  RaceMemoryAddress                lap count
         *  RaceMemoryAddress - 0x6          postion
         *  RaceMemoryAddress + 0x2          lap distance
         *  ChempionshipMemoryAddress        total chemp score
         *  ChempionshipMemoryAddress + 0xC  prev chemp score
         */


        public void Update(byte SetLap, bool config, Map map)
        {
            IMemoryRW rw = new MemoryRW(Processname);
            PositionRead = rw.GetByte(RaceMemoryAddress - 0x6);
            Distance = rw.GetByte(RaceMemoryAddress + 0x2);
            TotalChempScore = rw.GetByte(ChempionshipMemoryAddress);
            PrevChempScore = rw.GetByte(ChempionshipMemoryAddress+0xC);
            CurrChempScore = ScorePoints[PositionRead];
            FutureChempScore = TotalChempScore + CurrChempScore;
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
