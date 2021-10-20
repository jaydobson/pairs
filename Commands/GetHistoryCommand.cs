using jdobson_pairs.Models;
using MediatR;
using System.Collections.Generic;

namespace jdobson_pairs.Commands
{
    public class GetHistoryCommand : IRequest<IList<HistoryItem>>
    {
        public string UserId { get; set; }
    }
}
