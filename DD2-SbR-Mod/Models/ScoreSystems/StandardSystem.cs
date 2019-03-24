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
        public int surpriseCounter = 10;
        string SurpriceInfo = "";

        public int eliminateCounter = 10;
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
            surpriseCounter--;
            if (surpriseCounter == 0)
            {
                int Target = Rnd.Next(0, 19);
                int Value = 0;
                SurpriceInfo = "Car " + CarList[Target].Number + " " + CarList[Target].Name + " gets: ";
                switch (Rnd.Next(1, 6)) // choosing surprice
                {
                    case 1: //DAMAGE CAR
                        Value = Rnd.Next(0, 100);
                        switch (Rnd.Next(1, 7)) // choosing side to damage
                        {
                            case 1:
                                SurpriceInfo += "front left damage set: " + Value;
                                CarList[Target].DamageModel.SetFrontLeft(Value);
                                break;
                            case 2:
                                SurpriceInfo += "front right damage set: " + Value;
                                CarList[Target].DamageModel.SetFrontRight(Value);
                                break;
                            case 3:
                                SurpriceInfo += "side left damage set: " + Value;
                                CarList[Target].DamageModel.SetSideLeft(Value);
                                break;
                            case 4:
                                SurpriceInfo += "side right damage set: " + Value;
                                CarList[Target].DamageModel.SetSideRight(Value);
                                break;
                            case 5:
                                SurpriceInfo += "rear left damage set: " + Value;
                                CarList[Target].DamageModel.SetRearLeft(Value);
                                break;
                            case 6:
                                SurpriceInfo += "rear right damage set: " + Value;
                                CarList[Target].DamageModel.SetRearRight(Value);
                                break;
                            default:
                                SurpriceInfo = "";
                                break;
                        }
                        break;
                    case 2: //STERRING POWER
                        Value = Rnd.Next(0, 2);
                        SurpriceInfo += "steering power set [0-2]: " + Value;
                        CarList[Target].DamageModel.SetPowerSter(Value);
                        break;
                    case 3: //STERRING ISSUE
                        Value = Rnd.Next(-256, 256);
                        SurpriceInfo += "steering issue set [-256-256]: " + Value;
                        CarList[Target].DamageModel.SetSterIssue(Value);
                        break;
                    case 4: //Bonus lap
                        if (Rnd.Next(0, 2) == 0)
                        {
                            CarList[Target].LapNumber--;
                            SurpriceInfo += "minus 1 lap";
                        }
                        else
                        {
                            CarList[Target].LapNumber++;
                            SurpriceInfo += "plus 1 lap";
                        }
                        break;
                    case 5: //ACC VALUE
                        Value = Rnd.Next(0, 200);
                        SurpriceInfo += "accelerate power [0-200]: " + Value;
                        CarList[Target].DamageModel.SetAccVal(Value);
                        break;
                    default:
                        SurpriceInfo = "";
                        break;
                }
                surpriseCounter = Car.ModConfig.surpriseSec;
            }
            else
            {
                SurpriceInfo = "Next suprise in... " + surpriseCounter;
            }
            Console.WriteLine(SurpriceInfo);
        }
        

        void EliminateRace()
        {
            if (eliminateCounter == 0)
            {
                CarList[eliminateIndeks].DamageModel.SetFrontRight(100);
                Console.WriteLine("Car " + CarList[eliminateIndeks].Number + " " + CarList[eliminateIndeks].Name + " is eliminated!!!");
                eliminateIndeks--;
                eliminateCounter = Car.ModConfig.eliminateSec;
            }
            else Console.WriteLine("Next elimination in... "+ eliminateCounter);
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
