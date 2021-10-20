using jdobson_pairs.Enums;
using System;
using System.Collections.Generic;

namespace jdobson_pairs.Models
{
    public class Game
    {
        public Guid Id { get; set; }

        public List<GameCard> Cards { get; } = new List<GameCard>();
        public DateTime Finish { get; set; }
        public double Score { get; set; }
        public DateTime Start { get; set; }
        public GameStatus Status { get; set;}
        public int Turns { get; set; }
        public string UserName { get; set; }
    }
}
