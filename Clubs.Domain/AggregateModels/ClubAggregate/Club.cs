using System;
using System.Collections.Generic;
using Clubs.Domain.SeedWorks;

namespace Clubs.Domain.AggregateModels.ClubAggregate
{
    public class Club : Entity, IAggregateRoot
    {
        public string ClubId { get; private set; }
        public string Name { get; private set; }
        private List<Player> _players;
        public IEnumerable<Player> Players => _players.AsReadOnly();

        public Club()
        {
            _players = new List<Player>();
        }

        public Club(string clubId, string name) : this()
        {
            ClubId = clubId;
            Name = name;
        }

        public void AddPlayer(int playerId)
        {
            _players.Add(new Player(playerId));
        }
    }
}
