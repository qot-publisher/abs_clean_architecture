using Abs.Domain.Abstractions;

namespace Abs.Domain.Bookings
{
    public sealed record BookingReservedDomainEvent(Guid BookingId) : IDomainEvent;
}