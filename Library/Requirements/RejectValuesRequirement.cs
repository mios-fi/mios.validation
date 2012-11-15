using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class RejectValuesRequirement<TValue> : AbstractRequirement<TValue> {
		private readonly HashSet<TValue> rejected;

		public RejectValuesRequirement(IEnumerable<TValue> rejectedValues) {
			rejected = new HashSet<TValue>(rejectedValues);
			Message = "RejectValues";
		}

		public string Message { get; set; }

		public override IEnumerable<ValidationError> Check(TValue value) {
			if (rejected.Contains(value)) {
				yield return new ValidationError {Message = String.Format(Message, value)};
			}
		}
	}
}