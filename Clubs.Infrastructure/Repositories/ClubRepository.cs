using System;
using System.Linq;
using System.Threading.Tasks;
using Clubs.Domain.AggregateModels.ClubAggregate;
using Clubs.Domain.SeedWorks;
using Clubs.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Clubs.Infrastructure.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private readonly ClubDbContext _dbContext;
        public IUnitOfWork UnitOfWork => _dbContext;

        public ClubRepository(ClubDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Club AddClub(Club club)
        {
            return _dbContext.Clubs.Add(club).Entity;
        }

        public Club UpdateClub(Club club)
        {
            return _dbContext.Clubs.Update(club).Entity;
        }

        public async Task<Club> GetClubAsync(string clubId)
        {
            return await _dbContext.Clubs.FirstOrDefaultAsync(x => x.ClubId == clubId);
        }
    }
}
