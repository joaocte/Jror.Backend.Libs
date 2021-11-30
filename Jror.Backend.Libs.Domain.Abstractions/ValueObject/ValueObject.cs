using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Jror.Backend.Libs.Domain.Abstractions.ValueObject
{
    public abstract class ValueObject
    {
        protected static bool EqualOperator(ValueObject obj, ValueObject otherObj)
        {
            if (obj is null ^ otherObj is null)
            {
                return false;
            }
            return obj?.Equals(otherObj) != false;
        }

        public static ValueObject Parse(string value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(ValueObject));
            if (converter != null)
            {
                return (ValueObject)converter.ConvertFromString(value);
            }
            return default;
        }

        protected static bool NotEqualOperator(ValueObject obj, ValueObject otherObj)
        {
            return !EqualOperator(obj, otherObj);
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => (x?.GetHashCode()) ?? 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}