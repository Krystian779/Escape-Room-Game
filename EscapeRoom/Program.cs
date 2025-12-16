using NAudio.Wave;
using System;
using System.Globalization;
using System.Media;
using System.Threading.Tasks;

namespace EscapeRoom
{

    internal class Program
    {
        private static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}