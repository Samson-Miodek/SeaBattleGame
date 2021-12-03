using System;
using System.Windows.Forms;

namespace SeaBattleGame
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var gameForm = new GameForm();
            Application.Run(gameForm);
        }
    }
}
