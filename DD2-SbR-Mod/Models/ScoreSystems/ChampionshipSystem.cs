using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbr.Models.ScoreSystems
{
    public class ChampionshipSystem
    {
        
        public List<Car> Division1 = new List<Car>();
        public List<Car> Division2 = new List<Car>();
        public List<Car> Division3 = new List<Car>();
        public List<Car> Division4 = new List<Car>();
        public bool seasonChanged = false;


        public void UpdateChampionship()
        {
            foreach (Car car in Division1) car.UpdateChampionship();
            foreach (Car car in Division2) car.UpdateChampionship();
            foreach (Car car in Division3) car.UpdateChampionship();
            foreach (Car car in Division4) car.UpdateChampionship();
            if (Car.isNewSeason != seasonChanged)
            {
                if (!seasonChanged)
                {
                    //usuniecie awansow
                    Car d2promo = Division2.First();
                    Division2.Remove(d2promo);
                    Car d3promo = Division3.First();
                    Division3.Remove(d3promo);
                    Car d4promo = Division4.First();
                    Division4.Remove(d4promo);

                    //spadki
                    Division4.Add(Division3.Last());
                    Division3.Remove(Division3.Last());

                    Division3.Add(Division2.Last());
                    Division2.Remove(Division2.Last());

                    Division2.Add(Division1.Last());
                    Division1.Remove(Division1.Last());

                    //dodanie awansow
                    Division1.Add(d2promo);
                    Division2.Add(d3promo);
                    Division3.Add(d4promo);

                    seasonChanged = true;
                }
                else seasonChanged = false;
            }

            Division1 = Division1.OrderByDescending(x => x.TotalChempScore).ToList();
            Division2 = Division2.OrderByDescending(x => x.TotalChempScore).ToList();
            Division3 = Division3.OrderByDescending(x => x.TotalChempScore).ToList();
            Division4 = Division4.OrderByDescending(x => x.TotalChempScore).ToList();
        }

        public void CleanDivisions()
        {
            Division1.Clear();
            Division2.Clear();
            Division3.Clear();
            Division4.Clear();
        }
        public void SetDivisions(List<Car> CarList)
        {
            Division1.AddRange(CarList.Where(x => new[] { 1, 3, 7, 10, 16 }.Contains(x.Id)));
            Division2.AddRange(CarList.Where(x => new[] { 2, 4, 8, 11, 14 }.Contains(x.Id)));
            Division3.AddRange(CarList.Where(x => new[] { 18, 19, 5, 13, 17 }.Contains(x.Id)));
            Division4.AddRange(CarList.Where(x => new[] { 0, 6, 9, 12, 15 }.Contains(x.Id)));
        }
    }
}
