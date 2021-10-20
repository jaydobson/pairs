using System;

namespace jdobson_pairs.Models
{
    public class GameCard
    {
        public Guid Id { get; set; }
        public Guid CardId { get; set; }
        public string ImagePath { get; set; }
        public bool Matched { get; set; }
        public string Name { get; set; }        
    }
}
