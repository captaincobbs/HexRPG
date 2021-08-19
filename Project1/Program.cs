using System;

namespace HexRPG
{
    public static class Program
    {
        private static MainGame program;

        [STAThread]
        static void Main()
        {
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
