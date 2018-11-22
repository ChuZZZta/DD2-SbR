using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WpfApp1
{
    public class MemoryRW
    {
        public MemoryRW(string processname)
        {
            this.processname = processname;
        }
        const int PROCESS_WM_READ = 0x0010;
        string processname;
        static string result = "";

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        public int getByte(int offset)
        {

            Process process = Process.GetProcessesByName(processname)[0];
            IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

            int bytesRead = 0;
            byte[] buffer = new byte[1]; 
            ReadProcessMemory((int)processHandle, offset, buffer, buffer.Length, ref bytesRead);
            return buffer[0];
        }
    }
}
