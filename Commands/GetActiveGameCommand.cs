using jdobson_pairs.Models;
using MediatR;

namespace jdobson_pairs.Commands
{
    public class GetActiveGameCommand : IRequest<Game>
    {
        public string UserId { get; set; }
    }
}
