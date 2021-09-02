using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace KeyblangChanger
{
    public class Changer
    {
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        
        [DllImport("user32.dll",
            CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode,
            EntryPoint = "LoadKeyboardLayout",
            SetLastError = true,
            ThrowOnUnmappableChar = false)]
        static extern uint LoadKeyboardLayout(
            StringBuilder pwszKLID,
            uint flags);
        
        private const uint WM_INPUTLANGCHANGEREQUEST = 0x0050;
        private const uint KLF_ACTIVATE = 0x00000001;
        

        public static void SetByCulture(string cultureString)
        {
            var cultureInfo = CultureInfo.GetCultureInfo(cultureString);
            SetByLayout(cultureInfo.LCID.ToString("x8"));
        }
        
        public static void SetByLayout(string layoutId)
        {
            var foregroundWindow = GetForegroundWindow();
                        
            PostMessage(foregroundWindow,
                WM_INPUTLANGCHANGEREQUEST,
                0,
                (int)LoadKeyboardLayout(new StringBuilder(layoutId), KLF_ACTIVATE)
            );
        }
    }
}