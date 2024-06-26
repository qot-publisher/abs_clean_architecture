﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abs.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangedAsync(CancellationToken cancellationToken = default);
    }
}
