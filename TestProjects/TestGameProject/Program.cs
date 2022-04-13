using System;
using Swordfish.Core;

namespace TestGameProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(800, 600, "Hello world!", new TestGameState());
        }
    }
}
