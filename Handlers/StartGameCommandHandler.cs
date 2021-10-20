using AutoMapper;
using static jdobson_pairs.Helpers.ListHelpers;
using jdobson_pairs.Commands;
using jdobson_pairs.Data;
using jdobson_pairs.Entities;
using jdobson_pairs.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace jdobson_pairs.Handlers
{
    public class StartGameCommandHandler : IRequestHandler<StartGameCommand, Models.Game>
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<StartGameCommandHandler> _logger;
        private readonly IMapper _mapper;

        public StartGameCommandHandler(ApplicationDbContext db, ILogger<StartGameCommandHandler> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Models.Game> Handle(StartGameCommand request, CancellationToken cancellationToken)
        {
            var numPairs = request.NumCards / 2;
            var gameCards = new List<GameCard>();

            // Retrieve available cards
            var cards = await _db.Cards.ToListAsync();
            
            Random rnd = new Random();

            for (int i = 0; i < numPairs; i++)
            {                
                int random = rnd.Next(0, cards.Count() - 1);
                var card = cards[random];

                gameCards.Add(new GameCard() { CardId = card.Id });
                gameCards.Add(new GameCard() { CardId = card.Id }); // Adding second time to make pair
            }
            
            gameCards.Shuffle();

            var newGame = new Game()
            {
              Cards = gameCards, 
              Start = DateTime.UtcNow, 
              UserId = request.UserId, 
              UserName = request.UserName,
              Status = Enums.GameStatus.InProgress 
            };

            _db.Add(newGame);
            await _db.SaveChangesAsync();

            _logger.LogInformation(String.Format("Successfully started new game {0}", newGame.Id));

            var result = _mapper.Map<Models.Game>(newGame);
            return result;
        }
    }
}
