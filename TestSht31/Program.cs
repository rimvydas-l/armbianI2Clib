using armbianI2Clib;
using System;
using System.Threading;

namespace TestSht31
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test of SHT31-D and armbianI2Clib");

            try
            {
                using (var device = new I2CDevice("/dev/i2c-0", 0x44))
                {
                    Console.WriteLine("init complete");

                    device.Write(new byte[] { 0x24, 0x00 }, 2);
                    Thread.Sleep(100);
                    Console.WriteLine("write complete");

                    var readBuff = device.Read(3);

                    Console.WriteLine($"Read count: {readBuff.Length}");
                    Console.WriteLine($"Read 1:{readBuff[0]}, 2:{readBuff[1]}, 3:{readBuff[2]}");
                    Console.WriteLine($"Temp: {CalcTemp(readBuff[0], readBuff[1])}C");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            Console.WriteLine("END");
        }

        static double CalcTemp(int hByte, int lByte)
        {
            return (-45 + 175 * ((double)(hByte * 256 + lByte) / 65535));
        }
    }
}
