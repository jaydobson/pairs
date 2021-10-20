using jdobson_pairs.Commands;
using jdobson_pairs.Models;
using jdobson_pairs.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace jdobson_pairs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public GameController(ILogger<GameController> logger, IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _mediator = mediator;
            _userManager = userManager;
        }

        // Creates a new game
        [HttpPost("start")]
        public async Task<ActionResult<Game>> StartGame([FromBody] StartGameRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var userName = user.Email;

            var result = await _mediator.Send(new StartGameCommand() {
                 NumCards = request.NumCards, 
                 UserId = userId, 
                 UserName = userName
            });

            return Ok(result);
        }

        // Retrieves an existing game
        [HttpGet("play/{id}")]
        public async Task<ActionResult<Game>> PlayGame(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _mediator.Send(new LoadGameCommand()
            {
                Id = id,
                UserId = userId                
            });

            return Ok(result);
        }

        // Retrieves an existing game
        [HttpGet("active")]
        public async Task<ActionResult<Game>> GetActive()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _mediator.Send(new GetActiveGameCommand()
            {
                UserId = userId
            });

            return Ok(result);
        }

        // Retrieves an existing game
        [HttpPut("update")]
        public async Task<ActionResult<UpdateGameResponse>> UpdateGame([FromBody] UpdateGameRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _mediator.Send(new UpdateGameCommand()
            {
                Id = request.Id,
                UserId = userId,
                CardIds = request.CardIds,
            });

            return Ok(result);
        }

        // Retrieves history for user
        [HttpGet("history")]
        public async Task<ActionResult<List<Models.HistoryItem>>> GetHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _mediator.Send(new GetHistoryCommand()
            {
                UserId = userId
            });

            return Ok(result);
        }
    }
}
