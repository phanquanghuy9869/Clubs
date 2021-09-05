using System;
using System.Collections.Generic;

namespace Clubs.Api.Models
{
    public class CreateClubViewModel
    {
        public string Id { get; set; }
        public IEnumerable<int> Members { get; set; }
    }
}
