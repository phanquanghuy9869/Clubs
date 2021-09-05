using System;
using Clubs.Domain.AggregateModels.ClubAggregate;
using MediatR;

namespace Clubs.Api.CQRS.Commands
{
    public class ClubAddMemberCommand : IRequest<Club>
    {
        public string ClubId { get; private set; }
        public int PlayerId { get; private set; }

        public ClubAddMemberCommand(string clubId, int playerId)
        {
            ClubId = clubId;
            PlayerId = playerId;
        }
    }
}
