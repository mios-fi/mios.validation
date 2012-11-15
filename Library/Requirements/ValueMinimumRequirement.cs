using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class ValueMinimumRequirement<TValue> : AbstractRequirement<TValue> where TValue : struct, IComparable<TValue> {
		public string Message { get; set; }
		public ValueMinimumRequirement(TValue lowerBound)
			: this(lowerBound, false) {
		}
		public ValueMinimumRequirement(TValue lowerBound, bool lowerExclusive) {
			Limit = lowerBound;
			Exclusive = lowerExclusive;
			Message = "ValueMinimum";
		}

		public TValue Limit { get; set; }
		public bool Exclusive { get; set; }

		public override IEnumerable<ValidationError> Check(TValue value) {
			if ((Exclusive && value.CompareTo(Limit) <= 0) || (!Exclusive && value.CompareTo(Limit) < 0)) {
				yield return new ValidationError { Message = String.Format(Message,Limit,value)};
			}
		}
	}
}