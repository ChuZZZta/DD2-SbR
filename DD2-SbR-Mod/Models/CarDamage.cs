using Sbr.Models.Tools;
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
            //CAR DAMAGE
            FrontLeft = Radiator + 0x24;
            FrontRight = FrontLeft + 0x8;
            SideRight = FrontRight + 0x8;
            RearRight = SideRight + 0x4;
            RearLeft = RearRight + 0x8;
            SideLeft = RearLeft + 0x8;
            //MISC ISSU
            SteeringIssue = SideLeft + 0x10 ;
            SteeringPower = SteeringIssue + 0x4;
            AccVar = SteeringPower + 0x4;
            //WHEELS
            RearRightWheel = AccVar + 0x4;
            RearLeftWheel = RearRightWheel + 0x4;
            FrontRightWheel = RearLeftWheel + 0x4;
            FrontLeftWheel = FrontRightWheel + 0x4;
            //SUSPENSION
            FrontLeftSuspen = FrontLeftWheel + 0x10;
            RearLeftSuspen = FrontLeftSuspen + 0x4;
            FrontRightSuspen = RearLeftSuspen + 0x4;
            RearRightSuspen = FrontRightSuspen + 0x4;
        }


        // GETTING CAR DAMAGE
        public int GetFrontLeft()
        {
            return Convert.ToInt32(rw.GetByte(FrontLeft) / 40.96);
        }
        public int GetFrontRight()
        {
            return Convert.ToInt32(rw.GetByte(FrontRight) / 40.96);
        }
        public int GetSideRight()
        {
            return Convert.ToInt32(rw.GetByte(SideRight) / 40.96);
        }
        public int GetSideLeft()
        {
            return Convert.ToInt32(rw.GetByte(SideLeft) / 40.96);
        }
        public int GetRearLeft()
        {
            return Convert.ToInt32(rw.GetByte(RearLeft) / 40.96);
        }
        public int GetRearRight()
        {
            return Convert.ToInt32(rw.GetByte(RearRight) / 40.96);
        }

        // SETTING CAR DAMAGE
        public void SetFrontLeft(int percent)
        {
            if (percent >= 0 && percent <= 100)
            {
                int val = Convert.ToInt16(percent * 40.96);
                rw.SetByte(FrontLeft, val);
            }
        }
        public void SetFrontRight(int percent)
        {
            if (percent >= 0 && percent <= 100)
            {
                int val = Convert.ToInt16(percent * 40.96);
                rw.SetByte(FrontRight, val);
            }
        }
        public void SetSideRight(int percent)
        {
            if (percent >= 0 && percent <= 100)
            {
                int val = Convert.ToInt16(percent * 40.96);
                rw.SetByte(SideRight, val);
            }
        }
        public void SetSideLeft(int percent)
        {
            if (percent >= 0 && percent <= 100)
            {
                int val = Convert.ToInt16(percent * 40.96);
                rw.SetByte(SideLeft, val);
            }
        }
        public void SetRearLeft(int percent)
        {
            if (percent >= 0 && percent <= 100)
            {
                int val = Convert.ToInt16(percent * 40.96);
                rw.SetByte(RearLeft, val);
            }
        }
        public void SetRearRight(int percent)
        {
            if (percent >= 0 && percent <= 100)
            {
                int val = Convert.ToInt16(percent * 40.96);
                rw.SetByte(RearRight, val);
            }
        }

        //SUSPENSION WHEEL DAMAGE
        public void BreakFrontLeft(bool dmg)
        {
            if(dmg)
            {
                rw.SetByte(FrontLeftWheel,1);
                rw.SetByte(FrontLeftSuspen,2);
            }
            else
            {
                rw.SetByte(FrontLeftWheel, 0);
                rw.SetByte(FrontLeftSuspen, 0);
            }
        }
        public void BreakFrontRight(bool dmg)
        {
            if (dmg)
            {
                rw.SetByte(FrontRightWheel, 1);
                rw.SetByte(FrontRightSuspen, 2);
            }
            else
            {
                rw.SetByte(FrontRightWheel, 0);
                rw.SetByte(FrontRightSuspen, 0);
            }
        }
        public void BreakRearLeft(bool dmg)
        {
            if (dmg)
            {
                rw.SetByte(RearLeftWheel, 1);
                rw.SetByte(RearLeftSuspen, 2);
            }
            else
            {
                rw.SetByte(RearLeftWheel, 0);
                rw.SetByte(RearLeftSuspen, 0);
            }
        }
        public void BreakRearRight(bool dmg)
        {
            if (dmg)
            {
                rw.SetByte(RearRightWheel, 1);
                rw.SetByte(RearRightSuspen, 2);
            }
            else
            {
                rw.SetByte(RearRightWheel, 0);
                rw.SetByte(RearRightSuspen, 0);
            }
        }

        //Misc
        public void SetAccVal(int percent)
        {
            int value = Convert.ToInt16(percent * 40.96);
            rw.SetByte(AccVar, value);
        }
        public void SetPowerSter(int val)
        {
            if(val<=2 && val>=0)
            {
                rw.SetByte(SteeringPower, val);
            }
        }
        public void SetSterIssue(int val)
        {
            if (val <= 256 && val >= -256)
            {
                rw.SetByte(SteeringIssue, val);
            }
        }
        public void BreakRadiator(bool dmg)
        {
            if(dmg)
            {
                rw.SetByte(Radiator, 1);
            }
            else
            {
                rw.SetByte(Radiator, 0);
            }
        }
    }
}
