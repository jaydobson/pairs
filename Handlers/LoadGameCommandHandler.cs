using AutoMapper;
using jdobson_pairs.Commands;
using jdobson_pairs.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Web.Http;

namespace jdobson_pairs.Handlers
{
    public class LoadGameCommandHandler : IRequestHandler<LoadGameCommand, Models.Game>
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<LoadGameCommandHandler> _logger;
        private readonly IMapper _mapper;

        public LoadGameCommandHandler(ApplicationDbContext db, ILogger<LoadGameCommandHandler> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Models.Game> Handle(LoadGameCommand request, CancellationToken cancellationToken)
        {            
            var result = await _db.Games.Where(g => g.UserId == request.UserId && g.Id == request.Id && g.Status == Enums.GameStatus.InProgress)                
                .Include(g => g.Cards)
                .ThenInclude(x => x.Card)
                .OrderByDescending(s => s.Start)
                .FirstOrDefaultAsync();
         
            if (result == null)
            {
                throw new HttpResponseException(new System.Net.Http.HttpResponseMessage() {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ReasonPhrase = "There are no active games for the current user"
                });
            }
            
            return _mapper.Map<Models.Game>(result);
        }
    }
}
