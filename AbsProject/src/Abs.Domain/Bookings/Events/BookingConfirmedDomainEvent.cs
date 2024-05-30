using Abs.Domain.Abstractions;

namespace Abs.Domain.Bookings
{
    public sealed record BookingConfirmedDomainEvent(Guid BookingId) : IDomainEvent;
}