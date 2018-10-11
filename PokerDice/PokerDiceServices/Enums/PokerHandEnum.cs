using System.ComponentModel;

namespace PokerDice.Services.Enums
{
    /// <summary>
    /// Poker hand enum.
    /// </summary>
    public enum PokerHandEnum
    {
        [Description("Five of a kind")]
        FiveOfAKind = 1, //this is best
        [Description("Four of a kind")]
        FourOfAKind = 2,
        [Description("Full house")]
        FullHouse = 3,
        [Description("Straight")]
        Straight = 4,
        [Description("Three of a kind")]
        ThreeOfAKind = 5,
        [Description("Two pair")]
        TwoPair = 6,
        [Description("One pair")]
        OnePair = 7,
        [Description("Burst")]
        Burst = 8,
    }
}
