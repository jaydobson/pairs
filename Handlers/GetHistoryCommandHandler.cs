using AutoMapper;
using jdobson_pairs.Commands;
using jdobson_pairs.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using jdobson_pairs.Models;

namespace jdobson_pairs.Handlers
{
    public class GetHistoryCommandHandler : IRequestHandler<GetHistoryCommand, IList<HistoryItem>>
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<GetHistoryCommandHandler> _logger;
        private readonly IMapper _mapper;

        public GetHistoryCommandHandler(ApplicationDbContext db, ILogger<GetHistoryCommandHandler> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IList<HistoryItem>> Handle(GetHistoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _db.Games.Where(g => g.UserId == request.UserId && g.Status == Enums.GameStatus.Completed)
                .Include(g => g.Cards)
                .OrderByDescending(s => s.Start)
                .ToListAsync();

            if (result == null)
            {
                return null;
            }

            return _mapper.Map<List<Entities.Game>, List<HistoryItem>>(result);
        }
    }
}
