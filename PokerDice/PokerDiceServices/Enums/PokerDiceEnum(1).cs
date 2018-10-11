using System.ComponentModel;

namespace PokerDice.Services.Enums
{
    /// <summary>
    /// Poker dice enum.
    /// </summary>
    public enum PokerDiceEnum
    {
        [Description("A")]
        Ace = 1, //assume this is highest
        [Description("K")]
        King = 2,
        [Description("Q")]
        Queen = 3,
        [Description("J")]
        Jack = 4,
        [Description("10")]
        Ten = 5,
        [Description("9")]
        Nine = 6 //assume this is lowest
    }
}