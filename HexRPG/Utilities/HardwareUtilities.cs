using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using System.Management;
using System.Collections.Generic;
using System;

namespace HexRPG.Utilities
{
    public static class HardwareUtilities
    {
        public static GraphicsDeviceManager GraphicsDevice { get; set; }
        public static AudioEngine AudioDevice { get; set; }

        private static Dictionary<string, string> systemValues {get; set;}

        public static void Initialize()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            systemValues = new Dictionary<string, string>();

            systemValues.Add("GPUName", GraphicsDevice.GraphicsDevice.Adapter.Description);

            foreach (ManagementObject queryObj in searcher.Get())
            {
                systemValues.Add("ProcessorName", (string)queryObj["Name"]);
            }

            systemValues.Add("AssemblyRuntimeVersion", typeof(string).Assembly.ImageRuntimeVersion);
        }

        public static string GetVideoDeviceName()
        {
            return systemValues["GPUName"];
        }

        public static string GetAudioDeviceName()
        {
            return "";
        }

        public static string GetChipSetName()
        {
            return systemValues["ProcessorName"];
        }

        public static string GetAssemblyRuntimeVersion()
        {
            return systemValues["AssemblyRuntimeVersion"];
        }
    }
}
