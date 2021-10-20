using System;
using System.ComponentModel.DataAnnotations;

namespace jdobson_pairs.Entities
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }        
   }
}
