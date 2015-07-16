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
using GHI.Pins;

namespace Robotics.Micro.Core.Cerbuino
{
    public class TestRCCar
    {
        public static void Run ()
        {
            //
            // Controls server
            //
            // initialize the serial port for COM1 (using D0 & D1)
            // initialize the serial port for COM3 (using D7 & D8)
            var serialPort = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
            serialPort.Open ();
            var server = new ControlServer (serialPort);

            //
            // Just some diagnostic stuff
            //
            var uptimeVar = server.RegisterVariable ("Uptime (s)", 0);

            var lv = false;
            var led = new Microsoft.SPOT.Hardware.OutputPort (FEZCerbuino.Digital.LED1, lv);

            //
            // Make the robot
            //
            var leftMotor = HBridgeMotor.CreateForNetduino (FEZCerbuino.PWM.D5, FEZCerbuino.Digital.D7, FEZCerbuino.Digital.D8);
            var rightMotor = HBridgeMotor.CreateForNetduino(FEZCerbuino.PWM.D10, FEZCerbuino.Digital.D11, FEZCerbuino.Digital.D12);

            var robot = new TwoWheeledRobot (leftMotor, rightMotor);

            //
            // Expose some variables to control it
            //
            robot.SpeedInput.ConnectTo (server, writeable: true, name: "Speed");
            robot.DirectionInput.ConnectTo (server, writeable: true, name: "Turn");

            leftMotor.SpeedInput.ConnectTo (server);
            rightMotor.SpeedInput.ConnectTo (server);

            //
            // Show diagnostics
            //
            for (var i = 0; true; i++) {
                uptimeVar.Value = i;

                led.Write (lv);
                lv = !lv;
                Thread.Sleep (1000);
            }
        }
    }
}
