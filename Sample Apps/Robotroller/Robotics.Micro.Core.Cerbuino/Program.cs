using System;
using Microsoft.SPOT;
using System.Threading;
using Robotics.Micro.Motors;
using GHI.Pins;

namespace Robotics.Micro.Core.Cerbuino
{
    public class Program
    {
        public static void Main()
        {
            //using (var led = new Microsoft.SPOT.Hardware.OutputPort(GHI.Pins.FEZCerbuino.Digital.LED1, false))
            //{

            //    for (var i = 0; i < 3; i++)
            //    {
            //        led.Write(true);
            //        Thread.Sleep(250);
            //        led.Write(false);
            //        Thread.Sleep(250);
            //    }
            //}

            //TestBLEMini.Run();

            TestRCCar.Run();

            //TestHBridge();

            //Thread.Sleep(Timeout.Infinite);
        }

        private static void TestHBridge()
        {
            var leftMotor = HBridgeMotor.CreateForNetduino (FEZCerbuino.PWM.D5, FEZCerbuino.Digital.D7, FEZCerbuino.Digital.D8);
            //var rightMotor = HBridgeMotor.CreateForNetduino(FEZCerbuino.PWM.D10, FEZCerbuino.Digital.D12, FEZCerbuino.Digital.D11);

            leftMotor.A1Output.Value = 0;
            leftMotor.A2Output.Value = 1;
            leftMotor.PwmDutyCycleOutput.Value = .25;
        }
    }
}
