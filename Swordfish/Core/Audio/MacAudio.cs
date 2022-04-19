using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swordfish.Core.Audio
{
    internal class MacAudio : UnixAudio, IAudioPlayer
    {
        public MacAudio(string fileName)
        {
            this.fileName = fileName;
        }

        protected override string GetBashCommand(string fileName)
        {
            return "afplay";
        }

        public override Task SetVolume(byte percent)
        {
            if (percent > 100)
                throw new ArgumentOutOfRangeException(nameof(percent), "Percent can't exceed 100");

            var tempProcess = StartBashProcess($"osascript -e \"set volume output volume {percent}\"");
            tempProcess.WaitForExit();

            return Task.CompletedTask;
        }
    }
}
