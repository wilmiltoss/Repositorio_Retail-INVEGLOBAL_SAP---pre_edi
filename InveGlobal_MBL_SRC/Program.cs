using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace InveStockMBL
{
    static class Program
    {

        [DllImport("coredll.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        //
        [DllImport("coredll.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);
        //
        //[DllImport("coredll.dll", EntryPoint = "SendMessage")]
        //public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        //
        //const int WM_SYSCOMMAND = 0x112;
        //const int SC_CLOSE = 0xF060;

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            //localizamos una ventana con el mismo nombre que esta
            IntPtr hWnd = FindWindow(null, frm_principal.NOMBRE_APLICACION);

            //si el programa no esta ya en ejecucion
            if (!hWnd.Equals(IntPtr.Zero))
            {
                //lo mandamos al frente y cerramos esta version
                SetForegroundWindow(hWnd);
            }
            else
            {
                //sino, lanzamos esta version
                Application.Run(new frm_principal());
            }
        }
    }
}