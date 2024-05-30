using Abs.Domain.Abstractions;

namespace Abs.Domain.Bookings
{
    public sealed record BookingRejectdDomainEvent(Guid BookingId) : IDomainEvent;
}