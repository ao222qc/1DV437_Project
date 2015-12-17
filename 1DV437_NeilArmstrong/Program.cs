using System;

namespace _1DV437_NeilArmstrong
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Controller.Game1())
                game.Run();
        }
    }
#endif
}
