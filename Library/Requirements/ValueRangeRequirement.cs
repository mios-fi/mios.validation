using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mios.Validation.Requirements {
	public class ValueRangeRequirement<T> : IRequirement<T> where T : struct, IComparable<T> {
		public ValueRangeRequirement() {
		}
		public ValueRangeRequirement(T? lowerBound, T? upperBound)
			: this(lowerBound, false, upperBound, false) {
		}
		public ValueRangeRequirement(T? lowerBound, bool lowerExclusive, T? upperBound, bool upperExclusive) {
			Lower = lowerBound;
			LowerExclusive = lowerExclusive;
			Upper = upperBound;
			UpperExclusive = upperExclusive;
		}
		public T? Lower { get; set; }
		public bool LowerExclusive { get; set; }
		public T? Upper { get; set; }
		public bool UpperExclusive { get; set; }
		public IEnumerable<ValidationError> Check(T property) {
			if(Lower.HasValue && ((LowerExclusive && property.CompareTo(Lower.Value)<=0) || (!LowerExclusive && property.CompareTo(Lower.Value)<0))) {
				yield return new ValidationError();
			}
			if(Upper.HasValue && (( UpperExclusive && property.CompareTo(Upper.Value)>=0 ) || (!UpperExclusive && property.CompareTo(Upper.Value)>0))) {
				yield return new ValidationError();
			}
		}
	}
}
