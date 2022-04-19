using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;

namespace Swordfish.Core.Audio
{
    class WindowsAudio : IAudioPlayer
    {
        [DllImport("winmm.dll")]
        private static extern int mciSendString(string command, StringBuilder stringReturn, int returnLength, IntPtr hwndCallback);

        [DllImport("winmm.dll")]
        private static extern int mciGetErrorString(int errorCode, StringBuilder errorText, int errorTextSize);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        private Timer _playbackTimer;
        private Stopwatch _playStopwatch;

        private string _fileName;

        private string fileName;

        public event EventHandler PlaybackFinished;

        public bool Playing { get; private set; }
        public bool Paused { get; private set; }

        public WindowsAudio(string fileName)
        {
            this.fileName = fileName;
        }

        public Task Play()
        {
            ClearTempFiles();
            _fileName = $"\"{CheckFileToPlay(fileName)}\"";
            _playbackTimer = new Timer
            {
                AutoReset = false
            };
            _playStopwatch = new Stopwatch();
            ExecuteMsiCommand("Close All");
            ExecuteMsiCommand($"Status {_fileName} Length");
            ExecuteMsiCommand($"Play {_fileName}");
            Paused = false;
            Playing = true;
            _playbackTimer.Elapsed += HandlePlaybackFinished;
            _playbackTimer.Start();
            _playStopwatch.Start();

            return Task.CompletedTask;
        }

        public Task Pause()
        {
            if (Playing && !Paused)
            {
                ExecuteMsiCommand($"Pause {_fileName}");
                Paused = true;
                _playbackTimer.Stop();
                _playStopwatch.Stop();
                _playbackTimer.Interval -= _playStopwatch.ElapsedMilliseconds;
            }

            return Task.CompletedTask;
        }

        public Task Resume()
        {
            if (Playing && Paused)
            {
                ExecuteMsiCommand($"Resume {_fileName}");
                Paused = false;
                _playbackTimer.Start();
                _playStopwatch.Reset();
                _playStopwatch.Start();
            }
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            if (Playing)
            {
                ExecuteMsiCommand($"Stop {_fileName}");
                Playing = false;
                Paused = false;
                _playbackTimer.Stop();
                _playStopwatch.Stop();
                ClearTempFiles();
            }
            return Task.CompletedTask;
        }

        private void HandlePlaybackFinished(object sender, ElapsedEventArgs e)
        {
            Playing = false;
            PlaybackFinished?.Invoke(this, e);
            _playbackTimer.Dispose();
            _playbackTimer = null;
        }

        private Task ExecuteMsiCommand(string commandString)
        {
            var sb = new StringBuilder();

            var result = mciSendString(commandString, sb, 1024 * 1024, IntPtr.Zero);

            if (result != 0)
            {
                var errorSb = new StringBuilder($"Error executing MCI command '{commandString}'. Error code: {result}.");
                var sb2 = new StringBuilder(128);

                mciGetErrorString(result, sb2, 128);
                errorSb.Append($" Message: {sb2}");

                throw new Exception(errorSb.ToString());
            }

            if (commandString.ToLower().StartsWith("status") && int.TryParse(sb.ToString(), out var length))
                _playbackTimer.Interval = length;

            return Task.CompletedTask;
        }

        public Task SetVolume(byte percent)
        {
            // Calculate the volume that's being set
            int NewVolume = ushort.MaxValue / 100 * percent;
            // Set the same volume for both the left and the right channels
            uint NewVolumeAllChannels = ((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16);
            // Set the volume
            waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);

            return Task.CompletedTask;
        }
        private const string TempDirName = "temp";

        public static string CheckFileToPlay(string originalFileName)
        {
            var fileNameToReturn = originalFileName;
            if (originalFileName.Contains(" "))
            {
                Directory.CreateDirectory(TempDirName);
                fileNameToReturn = TempDirName + Path.DirectorySeparatorChar +
                    Path.GetFileName(originalFileName).Replace(" ", "");
                File.Copy(originalFileName, fileNameToReturn);
            }

            return fileNameToReturn;
        }

        public static void ClearTempFiles()
        {
            if (Directory.Exists(TempDirName))
                Directory.Delete(TempDirName, true);
        }
    }
}
