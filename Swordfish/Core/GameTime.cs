using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swordfish.Core
{
    public class GameTime
    {
        public TimeSpan deltaTime { get; private set; }
        public TimeSpan totalTime { get; private set; }

        //TODO: helper methods as needed

        internal GameTime(double initialDeltaTime, double initialTotalTime)
        {
            deltaTime = TimeSpan.FromSeconds(initialDeltaTime);
            totalTime = TimeSpan.FromSeconds(initialTotalTime);
        }

        internal void UpdateTime(double seconds)
        {
            deltaTime = TimeSpan.FromSeconds(seconds);
            totalTime += deltaTime;
        }

    }
}
