using Abs.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abs.Domain.Reviews
{
    public class ReviewErrors
    {
        public static Error NotEligible => new(
            "Review.NotEligible",
            "The review is not eligible because the booking is not yet completed");
    }
}
