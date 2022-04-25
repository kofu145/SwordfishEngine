using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swordfish.Core.Audio.Formats
{
    internal class WavHandler : FormatHandler
    {
        private WavHandler(byte[] audioData, int numChannels, int bitsPerSample, int size, int sampleRate)
        {
            AudioData = audioData;
            NumChannels = numChannels;
            BitsPerSample = bitsPerSample;
            Size = size;
            SampleRate = sampleRate;
        }

        private static void CheckRIFFHeader(BinaryReader reader)
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

            
        }

        internal static WavHandler Read(FileStream stream, BinaryReader reader)
        {
            CheckRIFFHeader(reader);

            int? numChannels = null;
            int? sampleRate = null;
            int? bitsPerSample = null;
            #nullable enable
            byte[]? audioData = null;
            int? size = null;
            bool haveFmt = false;
            bool haveWave = false;
            /*
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
            }*/

            // cache stream length, since getting it every iteration is slow
            var streamLength = stream.Length;
            while (stream.Position < streamLength)
            {
                // Subchunk1ID (4 bytes)
                //Contains the letters "fmt "
                //(0x666d7420 big - endian form).

                string subChunk1ID = new String(reader.ReadChars(4));
                //Console.WriteLine(subChunk1ID);

                // Subchunk1Size (4 bytes)
                
                int subChunkSize = reader.ReadInt32();

                if (subChunk1ID == "fmt ")
                {
                    // Console.WriteLine("found fmt!");
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

                    haveFmt = true;
                }

                else if (subChunk1ID == "data")
                {
                    // Subchunk1Size (4 bytes)
                    // NumSamples * NumChannels * BitsPerSample/8 (num of bytes in sound data)
                    size = subChunkSize;
                    // Data
                    audioData = reader.ReadBytes((int)subChunkSize);
                    haveWave = true;

                    break;
                }

                else
                {
                    // just ignore anything we don't care about (not fmt or data)
                    reader.ReadBytes((int)subChunkSize);
                }

            }

            // Console.WriteLine(subChunk1ID);
            if (!haveFmt)
                throw new Exception("Invalid wave file! (could not read fmt )");

            // Subchunk2ID (4 bytes)
            //Contains the letters "data"
            //(0x64617461 big - endian form).
            if (!haveWave)
                throw new Exception("Invalid wave file! (cannot find data header)");

            return new WavHandler(audioData, (int)numChannels, (int)bitsPerSample, (int)size, (int)sampleRate);
        }

    }
}
