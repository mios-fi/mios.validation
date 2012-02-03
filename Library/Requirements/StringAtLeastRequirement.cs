using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class StringAtLeastRequirement : IRequirement<string> {
		private readonly int minLength;

		public StringAtLeastRequirement(int minLength) {
			this.minLength = minLength;
			Message = "Value is too short ({1} characters while {0} are required)";
		}

		public string Message { get; set; }

		#region IRequirement<string> Members

		public IEnumerable<ValidationError> Check(string property) {
			if (property != null && property.Length < minLength) {
				yield return new ValidationError {Message = String.Format(Message, minLength, property.Length)};
			}
		}

		#endregion
	}
}