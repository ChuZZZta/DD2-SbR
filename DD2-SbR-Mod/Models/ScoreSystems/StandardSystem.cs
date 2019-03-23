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


        public List<Car> CarList { get; set; } = new List<Car>();
        int surpriseCounter = 10;

        int eliminateCounter = 10;
        int eliminateIndeks = 19;

        public void UpdateStandard()
        {
            foreach (Car car in CarList) car.Update();

            if (Car.modConfig.eliminateModConfig) EliminateRace();
            if (Car.modConfig.surpriseModConfig) SurpriseRace();

            CarList = CarList.OrderByDescending(x => x.SortByLapDis).ToList();
            for (int i = 0; i < 20; i++) CarList[i].Position = i + 1;
        }

        void SurpriseRace()
        {

        }
        void EliminateRace()
        {
            if(eliminateCounter == 0)
            {
                CarList[eliminateIndeks].DamageModel.SetFrontRight(100);
                eliminateIndeks--;
                eliminateCounter = Car.modConfig.eliminateSec;
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
