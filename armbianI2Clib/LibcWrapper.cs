using System;
using System.Runtime.InteropServices;

namespace armbianI2Clib
{
    public class LibcWrapper
    {
        [DllImport("libc.so", EntryPoint = "open")]
        public static extern int Open(string fileName, int mode);

        [DllImport("libc.so", EntryPoint = "ioctl", SetLastError = true)]
        public static extern int Ioctl(int fd, int request, IntPtr data);

        [DllImport("libc.so", EntryPoint = "read", SetLastError = true)]
        public static extern int Read(int handle, byte[] data, int length);

        [DllImport("libc.so", EntryPoint = "write", SetLastError = true)]
        public static extern int Write(int handle, byte[] data, int length);

    }
}
