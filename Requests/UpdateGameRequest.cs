using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jdobson_pairs.Requests
{
    public class UpdateGameRequest
    {
        public Guid Id { get; set;}

        public Guid[] CardIds { get; set; }
    }
}
