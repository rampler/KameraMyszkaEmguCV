using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace KameraMyszkaEmguCV
{
    class KeyboardSimulating
    {
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        private const uint KEYEVENTF_KEYUP = 2;
        private const byte VK_CONTROL = 0x11;

        public static void SendCtrlX()
        {
            keybd_event(VK_CONTROL, 0, 0, 0);
            keybd_event(0x58, 0, 0, 0); //Send the X key (58 is "X")
            keybd_event(0x58, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);
        }

        public static void SendCtrlC()
        {
            keybd_event(VK_CONTROL, 0, 0, 0);
            keybd_event(0x43, 0, 0, 0); //Send the C key (43 is "C")
            keybd_event(0x43, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);
        }

        public static void SendCtrlV()
        {
            keybd_event(VK_CONTROL, 0, 0, 0);
            keybd_event(0x56, 0, 0, 0); //Send the V key (56 is "V")
            keybd_event(0x56, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);
        }
    }
}
