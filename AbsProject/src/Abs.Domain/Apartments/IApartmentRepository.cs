namespace Abs.Domain.Apartments
{
    internal interface IApartmentRepository
    {
        Task<Apartment> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
