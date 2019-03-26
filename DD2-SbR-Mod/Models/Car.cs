using Newtonsoft.Json;
using Sbr.Models.Tools;
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
        [JsonProperty("DamageMemoryAddress")]
        public int DamageMemoryAddress { get; set; }

        public static bool isNewSeason = false;
        
        public static string Processname { get; set; }

        private static List<int> ScorePoints = new List<int>() { 999,100,75,50,40,35,30,25,20,15,10,9,8,7,6,5,4,3,2,1,0};

        public static ModConfig ModConfig;
        private int PrevDistance = 0;
        IMemoryRW rw;

        //memories variables
        public int LapNumber { get; set; } = 1;
        public int Distance { get; set; } = 1;
        public int PositionRead { get; set; } = 1;
        public int Position { get; set; } = 1;
        public int TotalChempScore { get; set; } = 1;
        public int PrevChempScore { get; set; } = 1;
        public int CurrChempScore { get; set; } = 1;
        public int FutureChempScore { get; set; } = 1;



        public CarDamage DamageModel = new CarDamage();
        public int FrontRight { get; set; } = 0;
        public int FrontLeft { get; set; } = 0;
        public int RearLeft { get; set; } = 0;
        public int RearRight { get; set; } = 0;
        public int SideRight { get; set; } = 0;
        public int SideLeft { get; set; } = 0;


        public double SortByLapDis = 0;
        /* Addreses in memmory:
         *  RaceMemoryAddress                lap count
         *  RaceMemoryAddress - 0x6          postion
         *  RaceMemoryAddress + 0x2          lap distance
         *  ChempionshipMemoryAddress        total chemp score
         *  ChempionshipMemoryAddress + 0xC  prev chemp score
         */


        public void Update()
        {
            rw = new MemoryRW(Processname);
            PositionRead = rw.GetByte(RaceMemoryAddress - 0x6);
            Distance = rw.GetByte(RaceMemoryAddress + 0x2);
            UpdateDamage();

            ReadLapNumber(ModConfig.lapModConfig);

            SortByLapDis = LapNumber + ((double)Distance / 1000);
        }
        public void UpdateChampionship()
        {
            rw = new MemoryRW(Processname);
            PositionRead = rw.GetByte(RaceMemoryAddress - 0x6);
            TotalChempScore = rw.GetByte(ChempionshipMemoryAddress);
            PrevChempScore = rw.GetByte(ChempionshipMemoryAddress + 0xC);
            CurrChempScore = ScorePoints[PositionRead];
            FutureChempScore = TotalChempScore + CurrChempScore;
            // xDDD very nice season check - i fix it later
            if (TotalChempScore < PrevChempScore) isNewSeason = true;
            else isNewSeason = false;
        }

        public void SetDamageModel()
        {
            DamageModel.SetAddresses(DamageMemoryAddress, Processname);
        }

        void UpdateDamage()
        {
            FrontRight = DamageModel.GetFrontRight();
            FrontLeft = DamageModel.GetFrontLeft();
            RearLeft = DamageModel.GetRearLeft();
            RearRight = DamageModel.GetRearRight();
            SideRight = DamageModel.GetSideRight();
            SideLeft = DamageModel.GetSideLeft();
        }

        private void ReadLapNumber(bool lapmode)
        {
            int mapId = 0;
            if (lapmode)
            {
                mapId = rw.GetByte(Map.SelectedMapAddress);
                if(LapNumber >= ModConfig.lapLimit)
                {
                    rw.SetByte(RaceMemoryAddress, ModConfig.MapList[mapId].LapsNumber);
                }
                else
                {
                    if (PrevDistance > Distance)
                    {
                        LapNumber++;
                        rw.SetByte(RaceMemoryAddress, 1);
                    }
                    PrevDistance = Distance;
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
