using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class PredicateRequirement<T> : IRequirement<T> {
		private readonly Predicate<T> predicate;

		public PredicateRequirement(Predicate<T> predicate) {
			this.predicate = predicate;
			Message = "Predicate not satisfied";
		}

		public string Message { get; set; }

		#region IRequirement<T> Members

		public IEnumerable<ValidationError> Check(T property) {
			if (!predicate.Invoke(property)) {
				yield return new ValidationError {Message = Message};
			}
		}

		#endregion
	}
}