using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp
{
    public class Team: IComparable<Team>
    {
        public string Name { get; set; }
        public List<Player> Players { get; set; }
        
        public int Points => Players.Sum(player => player.Points);
        
        public Team(string name)
        {
            Name = name;
            Players = new List<Player>();
        }

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
        
        public int CompareTo(Team other)
        {
            return Points.CompareTo(other.Points);
        }
    }
}