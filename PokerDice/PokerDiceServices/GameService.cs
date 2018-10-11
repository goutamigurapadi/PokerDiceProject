using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokerDice.Data;
using PokerDice.Services.Enums;

namespace PokerDice.Services
{
    /// <summary>
    /// Game Service
    /// </summary>
    public class GameService
    {
        private readonly PlayerService _playerService;

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="GameService"/> class.
        /// </summary>
        public GameService()
        {
            _playerService = new PlayerService();
        }

        /// <summary>
        /// Plays the game.
        /// </summary>
        public void PlayGame()
        {
            //get number of ai players
            var numberOfAiPlayers = _playerService.GetNumberOfAIPlayers();
            //get dice result for human player
            var humanPlayerResult = _playerService.PlayGameForHumanPlayer();
            //get dice result for AI player
            var AIPlayersResult = _playerService.PlayGameForAIPlayer(numberOfAiPlayers);
            //display AI players result
            _playerService.DisplayAIPlayersResult(AIPlayersResult.ToList());
            //display round winner
            _playerService.DisplayRoundWinner(humanPlayerResult, AIPlayersResult);
        }
    }
}
