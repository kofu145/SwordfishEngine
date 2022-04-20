/*using System;
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
        private AudioPlayer audioPlayer;
        private IAudioPlayer audio_player;
        private string audio;
        private byte volume;

        /// <summary>
        /// A sound component that is set to handle the playing and handling of audio files.
        /// </summary>
        /// <param name="soundFilePath"></param>
        public Sound(string soundFilePath)
        {
            this.audioPlayer = new AudioPlayer(soundFilePath);
        }

        /// <summary>
        /// A sound component that is set to handle the playing and handling of audio files.
        /// </summary>
        /// <param name="volume">Volume value, set as a percentage between 1-100.</param>
        public Sound(string soundFilePath, byte volume)
        {
            audioPlayer = new AudioPlayer(soundFilePath);
            audioPlayer.SetVolume(volume);
            this.volume = volume;
        }

        public void Play()
        {
            audioPlayer.Play();
        }

        public void Pause()
        {
            audioPlayer.Pause();
        }

        public void Resume()
        {
            audioPlayer.Resume();
        }

        public void SetVolume(byte volume)
        {
            this.volume = volume;
            audioPlayer.SetVolume(volume);
        }

        public void Stop()
        {
            audioPlayer.Stop();
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
*/