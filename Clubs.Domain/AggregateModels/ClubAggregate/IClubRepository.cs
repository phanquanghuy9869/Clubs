using System;
using System.Threading.Tasks;
using Clubs.Domain.SeedWorks;

namespace Clubs.Domain.AggregateModels.ClubAggregate
{
    public interface IClubRepository : IRepository<Club>
    {
        Club AddClub(Club club);
        Task<Club> GetClubAsync(string clubId);
        Club UpdateClub(Club club);
    }
}
