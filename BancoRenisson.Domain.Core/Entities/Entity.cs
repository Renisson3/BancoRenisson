using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Envolva.Domain.Core.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateLastUpdate { get; set; }

        [IgnoreDataMember]
        public ValidationResult ValidationResult { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            DateCreation = DateTime.UtcNow;
            DateLastUpdate = DateTime.UtcNow;
            ValidationResult = new ValidationResult();
        }

        public void AddNotification(string propertyName, string errorMessage)
        {
            ValidationResult.Errors.Add(new ValidationFailure(propertyName, errorMessage));
        }

        public void AddNotification(IList<ValidationFailure> erros)
        {
            if (!(erros is null) && erros.Any())
                foreach (var erro in erros)
                    ValidationResult.Errors.Add(erro);
        }

        public void ClearNotification()
        {
            ValidationResult.Errors.Clear();
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b) => !(a == b);

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }
    }
}