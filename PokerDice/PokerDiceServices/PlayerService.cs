using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerDice.Data;
using PokerDice.Services.Enums;

namespace PokerDice.Services
{
    public class PlayerService
    {
        private readonly DiceService _diceService;

        public PlayerService()
        {
            _diceService = new DiceService();
        }

        /// <summary>
        /// Gets the number of ai players.
        /// </summary>
        public int GetNumberOfAIPlayers()
        {
            //ai players count
            int aiPlayersCount;

            //ask user until he enters right number of players
            do
            {
                //ask user to enter number of players
                Console.WriteLine("Please enter number of players between 2 to 5:");

                //get the players
                if (int.TryParse(Console.ReadLine(), out aiPlayersCount)){}
            }
            while (aiPlayersCount < 2 || aiPlayersCount > 5);
            //return count
            return aiPlayersCount;
        }

        /// <summary>
        /// Gets the dice result for human player.
        /// </summary>
        /// <returns></returns>
        public Player PlayGameForHumanPlayer()
        {
            var currentHand = string.Empty;
            var throwResult = string.Empty;
            var player = new Player() { Name = "You" };
            var allHands = new List<PlayerHand>();
            var numberOfTurns = 0;
            do
            {
                var diceResult = new DiceResult();
                //Roll the dices for human player
                diceResult = _diceService.RollDice();
                //get throw result as string
                throwResult = diceResult.Result;
                //get hand by roll
                currentHand = _diceService.GetHandNameByRoll(diceResult.DiceResultDetails);
                //add hand and result to list
                allHands.Add(new PlayerHand() { HandName = currentHand, ThrowResult = throwResult });
                //Display current hand to user
                Console.WriteLine($"{Environment.NewLine}The current hand is: {currentHand} - {throwResult} {Environment.NewLine}");
                numberOfTurns++;
            } while (numberOfTurns < 3 && _diceService.RollDiceAgain());//player only have 3 chances to play

            //if hands has more than 1 then sort list for highest hand ortherwise return the current hand
            if (allHands.Count > 1)
            {
                //get the highest hand
                var highestHand = _diceService.GetTheHighestHand(allHands);
                //add hand to player
                player.Hand = new PlayerHand() { HandName = highestHand, ThrowResult = throwResult };
                //return result
                return player;
            }
            //add player hand
            player.Hand = allHands.FirstOrDefault();
            //return player
            return player;
        }

        /// <summary>
        /// Gets the dice result for ai player.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Player> PlayGameForAIPlayer(int numberOfAiPlayers)
        {
            //null check
            if (numberOfAiPlayers <= int.MinValue)
            {
                return new List<Player>();
            }
            var players = new List<Player>();
            //loop through AI players count
            for (var i = 0; i < numberOfAiPlayers; i++)
            {
                var diceResult = new DiceResult();
                //get the dice roll
                diceResult = _diceService.RollDice();
                //get the hand by roll
                var currentHand = _diceService.GetHandNameByRoll(diceResult.DiceResultDetails);
                //add player data to players
                players.Add(new Player()
                {
                    Name = $"Player{i + 1}",
                    Hand = new PlayerHand() { HandName = currentHand, ThrowResult = diceResult.Result }
                });
            }
            //return dice results for all AI players
            return players;
        }

        /// <summary>
        /// Displays the ai players result.
        /// </summary>
        public void DisplayAIPlayersResult(List<Player> aiPlayers)
        {
            Console.Write($"AI player hands are:{Environment.NewLine}");
            foreach (var player in aiPlayers)
            {
                var handNameDescription = GenericEnumHelper.GetEnumDescription<PokerHandEnum>(player.Hand.HandName);
                Console.Write($"{player.Name} hand is: {handNameDescription} {Environment.NewLine}");
            }
        }

        /// <summary>
        /// Displays the round winner.
        /// </summary>
        /// <param name="humanPlayerResult">The human player result.</param>
        /// <param name="aiPlayersResult">The ai players result.</param>
        public void DisplayRoundWinner(Player humanPlayerResult, IEnumerable<Player> aiPlayersResult)
        {
            var players = new List<Player>();
            players.Add(humanPlayerResult);
            players.AddRange(aiPlayersResult);
            //get the winner of the game
            var winner = GetWinner(players);
            Console.WriteLine($"{Environment.NewLine}The winner is {winner.Name}");
        }
        
        #region privateMethods
        /// <summary>
        /// Gets the winner of the game.
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        private Player GetWinner(List<Player> players)
        {
            var highestHand = _diceService.GetTheHighestHand(players.Select(s => s.Hand).ToList());
            var winner = players.FirstOrDefault(s => string.Equals(s.Hand.HandName, highestHand, StringComparison.CurrentCultureIgnoreCase));
            return winner;
        }
        #endregion
    }
}
