using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Audio.OpenAL;
using OpenTK.Audio.OpenAL.Extensions.Creative.EFX;

namespace Swordfish.Core.Audio
{
    internal class AudioManager
    {
        private float volume;
        private readonly ALDevice device;
        private readonly ALContext context;
        private int[] channels;

        public AudioManager(int numOfChannels=32, float volume=1f)
        {
            // Grab AL soft device if possible
            var devices = ALC.GetStringList(GetEnumerationStringList.DeviceSpecifier);
            string deviceName = ALC.GetString(ALDevice.Null, AlcGetString.DefaultDeviceSpecifier);
            foreach (var i in devices)
            {
                if (i.Contains("OpenAL Soft"))
                {
                    deviceName = i;
                }
            }

            device = ALC.OpenDevice(deviceName);
            context = ALC.CreateContext(device, (int[])null);
            ALC.MakeContextCurrent(context);

            CheckALError("Start");

            string exts = AL.Get(ALGetString.Extensions);
            string rend = AL.Get(ALGetString.Renderer);
            string vend = AL.Get(ALGetString.Vendor);
            string vers = AL.Get(ALGetString.Version);

            string alcExts = ALC.GetString(device, AlcGetString.Extensions);
            ALC.GetInteger(device, AlcGetInteger.MajorVersion, 1, out int alcMajorVersion);
            ALC.GetInteger(device, AlcGetInteger.MinorVersion, 1, out int alcMinorVersion);

            Console.WriteLine($"OpenAL Info:\nVendor: {vend}, \nVersion: {vers}, \nRenderer: {rend}, \nExtensions: {exts}, \nALC Version: {alcMajorVersion}.{alcMinorVersion}, \nALC Extensions: {alcExts}");

            channels = new int[numOfChannels];

            AL.GenSources(numOfChannels, channels);

            AL.Listener(ALListenerf.Gain, volume);

        }

        public void PlayAudio()
        {

        }

        public static void CheckALError(string str)
        {
            ALError error = AL.GetError();
            if (error != ALError.NoError)
            {
                Console.WriteLine($"ALError at '{str}': {AL.GetErrorString(error)}");
            }
        }

    }
}
