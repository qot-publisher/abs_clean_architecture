using Abs.Application.Abstractions.Messaging;
using Abs.Application.Abstractions.Time;
using Abs.Application.Exceptions;
using Abs.Domain.Abstractions;
using Abs.Domain.Apartments;
using Abs.Domain.Bookings;
using Abs.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abs.Application.Bookings.ReserveBooking
{
    internal sealed class ReserveBookingCommandHandler : ICommandHandler<ReserveBookingCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PricingService _pricingService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ReserveBookingCommandHandler(
            IUserRepository userRepository,
            IApartmentRepository apartmentRepository,
            IBookingRepository bookingRepository,
            IUnitOfWork unitOfWork,
            PricingService pricingService,
            IDateTimeProvider dateTimeProvider)
        {
            _userRepository = userRepository;
            _apartmentRepository = apartmentRepository;
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
            _pricingService = pricingService;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

            if(user is null)
            {
                return Result.Failure<Guid>(UserErrors.NotFound);
            }

            var apartment = await _apartmentRepository.GetByIdAsync(request.ApartmentId, cancellationToken);

            if(apartment is null)
            {
                return Result.Failure<Guid>(ApartmentErrors.NotFound);
            }

            // TODO: add validation to Create Method
            var duration = DateRange.Create(request.StartDate, request.EndDate);

            if(await _bookingRepository.IsOverlappingAsync(apartment, duration, cancellationToken))
            {
                return Result.Failure<Guid>(BookingErrors.Overlap);
            }

            // Reserving booking trigerring apartment changes that implements optimistic concurrency versioning
            try
            {
                var booking = Booking.Reserve(
                    apartment,
                    user.Id,
                    duration,
                    _dateTimeProvider.UtcNow,
                    _pricingService);

                _bookingRepository.Add(booking);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return booking.Id;
            }
            catch (ConcurrencyException)
            {
                return Result.Failure<Guid>(BookingErrors.Overlap);
            }

        }
    }
}
