using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace HexRPG.Utilities
{
    public static class HardwareUtilities
    {
        public static GraphicsDeviceManager GraphicsDevice { get; set; }
        public static AudioEngine AudioDevice { get; set; }
        public static string GetVideoDeviceName()
        {
            return GraphicsDevice.GraphicsDevice.Adapter.DeviceName;
        }

        public static string GetAudioDeviceName()
        {
            return "";
        }
    }
}
