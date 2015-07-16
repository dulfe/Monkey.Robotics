using Microsoft.SPOT.Hardware;

namespace GHI.Pins
{
    public static class FEZCerbuino
    {
        /// <summary>
        /// Provides Pin definitions for FEZ Cerbuino's Arduino-styled I/O headers.
        /// </summary>
        public class Digital
        {
            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin A0 = FEZCerb.PB1;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin A1 = FEZCerb.PA5;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin A2 = FEZCerb.PB0;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin A3 = FEZCerb.PC3;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin A4 = FEZCerb.PC1;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin A5 = FEZCerb.PA4;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin D0 = FEZCerb.PB11;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin D1 = FEZCerb.PB10;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin D2 = FEZCerb.PB12;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin D3 = FEZCerb.PC14;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin D4 = FEZCerb.PC15;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin D5 = FEZCerb.PA8;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin D6 = FEZCerb.PA10;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin D7 = FEZCerb.PC4;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin D8 = FEZCerb.PB13;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin D9 = FEZCerb.PA9;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin D10 = FEZCerb.PA15;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin D11 = FEZCerb.PB5;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin D12 = FEZCerb.PB4;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin D13 = FEZCerb.PB3;

            /// <summary>Digital I/O.</summary>
            public const Cpu.Pin LED1 = FEZCerb.PB2;

        }

        /// <summary>
        /// Provides Pin definitions for FEZ Cerbuino's Gadgeteer sockets.
        /// </summary>
        public class Gadgeteer
        {
            /// <summary>
            /// Provides Pin definitions for FEZ Cerbuino's Socket 1.
            /// </summary>
            public class Socket1
            {
                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin3 = FEZCerb.PA14;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin4 = FEZCerb.PB10;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin5 = FEZCerb.PB11;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin6 = FEZCerb.PA13;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin7 = FEZCerb.PB5;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin8 = FEZCerb.PB4;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin9 = FEZCerb.PB3;

            }

            /// <summary>
            /// Provides Pin definitions for FEZ Cerbuino's Socket 2.
            /// </summary>
            public class Socket2
            {
                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin3 = FEZCerb.PA6;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin4 = FEZCerb.PA2;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin5 = FEZCerb.PA3;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin6 = FEZCerb.PA1;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin7 = FEZCerb.PA0;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin8 = FEZCerb.PB7;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin9 = FEZCerb.PB6;

            }

            /// <summary>
            /// Provides Pin definitions for FEZ Cerbuino's Socket 3.
            /// </summary>
            public class Socket3
            {
                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin3 = FEZCerb.PC0;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin4 = FEZCerb.PC1;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin5 = FEZCerb.PA4;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin6 = FEZCerb.PC5;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin7 = FEZCerb.PB8;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin8 = FEZCerb.PA7;

                /// <summary>Digital I/O.</summary>
                public const Cpu.Pin Pin9 = FEZCerb.PB9;

            }


        }

        /// <summary>
        /// Provides Channel definitions for FEZ Cerbuino's PWM capable pins.
        /// </summary>
        public class PWM
        {
            /// <summary>PWM Output</summary>
            public const Cpu.PWMChannel D5 = (Cpu.PWMChannel)3;

            /// <summary>PWM Output</summary>
            public const Cpu.PWMChannel A2 = (Cpu.PWMChannel)4;

            /// <summary>PWM Output</summary>
            public const Cpu.PWMChannel A0 = (Cpu.PWMChannel)5;

            /// <summary>PWM Output</summary>
            public const Cpu.PWMChannel D11 = (Cpu.PWMChannel)6;

            /// <summary>PWM Output</summary>
            public const Cpu.PWMChannel D12 = (Cpu.PWMChannel)7;

            /// <summary>PWM Output</summary>
            public const Cpu.PWMChannel D13 = (Cpu.PWMChannel)8;

            /// <summary>PWM Output</summary>
            public const Cpu.PWMChannel D0 = (Cpu.PWMChannel)9;

            /// <summary>PWM Output</summary>
            public const Cpu.PWMChannel D1 = (Cpu.PWMChannel)10;

            /// <summary>PWM Output</summary>
            public const Cpu.PWMChannel D6 = (Cpu.PWMChannel)11;

            /// <summary>PWM Output</summary>
            public const Cpu.PWMChannel D9 = (Cpu.PWMChannel)12;

            /// <summary>PWM Output</summary>
            public const Cpu.PWMChannel D10 = (Cpu.PWMChannel)13;

        }

        /// <summary>
        /// Provides Channel definitions for FEZ Cerbuino's AnalogIn capable pins.
        /// </summary>
        public class AnalogIn
        {
            /// <summary>Analog Input</summary>
            public const Cpu.AnalogChannel A0 = (Cpu.AnalogChannel)10;

            /// <summary>Analog Input</summary>
            public const Cpu.AnalogChannel A1 = (Cpu.AnalogChannel)8;

            /// <summary>Analog Input</summary>
            public const Cpu.AnalogChannel A2 = (Cpu.AnalogChannel)9;

            /// <summary>Analog Input</summary>
            public const Cpu.AnalogChannel A3 = (Cpu.AnalogChannel)7;

            /// <summary>Analog Input</summary>
            public const Cpu.AnalogChannel A4 = (Cpu.AnalogChannel)4;

            /// <summary>Analog Input</summary>
            public const Cpu.AnalogChannel A5 = (Cpu.AnalogChannel)5;

        }

    }
}