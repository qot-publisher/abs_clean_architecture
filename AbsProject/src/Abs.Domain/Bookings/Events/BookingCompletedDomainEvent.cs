using Abs.Domain.Abstractions;

namespace Abs.Domain.Bookings
{
    public sealed record BookingCompletedDomainEvent(Guid BookingId) : IDomainEvent;
}