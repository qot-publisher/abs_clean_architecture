using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abs.Application.Abstractions.Time
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
