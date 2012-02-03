using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class ValueMaximumRequirement<T> : IRequirement<T> where T : struct, IComparable<T> {
		public string Message { get; set; }
		public ValueMaximumRequirement(T upperBound)
			: this(upperBound, false) {
		}
		public ValueMaximumRequirement(T upperBound, bool upperExclusive) {
			Limit = upperBound;
			Exclusive = upperExclusive;
			Message = "Value {1} is greater than the allowed maximum {0}";
		}

		public T Limit { get; set; }
		public bool Exclusive { get; set; }

		#region IRequirement<T> Members

		public IEnumerable<ValidationError> Check(T property) {
			if ((Exclusive && property.CompareTo(Limit) >= 0) || (!Exclusive && property.CompareTo(Limit) > 0)) {
				yield return new ValidationError { Message = String.Format(Message,Limit,property)};
			}
		}

		#endregion
	}
}