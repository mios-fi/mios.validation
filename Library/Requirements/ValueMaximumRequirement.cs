using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class ValueMaximumRequirement<TValue> : AbstractRequirement<TValue> where TValue : struct, IComparable<TValue> {
		public string Message { get; set; }
		public ValueMaximumRequirement(TValue upperBound)
			: this(upperBound, false) {
		}
		public ValueMaximumRequirement(TValue upperBound, bool upperExclusive) {
			Limit = upperBound;
			Exclusive = upperExclusive;
			Message = "ValueMaximum";
		}

		public TValue Limit { get; set; }
		public bool Exclusive { get; set; }

		public override IEnumerable<ValidationError> Check(TValue property) {
			if ((Exclusive && property.CompareTo(Limit) >= 0) || (!Exclusive && property.CompareTo(Limit) > 0)) {
				yield return new ValidationError { Message = String.Format(Message,Limit,property)};
			}
		}
	}
}