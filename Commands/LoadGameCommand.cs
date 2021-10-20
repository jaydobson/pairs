using jdobson_pairs.Models;
using MediatR;
using System;

namespace jdobson_pairs.Commands
{
    public class LoadGameCommand : IRequest<Game>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
    }
}
