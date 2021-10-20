using AutoMapper;
using jdobson_pairs.Commands;
using jdobson_pairs.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Web.Http;

namespace jdobson_pairs.Handlers
{
    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, Models.UpdateGameResponse>
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<UpdateGameCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateGameCommandHandler(ApplicationDbContext db, ILogger<UpdateGameCommandHandler> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Models.UpdateGameResponse> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            var isMatch = false;
            var currentGame = await _db.Games.Where(g => g.UserId == request.UserId && g.Id == request.Id && g.Status == Enums.GameStatus.InProgress)
                .Include(g => g.Cards)
                .ThenInclude(x => x.Card)
                .OrderByDescending(s => s.Start)
                .FirstOrDefaultAsync();

            // TODO: Consider validator on UpdateRequest to verify 2 items in array

            var card1 = currentGame.Cards.Where(c => c.Id == request.CardIds[0]).FirstOrDefault();
            var card2 = currentGame.Cards.Where(c => c.Id == request.CardIds[1]).FirstOrDefault();

            if (currentGame == null)
            {
                throw new HttpResponseException(new System.Net.Http.HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    ReasonPhrase = "There are no active games for the current user"
                });
            }

            currentGame.Turns += 1;

            if (card1?.CardId == card2.CardId) { 
                currentGame.Cards.FindAll(c => request.CardIds.Contains(c.Id)).ToList()?.ForEach(i => i.Matched = true);
                isMatch = true;
            }
            
            if (!currentGame.Cards.Any(c => !c.Matched)) {
                currentGame.Finish = DateTime.UtcNow;
                currentGame.Status = Enums.GameStatus.Completed;
            }

            await _db.SaveChangesAsync();

            var result = new Models.UpdateGameResponse()
            {
                Game = _mapper.Map<Models.Game>(currentGame),
                IsMatch = isMatch
            };

            return result;
        }
    }
}
