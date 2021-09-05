﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clubs.Domain.SeedWorks
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
