using System;
using System.Collections.Generic;
using System.Text;

namespace armbianI2Clib
{
    public static class LibcConstants
    {
        public enum OpenFileMode
        {
            O_RDONLY = 0,
            O_WRONLY = 1,
            O_RDWR = 2
        }

        public enum I2CMode
        {
            I2C_SLAVE = 0x0703
        }
    }
}
