using Abs.Domain.Abstractions;
using Abs.Domain.Bookings;
using Abs.Domain.Reviews.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abs.Domain.Reviews
{
    public class Review : Entity
    {
        private Review(
            Guid id, 
            Guid apartmentId, 
            Guid bookingId, 
            Guid userId, 
            Rating rating, 
            Comment comment, 
            DateTime createdOnUtc) : base(id)
        {
            ApartmentId = apartmentId;
            BookingId = bookingId;
            UserId = userId;
            Rating = rating;
            Comment = comment;
            CreatedOnUtc = createdOnUtc;
        }

        private Review()
        {
        }

        public Guid ApartmentId { get; }

        public Guid BookingId { get; }

        public Guid UserId { get; }

        public Rating Rating { get; private set; }

        public Comment Comment { get; private set; }

        public DateTime CreatedOnUtc { get; }

        public static Result<Review> Create(
            Booking booking,
            Rating rating,
            Comment comment,
            DateTime createOnUtc)
        {
            var review = new Review(
                Guid.NewGuid(),
                booking.ApartmentId,
                booking.Id,
                booking.UserId,
                rating,
                comment,
                createOnUtc);

            review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));

            return review;
        }
    }
}
