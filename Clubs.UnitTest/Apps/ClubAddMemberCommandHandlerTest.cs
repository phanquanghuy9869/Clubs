using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clubs.Api.CQRS.Commands;
using Clubs.Domain.AggregateModels.ClubAggregate;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Clubs.UnitTest.Apps
{
    public class ClubAddMemberCommandHandlerTest
    {
        private readonly Mock<IClubRepository> _clubRepositoryMock;
        private readonly Mock<ILogger<CreateClubCommandHandler>> _loggerMock;

        public ClubAddMemberCommandHandlerTest()
        {
            _clubRepositoryMock = new Mock<IClubRepository>();
            _loggerMock = new Mock<ILogger<CreateClubCommandHandler>>();
        }

        [Fact]
        public async Task Handle_create_new_club_object()
        {
            var clubName = "FakeClubName";
            var clubId = "5e324948-6a88-40e6-8b09-578d061de816";
            var fakeClubCommand = FakeCreatClubCommand(new Dictionary<string, object>
            {
                ["name"] = clubName,
                ["playerIds"] = new List<int> { 123 }
            });

            var club = FakeClub(new Dictionary<string, object>
            {
                ["clubId"] = clubId,
                ["clubName"] = clubName
            }); ;

            _clubRepositoryMock.Setup(r => r.AddClub(It.IsAny<Club>())).Returns(club);
            _clubRepositoryMock.Setup(r => r.UnitOfWork.SaveChangesAsync(default(CancellationToken)))
                .Returns(Task.FromResult(1));

            var creatClubHandler = new CreateClubCommandHandler(_clubRepositoryMock.Object, _loggerMock.Object);
            var token = new CancellationToken();
            var result = await creatClubHandler.Handle(fakeClubCommand, token);

            Assert.NotNull(result);
            Assert.Equal(result.ClubId, clubId);
        }

        public CreateClubCommand FakeCreatClubCommand(Dictionary<string, object> args)
        {
            return new CreateClubCommand(
                    name: args != null && args.ContainsKey("name") ? (string)args["name"] : null,
                    playerId: args != null && args.ContainsKey("playerIds") ? (List<int>)args["playerIds"] : null
                );
        }

        public Club FakeClub(Dictionary<string, object> args)
        {
            return new Club(
                    clubId: args != null && args.ContainsKey("clubId") ? (string)args["clubId"] : null,
                    name: args != null && args.ContainsKey("name") ? (string)args["name"] : null
                );
        }
    }
}
