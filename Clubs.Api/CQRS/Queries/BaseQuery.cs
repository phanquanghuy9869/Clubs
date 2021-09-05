using System;
using System.Data;

namespace Clubs.Api.CQRS.Queries
{
    public class BaseQuery
    {
        protected readonly IDbConnection _dbConnection;

        public BaseQuery(IDbConnection con)
        {
            _dbConnection = con;
        }
    }
}
