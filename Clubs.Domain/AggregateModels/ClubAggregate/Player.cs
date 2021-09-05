using System;
using Clubs.Domain.SeedWorks;

namespace Clubs.Domain.AggregateModels.ClubAggregate
{
    public class Player : Entity
    {
        public int PlayerId { get; private set; }

        public Player(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
