using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using System.Management;

namespace HexRPG.Utilities
{
    public static class HardwareUtilities
    {
        public static GraphicsDeviceManager GraphicsDevice { get; set; }
        public static AudioEngine AudioDevice { get; set; }
        public static string GetVideoDeviceName()
        {
            return GraphicsDevice.GraphicsDevice.Adapter.Description;
        }

        public static string GetAudioDeviceName()
        {
            return "";
        }

        public static string GetChipSetName()
        {
            return System.Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
        }
    }
}
