using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Audio.OpenAL;

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
            int numChannels;
            int sampleRate;
            int bitsPerSample;
            byte[] audioData;
            int size;

            // http://soundfile.sapp.org/doc/WaveFormat/wav-sound-format.gif WAV file format
            using (var stream = File.Open(filePath, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    // Chunk ID (4 bytes)
                    if (new String(reader.ReadChars(4)) != "RIFF")
                        throw new Exception("Invalid wave file! (could not read RIFF)");
                    
                    // Chunk Size (4 bytes)
                    reader.ReadInt32();

                    // Format (4 bytes)
                    //Contains the letters "WAVE"
                    //(0x57415645 big - endian form).
                    if (new String(reader.ReadChars(4)) != "WAVE")
                        throw new Exception("Invalid wave file! (could not read WAVE)");

                    // Subchunk1ID (4 bytes)
                    //Contains the letters "fmt "
                    //(0x666d7420 big - endian form).
                    string subChunk1ID = new String(reader.ReadChars(4));

                    // Subchunk1Size (4 bytes)
                    int subChunkSize = reader.ReadInt32();

                    if (subChunk1ID == "JUNK")
                    {
                        reader.ReadBytes(subChunkSize);
                        // resetting subChunk1ID to read for "fmt "
                        subChunk1ID = new String(reader.ReadChars(4));

                        // if bext chunk exists, process that too
                        if (subChunk1ID == "bext")
                        {
                            reader.ReadBytes(reader.ReadInt32());
                            subChunk1ID = new String(reader.ReadChars(4));
                            // Subchunk1Size (4 bytes)
                            subChunkSize = reader.ReadInt32();
                        }
                    }

                    // Console.WriteLine(subChunk1ID);
                    if (subChunk1ID != "fmt ")
                        throw new Exception("Invalid wave file! (could not read fmt )");
                    

                    // AudioFormat (2 bytes)
                    // PCM = 1 (i.e. Linear quantization)
                    // Values other than 1 indicate some form of compression.
                    if (reader.ReadInt16() != 1)
                        throw new Exception("Wave files must be uncompressed to load!");

                    // NumChannels (2 bytes)
                    // Mono = 1, Stereo = 2, etc.
                    numChannels = reader.ReadInt16();

                    // SampleRate (4 bytes)
                    sampleRate = reader.ReadInt32();

                    // ByteRate (4 bytes)
                    // SampleRate * NumChannels * BitsPerSample/8
                    reader.ReadInt32();

                    // BlockAlign (2 bytes)
                    reader.ReadInt16();

                    // BitsPerSample (2 bytes)
                    bitsPerSample = reader.ReadInt16();

                    // Subchunk2ID (4 bytes)
                    //Contains the letters "data"
                    //(0x64617461 big - endian form).
                    if (new string(reader.ReadChars(4)) != "data")
                        throw new Exception("Invalid wave file! (cannot find data header)");

                    // Subchunk2Size (4 bytes)
                    // NumSamples * NumChannels * BitsPerSample/8 (num of bytes in sound data)
                    size = reader.ReadInt32();

                    // Data
                    audioData = reader.ReadBytes(size);

                }
            }
            return new Audio(audioData, numChannels, bitsPerSample, size, sampleRate, volume, pitch, priority);
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

        }

    }
}
