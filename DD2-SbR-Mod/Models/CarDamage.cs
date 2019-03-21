using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbr.Models
{
    public class CarDamage
    {
        string Processname = "";
        IMemoryRW rw;

        //car
        int FrontRight = 0;
        int FrontLeft = 0;
        int RearLeft = 0;
        int RearRight = 0;
        int SideRight = 0;
        int SideLeft = 0;

        //wheels
        int RearLeftWheel = 0;
        int RearRightWheel = 0;
        int FrontRightWheel = 0;
        int FrontLeftWheel = 0;

        //suspension
        int RearLeftSuspen = 0;
        int RearRightSuspen = 0;
        int FrontRightSuspen = 0;
        int FrontLeftSuspen = 0;

        //misc
        int Radiator = 0;
        int AccVar = 0;
        int SteeringPower = 0;
        int SteeringIssue = 0;

        public void SetAddresses(int address, string Processname)
        {
            this.Processname = Processname;
            rw = new MemoryRW(Processname);


            Radiator = address;

            FrontLeft = Radiator + 0x24;
            FrontRight = FrontLeft + 0x8;
            SideRight = FrontRight + 0x8;
            RearRight = SideRight + 0x4;
            RearLeft = RearRight + 0x8;
            SideLeft = RearLeft + 0x8;
        }

        public int GetFrontLeft()
        {
            return rw.GetByte(FrontLeft);
        }
        public int GetFrontRight()
        {
            return rw.GetByte(FrontRight);
        }
        public int GetSideRight()
        {
            return rw.GetByte(SideRight);
        }
        public int GetSideLeft()
        {
            return rw.GetByte(SideLeft);
        }
        public int GetRearLeft()
        {
            return rw.GetByte(RearLeft);
        }
        public int GetRearRight()
        {
            return rw.GetByte(RearRight);
        }


        public void SetFrontLeft(int percent)
        {
            
        }
        public void SetFrontRight(int percent)
        {
            
        }
        public void SetSideRight(int percent)
        {
            
        }
        public void SetSideLeft(int percent)
        {
            
        }
        public void SetRearLeft(int percent)
        {
            
        }
        public void SetRearRight(int percent)
        {
            
        }
    }
}
