using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Clubs.Api.CQRS.Commands;
using Clubs.Api.CQRS.Queries;
using Clubs.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Clubs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ClubsController> _logger;
        private readonly IClubQueries _clubQueries;

        public ClubsController(IMediator mediator, ILogger<ClubsController> logger, IClubQueries clubQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _clubQueries = clubQueries ?? throw new ArgumentException(nameof(clubQueries));
        }

        [Route("{clubId}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CreateClubViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Item(string clubId)
        {
            var club = await _clubQueries.GetClub(clubId);

            if (club == null) return NotFound();

            var responseViewModel = new CreateClubViewModel
            {
                Id = club.ClubId,
                Members = club.Members
            };

            return Ok(responseViewModel);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(CreateClubViewModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromHeader(Name = "Player-ID")] int playerId, [FromBody] CreateClubInput clubInput)
        {
            if (clubInput == null || clubInput.Name == null || playerId <= 0)
            {
                return BadRequest();
            }

            var clubName = clubInput.Name;
            var clubExisted = await _clubQueries.CheckExistedClub(clubInput.Name);
            if (clubExisted)
            {
                return Conflict(new { Error = "Club's name is already existed" });
            }

            var isPlayerAvaiable = await _clubQueries.CheckPlayerAvaiable(playerId);
            if (!isPlayerAvaiable)
            {
                return Conflict(new { Error = "Player is already member of one club" });
            }

            try
            {
                var createClubCommand = new CreateClubCommand(clubInput.Name, new List<int>() { playerId });
                var result = await _mediator.Send(createClubCommand);
                var responseViewModel = new CreateClubViewModel
                {
                    Id = result.ClubId,
                    Members = result.Players.Select(p => p.PlayerId)
                };
                return CreatedAtAction(nameof(Item), new { clubId = result.ClubId }, responseViewModel);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(new EventId(ex.HResult), ex, ex.Message);
                return Conflict();
            }
        }


        [Route("{clubId}/members")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddMember(string clubId, [FromBody] AddMemberInput memberInput)
        {
            if (string.IsNullOrEmpty(clubId) || memberInput.PlayerId <= 0)
            {
                return BadRequest();
            }

            var isPlayerAvaiable = await _clubQueries.CheckPlayerAvaiable(memberInput.PlayerId);
            if (!isPlayerAvaiable)
            {
                return Conflict(new { Error = "Player is already member of one club" });
            }

            var addMemberCommand = new ClubAddMemberCommand(clubId, memberInput.PlayerId);
            await _mediator.Send(addMemberCommand);
            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}
