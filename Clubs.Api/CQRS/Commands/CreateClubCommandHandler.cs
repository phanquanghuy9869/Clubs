using System;
using System.Threading;
using System.Threading.Tasks;
using Clubs.Domain.AggregateModels.ClubAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clubs.Api.CQRS.Commands
{
    public class CreateClubCommandHandler : IRequestHandler<CreateClubCommand, Club>
    {
        private readonly IClubRepository _clubRepository;
        private readonly ILogger<CreateClubCommandHandler> _logger;

        public CreateClubCommandHandler(IClubRepository clubRepository, ILogger<CreateClubCommandHandler> logger)
        {
            _clubRepository = clubRepository ?? throw new ArgumentNullException(nameof(clubRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Club> Handle(CreateClubCommand request, CancellationToken cancellationToken)
        {
            var clubId = Guid.NewGuid().ToString();
            var club = new Club(clubId,request.Name);
            foreach (var playerId in request.PlayerIds)
            {
                club.AddPlayer(playerId);
            }
            _logger.LogInformation("----- Creating club - club: {@Club}", club);
            var result = _clubRepository.AddClub(club);
            await _clubRepository.UnitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
