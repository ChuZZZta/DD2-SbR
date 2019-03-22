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

            SteeringIssue = SideLeft + 0x10 ;
            SteeringPower = SteeringIssue + 0x4;
            AccVar = SteeringPower + 0x4;

            RearRightWheel = AccVar + 0x4;
            RearLeftWheel = RearRightWheel + 0x4;
            FrontRightWheel = RearLeftWheel + 0x4;
            FrontLeftWheel = FrontRightWheel + 0x4;

            FrontLeftSuspen = FrontLeftWheel + 0x10;
            RearLeftSuspen = FrontLeftSuspen + 0x4;
            FrontRightSuspen = RearLeftSuspen + 0x4;
            RearRightSuspen = FrontRightSuspen + 0x4;
        }

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


        public void SetFrontLeft(int percent)
        {
            int val = Convert.ToInt16(percent * 40.96);
            rw.SetByte(FrontLeft, val);
        }
        public void SetFrontRight(int percent)
        {
            int val = Convert.ToInt16(percent * 40.96);
            rw.SetByte(FrontRight, val);
        }
        public void SetSideRight(int percent)
        {
            int val = Convert.ToInt16(percent * 40.96);
            rw.SetByte(SideRight, val);
        }
        public void SetSideLeft(int percent)
        {
            int val = Convert.ToInt16(percent * 40.96);
            rw.SetByte(SideLeft, val);
        }
        public void SetRearLeft(int percent)
        {
            int val = Convert.ToInt16(percent * 40.96);
            rw.SetByte(RearLeft, val);
        }
        public void SetRearRight(int percent)
        {
            int val = Convert.ToInt16(percent * 40.96);
            rw.SetByte(RearRight, val);
        }
    }
}
