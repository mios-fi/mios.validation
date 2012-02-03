using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class ValueMinimumRequirement<T> : IRequirement<T> where T : struct, IComparable<T> {
		public string Message { get; set; }
		public ValueMinimumRequirement(T lowerBound)
			: this(lowerBound, false) {
		}
		public ValueMinimumRequirement(T lowerBound, bool lowerExclusive) {
			Limit = lowerBound;
			Exclusive = lowerExclusive;
			Message = "Value {1} is lower than the allowed minimum {0}";
		}

		public T Limit { get; set; }
		public bool Exclusive { get; set; }

		#region IRequirement<T> Members

		public IEnumerable<ValidationError> Check(T property) {
			if ((Exclusive && property.CompareTo(Limit) <= 0) || (!Exclusive && property.CompareTo(Limit) < 0)) {
				yield return new ValidationError { Message = String.Format(Message,Limit,property)};
			}
		}

		#endregion
	}
}