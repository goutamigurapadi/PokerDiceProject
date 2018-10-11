using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PokerDice.Services;

namespace PokerDice
{
    /// <summary>
    /// program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {   
            using (var game = new Game())
            {
                do
                {
                    //start game
                    game.StartGame();
                } while (game.StartAgain());
            }
            
        }
    }
}
