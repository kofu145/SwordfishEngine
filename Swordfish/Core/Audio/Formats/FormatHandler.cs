using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swordfish.Core.Audio.Formats
{
    internal abstract class FormatHandler
    {
        internal protected int NumChannels;
        internal protected int SampleRate;
        internal protected int BitsPerSample;
        internal protected byte[] AudioData;
        internal protected int Size;

    }
}
