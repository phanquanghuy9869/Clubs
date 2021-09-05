using System;
using System.Threading.Tasks;

namespace Clubs.Api.CQRS.Queries
{
    public interface IClubQueries
    {
        Task<bool> CheckExistedClub(string clubId);
        Task<bool> CheckPlayerAvaiable(int playerId);
        Task<ClubQueryModel> GetClub(string clubId);
    }
}
