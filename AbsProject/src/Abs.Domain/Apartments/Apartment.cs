using Abs.Domain.Abstractions;
using Abs.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abs.Domain.Apartments
{
    public sealed class Apartment : Entity
    {
        public Apartment(
            Guid id, 
            Name name, 
            Description description, 
            Address address, 
            Money price, 
            Money cleaningFee, 
            DateTime? lastBookedOnUtc, 
            List<Amenity> amenities) 
            : base(id)
        {
            Name = name;
            Description = description;
            Address = address;
            Price = price;
            CleaningFee = cleaningFee;
            LastBookedOnUtc = lastBookedOnUtc;
            Amenities = amenities;
        }

        private Apartment() : base()
        {
        }

        public Name Name { get; private set; }

        public Description Description { get; private set; }

        public Address Address { get; private set; }

        public Money Price { get; private set; }

        public Money CleaningFee { get; private set; }

        public DateTime? LastBookedOnUtc { get; internal set; }

        public List<Amenity> Amenities { get; private set; } = [];
    }
}
