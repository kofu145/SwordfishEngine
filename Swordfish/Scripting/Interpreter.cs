using System;
using System.Collections.Generic;
using System.Text;
using IronPython.Runtime;
using IronPython.Hosting;
using Swordfish.ECS;
using Swordfish.Core;
using Swordfish.Components.UI;
using Swordfish.Components;
using System.Threading;
using Microsoft.Scripting.Hosting;
namespace Swordfish.Scripting
{
    // internal class for python commands given by the engine
    public class SystemPy
    {
       public IComponent getComponent(String type, Entity e)
        {
            switch(type)
            {
                case "ContextButton":
                    return e.GetComponent<ContextButton>();

                case "Label":
                    return e.GetComponent<Label>();
                    break;
                case "Text":
                    return null; // e.GetComponent<Text>();

                case "Camera":
                    return e.GetComponent<TextBox>();

                case "Sprite":
                    return e.GetComponent<Sprite>();

                case "TextBox":
                    return e.GetComponent<TextBox>();

            }
            return null;
        }
    }
    public class Interpreter
    {
        private static SystemPy sys = new SystemPy();
        // singleton interpreter class
        private static Interpreter instance;
        // create empty list for iteration
        private LinkedList<GameScript> globalScripts = new LinkedList<GameScript>();
        private LinkedList<GameScript> entityScripts = new LinkedList<GameScript>();
        private bool removeScript;
        private GameScript removedScript;
        // create python runtime engine
        private static ScriptEngine pyInterpreter = Python.CreateEngine();
        // this will be called when the interpreter is created but can be moved to a new function onInit() or similar later
        public static Interpreter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Interpreter();
                    pyInterpreter.CreateScope();
                }
                return instance;
            }
        }
        // run scripts on every frame, also clear inactive entities
        public void Update()
        {
            new Thread(updateThread).Start();
        }
        public void updateThread()
        {
            foreach (GameScript _script in globalScripts)
            {

                var scope = pyInterpreter.CreateScope();
                /* create scope using global vars */
                _script.script.Execute(scope);
            }

            foreach (GameScript _script in entityScripts)
            {

                var scope = pyInterpreter.CreateScope();

                var currentScreen = GameStateManager.Instance.GetScreen();

                foreach (Guid id in _script.entityID)
                {
                    int z = 0;
                    foreach (Entity i in currentScreen.GameScene.Entities)
                    {
                        if (i.id == id)
                        {
                            // add entity to script, numbering so it can be easily accessed without knowing the Guid
                            addVars(scope);
                            scope.SetVariable("Entity" + z, i);
                        }
                    }
                    z++;
                }
                    _script.script.Execute(scope);             
            }
        }

            // returns true if script is successfully created
            public bool createScript(string internalFilePath)
            {
                try
                {
                    globalScripts.AddLast(new GameScript(pyInterpreter, internalFilePath));
                } catch (Exception Exc)
                {
                    Console.WriteLine(Exc.ToString());
                    return false;
                }

                return true;
            }
            // returns true if script with entity array is successfully created
            // usage: createScript("filepath", create new Entity[numOfAttachedEnts] { ents } )
            public bool createScript(string internalFilePath, Entity[] e)
            {

                try
                {
                    List<Guid> guids = new List<Guid>();
                    foreach (Entity z in e)
                    {
                        guids.Add(z.id);
                    }
                    entityScripts.AddLast(new GameScript(pyInterpreter, internalFilePath, guids.ToArray()));
                } catch (Exception Exc)
                {
                    Console.WriteLine(Exc.ToString());
                    return false;
                }

                return true;
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
            // instant legacy code, probably do this once and then just extend this scope every time we load a script rather than doing this every time
            internal void addVars(ScriptScope e)
            {
            e.SetVariable("System", sys);

            }
        }
    }

