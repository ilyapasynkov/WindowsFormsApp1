using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Functions;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Func.PopytkaNum = 2; // Инициализация попыток.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LogForm());
        }
    }
}
