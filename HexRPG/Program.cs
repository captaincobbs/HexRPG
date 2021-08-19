using System;

namespace HexRPG
{
    public static class Program
    {
        private static MainGame program;

        [STAThread]
        static void Main(string[] args)
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
