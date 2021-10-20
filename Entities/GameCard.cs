using System;
using System.ComponentModel.DataAnnotations;

namespace jdobson_pairs.Entities
{
    public class GameCard
    {
        [Key]
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid CardId {  get; set; }
        public virtual Card Card { get; set; }
        public bool Matched { get; set; }
    }
}
