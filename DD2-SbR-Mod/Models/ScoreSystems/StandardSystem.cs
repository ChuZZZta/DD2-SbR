using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbr.Models.ScoreSystems
{
    public class StandardSystem
    {
        public StandardSystem(List<Car> CarList)
        {
            this.CarList = CarList;
        }

        Random Rnd = new Random();
        public List<Car> CarList { get; set; } = new List<Car>();
        int surpriseCounter = 10;

        int eliminateCounter = 10;
        int eliminateIndeks = 19;

        public void UpdateStandard()
        {
            foreach (Car car in CarList) car.Update();

            if (Car.ModConfig.eliminateModConfig) EliminateRace();
            if (Car.ModConfig.surpriseModConfig) SurpriseRace();

            CarList = CarList.OrderByDescending(x => x.SortByLapDis).ToList();
            for (int i = 0; i < 20; i++) CarList[i].Position = i + 1;
        }

        void SurpriseRace()
        {
            int Target = Rnd.Next(0, 19);

            switch (Rnd.Next(1, 5)) // choosing surprice
            {
                case 1: //DAMAGE CAR
                    switch (Rnd.Next(1, 6)) // choosing side to damage
                    {
                        case 1:
                            CarList[Target].DamageModel.SetFrontLeft(Rnd.Next(0, 100));
                            break;
                        case 2:
                            CarList[Target].DamageModel.SetFrontRight(Rnd.Next(0, 100));
                            break;
                        case 3:
                            CarList[Target].DamageModel.SetSideLeft(Rnd.Next(0, 100));
                            break;
                        case 4:
                            CarList[Target].DamageModel.SetSideRight(Rnd.Next(0, 100));
                            break;
                        case 5:
                            CarList[Target].DamageModel.SetRearLeft(Rnd.Next(0, 100));
                            break;
                        case 6:
                            CarList[Target].DamageModel.SetRearRight(Rnd.Next(0, 100));
                            break;
                    }
                    break;
                case 2: //STERRING POWER
                    CarList[Target].DamageModel.SetPowerSter(Rnd.Next(0, 2));
                    break;
                case 3: //STERRING ISSUE
                    CarList[Target].DamageModel.SetSterIssue(Rnd.Next(-256, 256));
                    break;
                case 4: //Bonus lap
                    if (Rnd.Next(0, 1)==0) CarList[Target].LapNumber--;
                    else CarList[Target].LapNumber++;
                    break;
                case 5: //ACC VALUE
                    CarList[Target].DamageModel.SetAccVal(Rnd.Next(0, 200));
                    break;
                default:
                    break;
            }
        }
        

        void EliminateRace()
        {
            if(eliminateCounter == 0)
            {
                CarList[eliminateIndeks].DamageModel.SetFrontRight(100);
                eliminateIndeks--;
                eliminateCounter = Car.ModConfig.eliminateSec;
            }
            eliminateCounter--;
            if(CarList[0].LapNumber==0)
            {
                eliminateIndeks = 19;
                eliminateCounter = 10;
            }
            if (eliminateIndeks == 0) eliminateIndeks = 19;
        }
    }
}
