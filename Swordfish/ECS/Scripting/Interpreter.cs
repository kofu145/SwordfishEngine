using System;
using System.Collections.Generic;
using System.Text;
using IronPython.Runtime;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
namespace Swordfish.ECS.Scripting
{
    public class Interpreter
    {
        // create empty list for iteration
        private LinkedList<GameScript> globalScripts = new LinkedList<GameScript>();
        private LinkedList<GameScript> entityScripts = new LinkedList<GameScript>();

        // create python runtime engine
        public readonly ScriptEngine pyInterpreter = Python.CreateEngine();
       // this will be called when the interpreter is created but can be moved to a new function onInit() or similar later

        public Interpreter()
        {
            pyInterpreter.CreateScope();




        }


  
        // run scripts on every frame, also clear inactive entities
        public void Update()
        {
            /* 
             running two for loops might be a couple of operations slower, but the total amount of scripts
             will stay the same, so the performance tradeoff in onSceneChange will be worth it.
            */

            foreach(GameScript _script in globalScripts)
            {
                
                var scope = pyInterpreter.CreateScope();
                /* create scope using global vars */
                _script.script.Execute(scope);
            }

            foreach (GameScript _script in entityScripts)
            {

                var scope = pyInterpreter.CreateScope();

                /* 
                 foreach(IComponent in (?)
                 {
                 scope.SetVariable("Component data name", "Component data value");
                 }
                 */
                _script.script.Execute(scope);
            }





        }

        // this should be called BEFORE every update() call to make sure nothing is updated after a scene change
        public void onSceneChange()
        {
            foreach (GameScript _script in entityScripts)
            {
                // todo: check entity-attached scripts Guid and if active in this scene remove the script from interpreter's memory.
                //
                // if(_script.Guid something) { remove script from list }
            }
        }
       
    }
}
