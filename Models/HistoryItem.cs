using System;

namespace jdobson_pairs.Models
{
    public class HistoryItem
    {        
        public DateTime Finish { get; set; }
        public DateTime Start { get; set; }
        public int Turns { get; set; }
        public int NumCards { get; set; }
    }
}
