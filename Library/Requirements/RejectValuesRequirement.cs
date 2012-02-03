using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class RejectValuesRequirement<T> : IRequirement<T> where T : struct {
		private readonly HashSet<T> rejected;

		public RejectValuesRequirement(params T[] rejectedValues) {
			rejected = new HashSet<T>(rejectedValues);
			Message = "Value {0} is not an allowed alternative";
		}

		public string Message { get; set; }

		#region IRequirement<T> Members

		public IEnumerable<ValidationError> Check(T property) {
			if (rejected.Contains(property)) {
				yield return new ValidationError {Message = String.Format(Message, property)};
			}
		}

		#endregion
	}
}