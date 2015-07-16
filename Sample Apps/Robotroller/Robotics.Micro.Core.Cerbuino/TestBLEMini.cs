using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System;
using System.IO.Ports;
using System.Threading;
using Robotics.Messaging;
using Robotics.Micro.Devices;
using Robotics.Micro.Generators;
using Robotics.Micro.Motors;
using Robotics.Micro.Sensors.Proximity;
using Robotics.Micro.SpecializedBlocks;

namespace Robotics.Micro.Core.Cerbuino
{
    public class TestBLEMini
    {
        public static void Run()
        {
            var serialPort = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
            serialPort.Open();

            var server = new ControlServer(serialPort);

            var led = new Microsoft.SPOT.Hardware.OutputPort(GHI.Pins.FEZCerbuino.Digital.LED1, false);
            var lv = false;

            var a0 = new AnalogInput(GHI.Pins.FEZCerbuino.AnalogIn.A0, -1);
            var a1 = new AnalogInput(GHI.Pins.FEZCerbuino.AnalogIn.A1, -1);

            var uptimeVar = server.RegisterVariable("Uptime (s)", 0);

            server.RegisterVariable("Speed", 0, v => {
                Debug.Print("Speed: " + v.Value.ToString());
            });
            server.RegisterVariable("Turn", 0, v => {
                Debug.Print("Turn: " + v.Value.ToString());
            });

            var a0Var = server.RegisterVariable("Analog 0", 0);
            var a1Var = server.RegisterVariable("Analog 1", 0);

            var magicCmd = server.RegisterCommand("Magic", () =>
            {
                Debug.Print("MAAAGIIICC");
                return 42;
            });

            for (var i = 0; true; i++)
            {

                uptimeVar.Value = i;
                a0Var.Value = a0.Read();
                a1Var.Value = a1.Read();

                led.Write(lv);
                lv = !lv;
                Thread.Sleep(1000);
            }
        }
    }
}
