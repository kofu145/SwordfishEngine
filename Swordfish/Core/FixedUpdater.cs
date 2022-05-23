using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Swordfish.Core
{
    internal class FixedUpdater
    {
        private float fixedTimestep;
        private Stopwatch stopwatch;
        private Thread fixedUpdateThread;
        private float nextUpdate;
        private GameTime gameTime;

        public FixedUpdater(float fixedTimestep) {
            this.fixedTimestep = fixedTimestep;
        }

        public void BeginUpdater()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            fixedUpdateThread = new Thread(UpdateLoop);
            fixedUpdateThread.Start();
            this.gameTime = new GameTime(0, 0);
            nextUpdate = 0;
        }
        
        private void UpdateLoop()
        {
            while (true)
            {
                if (stopwatch.Elapsed.TotalSeconds > nextUpdate)
                {
                    gameTime.UpdateTime(fixedTimestep);
                    var entities = GameStateManager.Instance.GetScreen().GameScene.Entities;


                    for (int i=0; i < entities.Count; i++)
                    {
                        if (i + 1 <= entities.Count && entities[i] != null)
                        {
                            var components = entities[i].GetComponents();
                            foreach (var component in components)
                                component.FixedUpdate(gameTime);
                        }
                    }

                    nextUpdate = (float)stopwatch.Elapsed.TotalSeconds + fixedTimestep;
                }
                
            }
            

        }

    }
}
