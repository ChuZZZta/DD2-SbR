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

        public void UpdateStandard()
        {
            //Updating car's meemory data 
            foreach (Car car in CarList) car.Update();

            //sort metod depends on used mode
            if (Car.modConfig.lapModConfig) CarList = CarList.OrderByDescending(x => x.LapNumber).ThenBy(x => x.PositionRead).ToList();
            else CarList = CarList.OrderBy(x => x.PositionRead).ToList();

            //repostion cars du to lack of better methodes on having position on view
            for (int i = 0; i < 20; i++) CarList[i].Position = i + 1;
        }
    }
}
