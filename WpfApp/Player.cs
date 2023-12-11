using System;
using System.Linq;

namespace WpfApp
{
    public class Player
    {
        public string Name { get; set; }
        public string ResultRecord { get; set; }
        
        public int Points => ResultRecord.Count(c => c == 'W') * 3 + ResultRecord.Count(c => c == 'D');
        
        public Player(string name, string resultRecord)
        {
            Name = name;
            ResultRecord = resultRecord;
        }

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