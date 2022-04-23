using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Audio.OpenAL;
using Swordfish.Core.Audio.Formats;

namespace Swordfish.Core.Audio
{
    /// <summary>
    /// Handles audio buffer data. 
    /// </summary>
    internal class Audio
    {
        internal int buffer;

        internal byte[] data;
        internal int numChannels;
        internal ALFormat format;
        internal int sampleRate;
        internal int bitsPerSample;
        internal float volume;
        internal float pitch;
        internal bool loops;
        internal AudioPriority priority;
        internal AudioState state { get { return AudioManager.Instance.GetAudioState(this); } private set { } }

        private Audio(byte[] data, int channels, int bitsPerSample, int size, int sampleRate, float volume, float pitch, AudioPriority priority)
        {
            this.data = data;
            this.numChannels = channels;
            this.format = channels == 1 
                ? bitsPerSample == 8 
                    ? ALFormat.Mono8 : ALFormat.Mono16 : 
            channels == 2 
                ? bitsPerSample == 8
                    ? ALFormat.Stereo8 : ALFormat.Stereo16 :
                // If we couldn't get any, both not true
                throw new Exception("Couldn't load format!");
            this.sampleRate = sampleRate;
            this.bitsPerSample = bitsPerSample;
            this.volume = volume;
            this.priority = priority;

            buffer = AL.GenBuffer();
            // TODO: stream into several buffers
            AL.BufferData(buffer, format, data, sampleRate);

        }
        
        /// <summary>
        /// Instantiates an <see cref="Audio"/> instance from a wav file.
        /// </summary>
        /// <param name="filePath">Filepath of the wav file to load.</param>
        /// <param name="volume">Volume of this particular audio segment.</param>
        /// <param name="priority">Priority of the given sound to play</param>
        /// <returns></returns>
        public static Audio LoadFromWAV(string filePath, float volume, float pitch=1f, AudioPriority priority=AudioPriority.Standard)
        {

            WavHandler wavHandler;
            // http://soundfile.sapp.org/doc/WaveFormat/wav-sound-format.gif WAV file format
            using (var stream = File.Open(filePath, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    wavHandler = WavHandler.Read(stream, reader);
                }
            }
            return new Audio(
                wavHandler.AudioData, wavHandler.NumChannels, wavHandler.BitsPerSample, wavHandler.Size, wavHandler.SampleRate,
                volume, pitch, priority
                );
        }

        //static Audio LoadFromMP3()
        //{
        //    return new Audio();
        //}

        public void Play()
        {
            AudioManager.Instance.PlayAudio(this);
        }

        public void Pause()
        {
            AudioManager.Instance.Pause(this);
        }

        public void Resume()
        {
            AudioManager.Instance.Resume(this);
        }

        public void Stop()
        {
            AudioManager.Instance.Stop(this);
        }

        public void SetVolume(float volume)
        {
            this.volume = volume;
            AudioManager.Instance.SetVolume(this);
        }

        public void SetPitch(float pitch)
        {
            this.pitch = pitch;
            AudioManager.Instance.SetPitch(this);
        }

        public void SetLooping(bool loop)
        {
            this.loops = loop;
            AudioManager.Instance.SetLooping(this);
        }
    }
}
