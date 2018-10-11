using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerDice.Data
{
    /// <summary>
    /// Dice result
    /// </summary>
    public class DiceResult
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public string Result { get; set; }

        /// <summary>
        /// Gets or sets the dice result details.
        /// </summary>
        /// <value>
        /// The dice result details.
        /// </value>
        public List<DiceResultDetails> DiceResultDetails { get; set; }

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="DiceResult"/> class.
        /// </summary>
        public DiceResult()
        {
            Result = string.Empty;
            DiceResultDetails = new List<DiceResultDetails>();
        }
       
    }

    /// <summary>
    /// Dice result details.
    /// </summary>
    public class DiceResultDetails
    {
        /// <summary>
        /// Gets or sets the name of the dice.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the count of the dice.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; set; }
    }
}
