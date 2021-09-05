using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Clubs.Api.CQRS.Queries
{
    public class ClubQueries : BaseQuery, IClubQueries
    {
        public ClubQueries(IDbConnection con) : base(con)
        {
        }

        public async Task<bool> CheckExistedClub(string clubName)
        {
            var query = "Select Count(1) From Clubs Where Name = @clubName";
            var result = await _dbConnection.QueryFirstAsync<int>(query, new { clubName });
            return (result == 1);
        }

        public async Task<bool> CheckPlayerAvaiable(int playerId)
        {
            var query = "SELECT COUNT(1) FROM Players WHERE PlayerId = @playerId AND ClubId IS NOT NULL AND ClubId > 0";
            var count = await _dbConnection.QueryFirstAsync<int>(query, new { playerId });
            return (count == 0);
        }

        public async Task<ClubQueryModel> GetClub(string clubId)
        {
            var clubQuery = "Select Id, Name, ClubId from Clubs Where ClubId = @clubId";
            var club = await _dbConnection.QueryFirstOrDefaultAsync<ClubQueryModel>(clubQuery, new { clubId });

            if (club == null) return null;

            var playerQuery = "Select PlayerId from Players Where ClubId = @Id";
            var players = await _dbConnection.QueryAsync<int>(playerQuery, new { Id = club.Id });
            club.Members = players;
            return club;
        }
    }
}
