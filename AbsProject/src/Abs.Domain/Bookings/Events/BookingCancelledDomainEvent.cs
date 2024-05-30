using Abs.Domain.Abstractions;

namespace Abs.Domain.Bookings
{
    public sealed record BookingCancelledDomainEvent(Guid BookingId) : IDomainEvent;
}