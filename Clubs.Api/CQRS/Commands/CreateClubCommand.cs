using System;
using System.Collections.Generic;
using Clubs.Domain.AggregateModels.ClubAggregate;
using MediatR;

namespace Clubs.Api.CQRS.Commands
{
    public class CreateClubCommand : IRequest<Club>
    {
        public string Name { get; private set; }
        private readonly List<int> _playerIds;
        public IEnumerable<int> PlayerIds => _playerIds;

        public CreateClubCommand(string name, List<int> playerId)
        {
            Name = name;
            _playerIds = playerId;
        }
    }
}
