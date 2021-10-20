using jdobson_pairs.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace jdobson_pairs.Entities
{
    public class Game
    {
        [Key]
        public Guid Id { get; set; }

        public List<GameCard> Cards { get; set; } = new List<GameCard>();
        public DateTime Finish { get; set; }
        public double Score { get; set; }
        public GameStatus Status { get; set; }
        public DateTime Start { get; set; }
        public string UserId { get; set; }        
        public string UserName {  get; set; }
        public int Turns { get; set; }
    }
}
