using System;
using System.Collections.Generic;
using System.Text;

namespace armbianI2Clib
{
    public class I2CDevice : IDisposable
    {
        private const int maxBufferLength = 10;

        private int _deviceHandle;

        /// <summary>
        /// Initializes I2C device
        /// </summary>
        /// <param name="devicePath">path to I2C device. Like "/dev/i2c-1"</param>
        /// <param name="deviceAddress">I2C address. Like "0x48"</param>
        public I2CDevice(string devicePath, int deviceAddress)
        {
            _deviceHandle = LibcWrapper.Open(devicePath, (int)LibcConstants.OpenFileMode.O_RDWR);
            if (_deviceHandle < 0) throw new Exception($"Can't open '{devicePath}'. Returned {_deviceHandle}");

            var deviceReturnCode = LibcWrapper.Ioctl(_deviceHandle, (int)LibcConstants.I2CMode.I2C_SLAVE, deviceAddress);
            if (deviceReturnCode < 0) throw new Exception($"Can't set I2C address. Returned {deviceReturnCode}");
        }

        public byte[] Read(int length)
        {
            if (length < 1 || length > maxBufferLength) throw new Exception("Length is out of bounds");

            byte[] result = new byte[length];
            var readOp = LibcWrapper.Read(_deviceHandle, result, length);
            if (readOp != length) throw new Exception($"Read error. Returned: {readOp}, expected: {length}");

             return result;
        }

        public void Write(byte[] data, int length)
        {
            if (length < 1 || length > maxBufferLength) throw new Exception("Length is out of bounds");

            var writeOp = LibcWrapper.Write(_deviceHandle,data, length);
            if (writeOp != length) throw new Exception($"Write error. Returned: {writeOp}, expected: {length}");
        }

        public void Dispose()
        {
            if (_deviceHandle >= 0)
            {
                LibcWrapper.Close(_deviceHandle);
            }
        }

        ~I2CDevice()
        {
            Dispose();
        }
    }
}
