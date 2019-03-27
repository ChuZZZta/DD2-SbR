using GameOverlay.Drawing;
using GameOverlay.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbr.Views
{
    public class WindowOverlay
    {
        //CTOR DTOR
        public WindowOverlay(IntPtr TargetWindow, Point LapPos, Point PositionPos, Point InfoPos)
        {
            var graphics = new Graphics
            {
                MeasureFPS = true,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true,
                UseMultiThreadedFactories = false,
                VSync = true,
                WindowHandle = IntPtr.Zero
            };
            Overlay = new StickyWindow(TargetWindow, graphics)
            {
                IsTopmost = true,
                IsVisible = true,
                FPS = 60,
                X = 0,
                Y = 0,
                Width = 800,
                Height = 600
            };
            Overlay.SetupGraphics += SetupGraphics;
            Overlay.DestroyGraphics += DestroyGraphics;
            Overlay.DrawGraphics += DrawGraphics;

            this.LapPos = LapPos;
            this.PositionPos = PositionPos;
            this.InfoPos = InfoPos;
        }

       
        //VARIABLES
        private readonly GraphicsWindow Overlay;

        private Font OverlayFont;
        private SolidBrush BackGroundColor;
        private SolidBrush FontColor;
        Point LapPos, PositionPos, InfoPos;
        string LapNumber = "", PosNumber = "", Info = "";


        //OVERLAY METHODES
        public void Run()
        {
            Overlay.StartThread();
        }
        public void Stop()
        {
            Overlay.Dispose();
        }

        private void SetupGraphics(object sender, SetupGraphicsEventArgs e)
        {
            var gfx = e.Graphics;
            OverlayFont = gfx.CreateFont("Arial", 16);
            BackGroundColor = gfx.CreateSolidBrush(0, 0, 0);
            FontColor = gfx.CreateSolidBrush(Color.Red);
        }
        
        private void DrawGraphics(object sender, DrawGraphicsEventArgs e)
        {
            var gfx = e.Graphics;
            gfx.ClearScene();
            gfx.DrawTextWithBackground(OverlayFont, 30, FontColor, BackGroundColor, LapPos.Y, LapPos.X, LapNumber);
            gfx.DrawTextWithBackground(OverlayFont, 60, FontColor, BackGroundColor, PositionPos.Y, PositionPos.X, PosNumber);
            gfx.DrawText(OverlayFont, 30, FontColor, InfoPos.Y, InfoPos.X, Info);
            
        }

        private void DestroyGraphics(object sender, DestroyGraphicsEventArgs e)
        {
            
        }

        //NORMAL METHIDES
        public void SetDrawingText(int LapNumber, int PosNumber, int LapLimit,string Info)
        {
            if (PosNumber > 9) this.PosNumber = PosNumber + "";
            else this.PosNumber = "0" + PosNumber;

            this.LapNumber =  LapNumber + " / " + LapLimit;

            this.Info = Info;
        }

    }
}
