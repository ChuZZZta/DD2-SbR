using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbr.Models
{
    public class CarDamage
    {
        public int FrontRight = 0;
        public int FrontLeft = 0;
        public int BackLeft = 0;
        public int BackRight = 0;
        public int SideRight = 0;
        public int SideLeft = 0;
        public int RearLeftWheel = 0;
        public int RearRightWheel = 0;
        public int SideRightWheel = 0;
        public int SideLeftWheel = 0;
        public int Radiator = 0;

        public int DamageAddress = 0x0;
        
        public void UpdateDamage()
        {

        }
    }
}
