using System;
using System.Threading;
using System.Threading.Tasks;
using Clubs.Domain.AggregateModels.ClubAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clubs.Api.CQRS.Commands
{
    public class ClubAddMemberCommandHandler : IRequestHandler<ClubAddMemberCommand, Club>
    {
        private readonly IClubRepository _clubRepository;
        private readonly ILogger<CreateClubCommandHandler> _logger;

        public ClubAddMemberCommandHandler(IClubRepository clubRepository, ILogger<CreateClubCommandHandler> logger)
        {
            _clubRepository = clubRepository;
            _logger = logger;
        }

        public async Task<Club> Handle(ClubAddMemberCommand request, CancellationToken cancellationToken)
        {
            var club = await _clubRepository.GetClubAsync(request.ClubId);
            club.AddPlayer(request.PlayerId);
            _logger.LogInformation("----- Adding member: {@PlayerId} to club - club: {@Club}", request.PlayerId, club);
            var result = _clubRepository.UpdateClub(club);
            await _clubRepository.UnitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
