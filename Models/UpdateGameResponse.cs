using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jdobson_pairs.Models
{
    public class UpdateGameResponse
    {        
        public Game Game { get; set; }
        public bool IsMatch { get; set; }
    }
}
