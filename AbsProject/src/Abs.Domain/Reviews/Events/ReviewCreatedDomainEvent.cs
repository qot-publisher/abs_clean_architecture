using Abs.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abs.Domain.Reviews.Events
{
    public sealed record ReviewCreatedDomainEvent(Guid ReviewId) : IDomainEvent;
}
