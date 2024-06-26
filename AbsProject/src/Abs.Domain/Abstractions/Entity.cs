using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abs.Domain.Abstractions
{
    public abstract class Entity : IEquatable<Entity>
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        protected Entity(Guid id) 
        {
            Id = id;
        }

        protected Entity()
        {
        }

        public Guid Id { get; init; }

        public bool Equals(Entity? other)
        {
            return other !=null &&
                other.Id.Equals(this.Id);
        }

        public override bool Equals(object? obj)
        {
            if(obj is Entity entity)
            {
                return Equals(entity);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents;
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
