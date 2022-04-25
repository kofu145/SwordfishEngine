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
        private int[] sources;
        private Audio[] channels;
        // next available channel
        private int channelTracker;

        /// <summary>
        /// The singleton instance of the AudioManager.
        /// NOTE: There are some context dependent things that need to be initialized, so don't reference this
        /// until you've completely made your window (which shouldn't be a problem anyways).
        /// </summary>
        private static AudioManager instance;

        // Singleton Pattern Logic
        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AudioManager();

                }
                return instance;
            }
        }

        public void Initialize(int numOfChannels = 64, float volume = 1f)
        {
            sources = new int[numOfChannels];
            channels = new Audio[numOfChannels];
            channelTracker = 0;

            AL.GenSources(numOfChannels, sources);

            AL.Listener(ALListenerf.Gain, volume);
        }

        private AudioManager()
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

            this.device = ALC.OpenDevice(deviceName);
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


        }

        /// <summary>
        /// Finds an applicable channel, then plays the data found in the given <see cref="Audio"/> object.
        /// </summary>
        /// <param name="audio"></param>
        /// <returns></returns>
        public void PlayAudio(Audio audio)
        {
            //Console.WriteLine(channelTracker);
            if (channelTracker >= sources.Length)
            {
                //Console.WriteLine(channelTracker);

                bool channelOverflow = true;
                // Find next possible channel
                for (int i = 0; i < sources.Length; i++)
                {
                    if (IsPlaying(i) || channels[i].priority == AudioPriority.Critical)
                        continue;

                    // if this isn't skipped, we know there's another available channel id
                    channelTracker = i;
                    channelOverflow = false;
                }

                if (channelOverflow)
                {
                    channelTracker = -1;
                    var priorityTrack = AudioPriority.Critical;
                    // overwrite first applicable sound with lowest priority
                    for (int i = 0; i < sources.Length; i++)
                    {
                        if (channels[i].priority < priorityTrack)
                            channelTracker = i;
                    }
                    // if we couldn't find any applicable channels
                    if (channelTracker == -1)
                    {
                        throw new Exception("Channels overflowed! Number of playable sounds exceeded!");
                    }
                }

            }
                
            int source = sources[channelTracker];
            channels[channelTracker] = audio;
            AL.Source(source, ALSourcei.Buffer, audio.buffer);
            AL.Source(source, ALSourcef.Gain, audio.volume); 
            AL.SourcePlay(source);
            channelTracker++;
        }

        /// <summary>
        /// Checks if any audio is playing on a given channel.
        /// </summary>
        /// <param name="channel">The channel to evaluate.</param>
        /// <returns>A boolean indicating whether or not it is playing.</returns>
        public bool IsPlaying(int channel)
        {
            return AL.GetSourceState(sources[channel]) == ALSourceState.Playing;
        }

        /// <summary>
        /// Returns the state of an audio channel.
        /// </summary>
        /// <param name="channel">The channel to evaluate.</param>
        /// <returns></returns>
        public AudioState GetAudioState(int channel)
        {
            return (AudioState)Enum.Parse(typeof(AudioState), AL.GetSourceState(sources[channel]).ToString());
        }

        /// <summary>
        /// Returns the state of an audio channel.
        /// </summary>
        /// <param name="audio">The <see cref="Audio"/> to evaluate.</param>
        /// <returns></returns>
        public AudioState GetAudioState(Audio audio)
        {
            return (AudioState)Enum.Parse(typeof(AudioState), AL.GetSourceState(sources[Array.IndexOf(channels, audio)]).ToString());
        }

        // TODO: Concern over indexof performance? Can come up with own algorithm. Also, pitch also scales time.
        /// <summary>
        /// Pauses the given audio channel.
        /// </summary>
        /// <param name="audio"></param>
        public void Pause(Audio audio)
        {
            AL.SourcePause(sources[Array.IndexOf(channels, audio)]);
        }

        public void Resume(Audio audio)
        {
            AL.SourcePlay(sources[Array.IndexOf(channels, audio)]);
        }

        public void Stop(Audio audio)
        {
            AL.SourceStop(sources[Array.IndexOf(channels, audio)]);
        }

        public void SetVolume(Audio audio)
        {
            AL.Source(sources[Array.IndexOf(channels, audio)], ALSourcef.Gain, audio.volume);
        }

        public void SetVolume(int channel, float volume)
        {
            AL.Source(sources[channel], ALSourcef.Gain, volume);
        }

        public void SetMasterVolume(float volume)
        {
            this.volume = volume;
            AL.Listener(ALListenerf.Gain, this.volume);
        }

        public void SetPitch(Audio audio)
        {
            AL.Source(sources[Array.IndexOf(channels, audio)], ALSourcef.Pitch, audio.pitch);
        }

        public void SetPitch(int channel, float pitch)
        {
            AL.Source(sources[channel], ALSourcef.Pitch, pitch);
        }
        public void SetLooping(Audio audio)
        {
            AL.Source(sources[Array.IndexOf(channels, audio)], ALSourceb.Looping, audio.loops);
        }

        public void SetLooping(int channel, bool loop)
        {
            AL.Source(sources[channel], ALSourceb.Looping, loop);
        }

        public static void CheckALError(string str)
        {
            ALError error = AL.GetError();
            if (error != ALError.NoError)
            {
                Console.WriteLine($"ALError at '{str}': {AL.GetErrorString(error)}");
            }
        }

        public void OnUnload()
        {
            AL.SourceStop(sources.Length, sources);
            AL.DeleteSources(sources.Length, ref sources[0]);

            ALC.MakeContextCurrent(ALContext.Null);
            ALC.DestroyContext(context);
            ALC.CloseDevice(device);

        }

    }
}
