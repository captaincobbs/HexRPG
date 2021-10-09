using System;

namespace HexRPG
{
    public static class Program
    {
        public const string MonoGameVersion = "3.8.0.1641";
        public const string CurrentVersion = "1.0";

        private static MainGame program;

        [STAThread]
        static void Main(string[] args)
        {
            Utilities.FileUtilities.CreateFolders();
            Utilities.LogUtilities.Log($"MonoGame Version: {Program.MonoGameVersion}","System");
            Utilities.LogUtilities.Log($"Version: {Program.CurrentVersion}", "System");
            using (program = new MainGame())
            {
                program.Exiting += (a, b) => Utilities.LogUtilities.Flush();
                program.Run();
            }
        }

        public static void Exit()
        {
            program.Exit();
        }
    }
}
