﻿namespace Abs.Domain.Apartments
{
    public interface IApartmentRepository
    {
        Task<Apartment> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
