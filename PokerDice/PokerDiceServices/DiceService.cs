using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerDice.Data;
using PokerDice.Services.Enums;

namespace PokerDice.Services
{
    /// <summary>
    /// Dice service.
    /// </summary>
    public class DiceService
    {
        /// <summary>
        /// Rolls the dice.
        /// </summary>
        /// <returns></returns>
        public DiceResult RollDice()
        {
            var diceResult = new DiceResult();
            var dieResult = new StringBuilder();
            var previousDieResult = string.Empty;
            for (var i = 0; i < 5; i++)
            {
                //get random dice description
                var randomDieResult =
                    GenericEnumHelper.GetEnumDescription<PokerDiceEnum>(GenericRandomPicker
                        .RandomEnumValue<PokerDiceEnum>().ToString());
                AddDiceResultToList(diceResult, randomDieResult, previousDieResult);
                diceResult.Result = dieResult.Append(randomDieResult).ToString();
                previousDieResult = randomDieResult;
            }
            diceResult.Result = dieResult.ToString();
            return diceResult;
        }

        /// <summary>
        /// Gets the hand name by roll.
        /// </summary>
        /// <param name="roll">The roll.</param>
        /// <returns></returns>
        public string GetHandNameByRoll(List<DiceResultDetails> diceResults)
        {
            //Five a kind
            if (diceResults.Count == 1 && diceResults.First().Count == 5)
            {
                return $"{PokerHandEnum.FiveOfAKind}";
            }
            //Four of a kind
            else if (diceResults.Any(s => s.Count == 4))
            {
                return $"{PokerHandEnum.FourOfAKind}";
            }
            //Full House - three of a kind and a pair
            else if (diceResults.Any(s => s.Count == 3) && diceResults.Any(s => s.Count == 2))
            {
                return $"{PokerHandEnum.FullHouse}";
            }
            //Three of a kind
            else if (diceResults.Any(s => s.Count == 3))
            {
                return $"{PokerHandEnum.ThreeOfAKind}";
            }
            //Two pair -has two pairs
            else if (diceResults.Any(s => s.Count == 2) && diceResults.Count(s => s.Count == 2) == 2)
            {
                return $"{PokerHandEnum.TwoPair}";
            }
            //one pair - has one pair
            else if (diceResults.Any(s => s.Count == 2) && diceResults.Count(s => s.Count == 2) == 1)
            {
                return $"{PokerHandEnum.OnePair}";
            }
            else if (diceResults.Count == 5)
            {
                //get dice sequence
                var pokerDiceSequence = GetEnumDescriptionInSequence();
                //Straight - all five different faces in sequence
                if (pokerDiceSequence.Contains(string.Join("", diceResults.Select(s => s.Name).ToArray())))
                {
                    return $"{PokerHandEnum.Straight}";
                }
                //Burst - assuming it is default case
                return $"{PokerHandEnum.Burst}";
            }
            //Burst - assuming it is default case
            return $"{PokerHandEnum.Burst}";
        }

        /// <summary>
        /// Rolls the dice again.
        /// </summary>
        /// <returns></returns>
        public bool RollDiceAgain()
        {
            //ask user until give correct answer
            while (true)
            {
                Console.Write("If you don't like the current hand, you can roll the dice again. Do you want to roll the dice again (yes/no)?");
                string response = Console.ReadLine()?.ToLower();
                //if response is yes, return true
                //if response no return false;
                switch (response)
                {
                    case "yes":
                        return true;
                    case "no":
                        return false;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the highest hand.
        /// </summary>
        /// <param name="hands">The hands.</param>
        /// <returns></returns>
        public string GetTheHighestHand(IEnumerable<PlayerHand> playerHands)
        {
            var sortedHands = new List<string>();
            if (playerHands.Any())
            {
                sortedHands = SortHands(playerHands.Select(s => s.HandName)).ToList();
            }
            return sortedHands.FirstOrDefault();
        }

        #region privateMethods
        /// <summary>
        /// Sorts the hands.
        /// </summary>
        /// <param name="hands">The hands.</param>
        /// <returns></returns>
        private IEnumerable<string> SortHands(IEnumerable<string> hands)
        {
            var handsList = new List<int>();
            foreach (var hand in hands)
            {
                if ((Enum.TryParse(hand, true, out PokerHandEnum handName)))
                {
                    handsList.Add((int)handName);
                }
            }
            var sortedHandsList = handsList.OrderBy(s => s);//order the hands
            return sortedHandsList.Select(s => ((PokerHandEnum)s).ToString()).ToList();
        }

        /// <summary>
        /// Adds the dice result to list.
        /// </summary>
        /// <param name="diceResult">The dice result.</param>
        /// <param name="randomDieResult">The random die result.</param>
        /// <param name="previousDierResult"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void AddDiceResultToList(DiceResult diceResult, string randomDieResult, string previousDierResult)
        {
            //if the die name is already exists, increase the count
            if (diceResult.DiceResultDetails.Any(s => s.Name == randomDieResult) &&
                randomDieResult == previousDierResult)
            {
                //filter the die result
                var thisDieResult = diceResult.DiceResultDetails.FirstOrDefault(s => s.Name == randomDieResult);
                //increase count by 1
                if (thisDieResult != null) thisDieResult.Count = thisDieResult.Count + 1;
            }
            else
            {
                //if the die name is not exists add new
                diceResult.DiceResultDetails.Add(new DiceResultDetails()
                {
                    Name = randomDieResult,
                    Count = 1
                });
            }
        }

        /// <summary>
        /// Gets the enum description in sequence.
        /// </summary>
        /// <returns></returns>
        private string GetEnumDescriptionInSequence()
        {
            var result = new StringBuilder();
            foreach (PokerDiceEnum eachEnum in Enum.GetValues(typeof(PokerDiceEnum)))
            {
                result.Append(GenericEnumHelper.GetEnumDescription<PokerDiceEnum>(eachEnum.ToString()));
            }
            return result.ToString();
        }
        #endregion
    }
}
