using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using PokerDice.Data;
using PokerDice.Services;

namespace PokerDice
{
    /// <inheritdoc />
    /// <summary>
    /// Game class
    /// </summary>
    public class Game: IDisposable
    {
        //game service interface
        private readonly GameService _gameService;

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            _gameService = new GameService();
        }
        /// <summary>
        /// Starts the game.
        /// </summary>
        public void StartGame()
        {
            //welcome message
            Console.WriteLine("Welcome to Poker Hand Game\r\n");
            //play game
            _gameService.PlayGame();
        }

        /// <summary>
        /// Start game again.
        /// </summary>
        /// <returns></returns>
        public bool StartAgain()
        {
            //ask user until give correct answer
            while (true)
            {
                Console.Write($"{Environment.NewLine}Hope you enjoyed the game. Do you want to play again (yes/no)?");
                string response = Console.ReadLine()?.ToLower();
                //if response is yes, return true
                //if response no return false;
                switch (response)
                {
                    case "yes":
                        return true;
                    case "no":
                        return false;
                }
            }
        }

        #region Dispose

        // Flag: Has Dispose already been called?
        bool _disposed = false;
        // Instantiate a SafeHandle instance.
        readonly SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _handle.Dispose();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            _disposed = true;
        }


        #endregion
    }
}
