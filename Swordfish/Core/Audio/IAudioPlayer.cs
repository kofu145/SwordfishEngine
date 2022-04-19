using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swordfish.Core.Audio
{
    internal interface IAudioPlayer
    {
        event EventHandler PlaybackFinished;

        bool Playing { get; }
        bool Paused { get; }

        Task Play();
        Task Pause();
        Task Resume();
        Task Stop();
        Task SetVolume(byte percent);
    }
}
