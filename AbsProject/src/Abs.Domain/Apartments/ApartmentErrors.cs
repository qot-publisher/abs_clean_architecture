using Abs.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abs.Domain.Apartments
{
    public static class ApartmentErrors
    {
        public static Error NotFound => new(
            "Apartment.NotFound", 
            "The apartment with the specified identifier not found");
    }
}
