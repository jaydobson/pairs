using jdobson_pairs.Models;
using MediatR;

namespace jdobson_pairs.Commands
{
    public class StartGameCommand : IRequest<Game>
    {
        public int NumCards { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }        
    }
}
