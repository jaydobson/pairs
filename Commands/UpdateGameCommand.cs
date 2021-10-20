using jdobson_pairs.Models;
using MediatR;
using System;

namespace jdobson_pairs.Commands
{
    public class UpdateGameCommand : IRequest<UpdateGameResponse>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid[] CardIds { get; set; }
    }
}
