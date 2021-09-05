using System;
using System.Linq;
using Clubs.Domain.AggregateModels.ClubAggregate;
using Xunit;

namespace Clubs.UnitTest.Domain
{
    public class ClubAggregateTest
    {
        public ClubAggregateTest()
        {
        }

        [Fact]
        public void Create_club_success()
        {
            var clubId = "5e324948-6a88-40e6-8b09-578d061de816";
            var clubName = "Fake Name";

            var club = new Club(clubId, clubName);

            Assert.NotNull(club);
            Assert.Equal(club.ClubId, clubId);
            Assert.Equal(club.Name, clubName);
            Assert.NotNull(club.Players);
        }

        [Fact]
        public void Club_add_player_success()
        {
            var club = new Club("Fake Id", "Fake name");
            var playerId = 123;
            club.AddPlayer(playerId);

            Assert.Equal(club.Players.FirstOrDefault().PlayerId, playerId);
        }
    }
}
