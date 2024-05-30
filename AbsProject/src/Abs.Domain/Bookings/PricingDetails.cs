using Abs.Domain.Shared;

namespace Abs.Domain.Bookings
{
    public record PricingDetails(
     Money PriceForPeriod,
     Money CleaningFee,
     Money AmenitiesUpCharge,
     Money TotalPrice);
  
}