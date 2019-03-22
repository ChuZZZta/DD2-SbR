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
            foreach (Car car in CarList) car.Update();

            CarList = CarList.OrderByDescending(x => x.SortByLapDis).ToList();
            for (int i = 0; i < 20; i++) CarList[i].Position = i + 1;
        }
    }
}
