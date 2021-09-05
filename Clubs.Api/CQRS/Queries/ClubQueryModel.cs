using System;
using System.Collections.Generic;

namespace Clubs.Api.CQRS.Queries
{
    public class ClubQueryModel
    {
        public int Id { get; set; }
        public string ClubId { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> Members { get; set; }
    }
}
