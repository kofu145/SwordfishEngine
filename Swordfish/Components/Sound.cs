using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swordfish.ECS;
using Swordfish.Core.Audio;

namespace Swordfish.Components
{
    public class Sound : IComponent
    {
        private Audio audio;

        /// <summary>
        /// A sound component that is set to handle the playing and handling of audio files.
        /// </summary>
        /// <param name="soundFilePath">The filepath to the sound file you want to play.</param>
        /// <param name="volume">Volume value, set as a float between 0 and 1.</param>
        /// <param name="priority">Priority value of this sound. See <see cref="AudioPriority"/>.</param>
        public Sound(string soundFilePath, float volume=1f, AudioPriority priority=AudioPriority.Standard)
        {
            this.audio = Audio.LoadFromWAV(soundFilePath, volume, priority);
        }

        public void Play()
        {
            audio.Play();
        }

        public void Pause()
        {
            
        }

        public void Resume()
        {
            
        }

        public void SetVolume(byte volume)
        {
            
        }

        public void Stop()
        {
        }

        public void OnLoad()
        {
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
