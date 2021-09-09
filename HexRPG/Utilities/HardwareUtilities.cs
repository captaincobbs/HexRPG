using System.Management;
using System.Collections.Generic;

namespace HexRPG.Utilities
{
    public static class HardwareUtilities
    {
        private static Dictionary<string, string> systemValues {get; set;}

        /// <summary>
        /// Caches the system values
        /// </summary>
        public static void Initialize()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            systemValues = new Dictionary<string, string>();

            systemValues.Add("GPUName", MainGame.Graphics.GraphicsDevice.Adapter.Description);

            foreach (ManagementObject queryObj in searcher.Get())
            {
                systemValues.Add("ProcessorName", (string)queryObj["Name"]);
            }

            systemValues.Add("AssemblyRuntimeVersion", typeof(string).Assembly.ImageRuntimeVersion);
        }

        /// <summary>
        /// Returns the name of the current video device
        /// </summary>
        public static string GetVideoDeviceName()
        {
            return systemValues["GPUName"];
        }

        /// <summary>
        /// Returns the name of the current audio device
        /// </summary>
        public static string GetAudioDeviceName()
        {
            return "";
        }

        /// <summary>
        /// Returns the CPU Chipset name
        /// </summary>
        public static string GetChipSetName()
        {
            return systemValues["ProcessorName"];
        }

        /// <summary>
        /// Returns the C# version used
        /// </summary>
        public static string GetAssemblyRuntimeVersion()
        {
            return systemValues["AssemblyRuntimeVersion"];
        }
    }
}
