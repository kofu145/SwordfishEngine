using System;
using System.Diagnostics;

namespace Swordfish.Core
{
    public class GameTime
    {
        /// <summary>
        /// The time between the last frame to the current one.
        /// </summary>
        public TimeSpan deltaTime { get; private set; }
        /// <summary>
        /// Total time from the start of the game.
        /// </summary>
        public TimeSpan totalTime { get; private set; }

        // more accurate measurement of total gametime
        private Stopwatch stopwatch;

        //TODO: helper methods as needed

        internal GameTime(double initialDeltaTime, double initialTotalTime)
        {
            deltaTime = TimeSpan.FromSeconds(initialDeltaTime);
            totalTime = TimeSpan.FromSeconds(initialTotalTime);
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        internal void UpdateTime(double seconds)
        {
            deltaTime = TimeSpan.FromSeconds(seconds);
            totalTime = stopwatch.Elapsed;
        }

    }
}
