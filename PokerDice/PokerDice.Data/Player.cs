using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerDice.Data
{
    /// <summary>
    /// Player
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the hand.
        /// </summary>
        /// <value>
        /// The hand.
        /// </value>
        public PlayerHand Hand { get; set; }

        /// <summary>
        /// Gets or sets the number of ai players.
        /// </summary>
        /// <value>
        /// The number of ai players.
        /// </value>
        public int NumberOfAIPlayers { get; set; }
    }
}
