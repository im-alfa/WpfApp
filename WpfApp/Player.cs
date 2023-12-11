using System;
using System.Linq;

namespace WpfApp
{
    public class Player
    {
        /// <summary>
        /// The name of the player
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The record of the player
        /// </summary>
        public string ResultRecord { get; set; }
        
        /// <summary>
        /// Returns the total points of the player
        /// </summary>
        public int Points => ResultRecord.Count(c => c == 'W') * 3 + ResultRecord.Count(c => c == 'D');
        
        /// <summary>
        /// Creates a new player
        /// </summary>
        /// <param name="name"></param>
        /// <param name="resultRecord"></param>
        public Player(string name, string resultRecord)
        {
            Name = name;
            ResultRecord = resultRecord;
        }

        /// <summary>
        /// Adds a result to the player's record
        ///
        /// It removes the first character of the record and adds the new result to the end
        ///
        /// Assumption: the result record is always 5 characters long
        /// </summary>
        /// <param name="outcome"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void AddResult(MatchOutcome outcome)
        {
            // remove first character
            ResultRecord = ResultRecord.Substring(1);
            ResultRecord += outcome switch
            {
                MatchOutcome.Win => "W",
                MatchOutcome.Draw => "D",
                MatchOutcome.Loss => "L",
                _ => throw new ArgumentOutOfRangeException(nameof(outcome), outcome, "Invalid mtch outcome")
            };
        }

        public override string ToString()
        {
            return $"{Name} ({Points} points)";
        }
    }
}