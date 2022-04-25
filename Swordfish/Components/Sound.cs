using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swordfish.ECS;
using Swordfish.Core.Audio;
using System.IO;

namespace Swordfish.Components
{
    // TODO: may just derive straight from audio? and treat everything inherited

    /// <summary>
    /// A sound component that is set to handle the playing and handling of audio files.
    /// </summary>
    public class Sound : Component
    {
        private Audio audio;

        public float Volume { get { return audio.volume; } set { SetVolume(value); } }
        public float Pitch { get { return audio.pitch; } set { SetPitch(value); } }
        public bool Loops { get { return audio.loops; } set { SetLooping(value); } }

        /// <summary>
        /// A sound component that is set to handle the playing and handling of audio files.
        /// </summary>
        /// <param name="soundFilePath">The filepath to the sound file you want to play.</param>
        /// <param name="volume">Volume value, set as a float between 0 and 1.</param>
        /// <param name="priority">Priority value of this sound. See <see cref="AudioPriority"/>.</param>
        public Sound(string soundFilePath, float volume=1f, float pitch=1f, AudioPriority priority=AudioPriority.Standard)
        {
            switch (Path.GetExtension(soundFilePath))
            {
                case ".wav":
                    this.audio = Audio.LoadFromWAV(soundFilePath, volume, pitch, priority);
                    break;
                default:
                    throw new Exception("The specified audio file isn't supported!");
                    break;
            }
        }

        public void Play()
        {
            audio.Play();
        }

        public void Pause()
        {
            audio.Pause();
        }

        public void Resume()
        {
            audio.Resume();
        }

        public void Stop()
        {
            audio.Stop();
        }

        public void SetVolume(float volume)
        {
            audio.SetVolume(volume);
        }

        public void SetPitch(float pitch)
        {
            audio.SetPitch(pitch);
        }

        public void SetLooping(bool loops)
        {
            audio.SetLooping(loops);
        }

        public override void OnLoad()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
