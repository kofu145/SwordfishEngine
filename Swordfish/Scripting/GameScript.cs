using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
namespace Swordfish.Scripting
{
    // these classes should only be created internally by the engine at runtime. create them on your own at your own risk
    /// <summary>
    /// Class that contains the script that needs to be run. This class only contains data and is used by the interpreter. 
    /// An entity Guid field is optional if this script is attached to an entity.
    /// </summary> 
    internal class GameScript
    {

        public readonly Guid entityID;
        public readonly bool isAttached = false;
        public readonly ScriptSource script;

        /// <summary>
        /// The constructor for a script object.
        /// </summary>
        public GameScript(ScriptEngine e, string fileName)
        {
            // compile every script once to avoid redoing this
            StreamReader SR = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Swordfish.py." + fileName));
            script = e.CreateScriptSourceFromString(SR.ReadToEnd());
        }

        /// <summary>
        /// The constructor for a script object.
        /// </summary>
        public GameScript(ScriptEngine e, string fileName, Guid _ID)
        {
            // compile every script once to avoid redoing this
            StreamReader SR = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Swordfish.py." + fileName));
            script = e.CreateScriptSourceFromString(SR.ReadToEnd());
            isAttached = true;
            entityID = _ID;
        }

    } 
}
