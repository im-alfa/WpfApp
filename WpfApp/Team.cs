using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp
{
    public class Team: IComparable<Team>
    {
        /// <summary>
        /// The name of the team
        /// </summary>
        
        public string Name { get; set; }
        /// <summary>
        /// The players in the team
        /// </summary>
        public List<Player> Players { get; set; }
        
        /// <summary>
        /// Returns the total points of all players in the team
        /// </summary>
        public int Points => Players.Sum(player => player.Points);
        
        /// <summary>
        /// Creates a new team
        /// </summary>
        /// <param name="name"></param>
        public Team(string name)
        {
            Name = name;
            Players = new List<Player>();
        }
        
        /// <summary>
        /// Adds an N number of players to the team
        /// </summary>
        /// <param name="players"></param>
        public void AddPlayers(params Player[] players)
        {
            foreach (var player in players)
            {
                Players.Add(player);
            }
        }
        
        public override string ToString()
        {
            return $"{Name} ({Points})";
        }
        
        /// <summary>
        /// Compares the points of the team to another team
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Team other)
        {
            return Points.CompareTo(other.Points);
        }
    }
}