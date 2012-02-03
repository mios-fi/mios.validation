using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class StringAtMostRequirement : IRequirement<string> {
		private readonly int maxLength;

		public StringAtMostRequirement(int maxLength) {
			this.maxLength = maxLength;
			Message = "Value is too long ({1} characters while {0} are permitted)";
		}

		public string Message { get; set; }

		#region IRequirement<string> Members

		public IEnumerable<ValidationError> Check(string property) {
			if (property != null && property.Length > maxLength) {
				yield return new ValidationError {Message = String.Format(Message, maxLength, property.Length)};
			}
		}

		#endregion
	}
}