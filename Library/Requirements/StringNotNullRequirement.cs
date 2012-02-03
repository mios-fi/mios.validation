using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class StringNotEmptyRequirement : IRequirement<string> {
		public StringNotEmptyRequirement() {
			Message = "A value is required";
		}

		public string Message { get; set; }

		#region IRequirement<string> Members

		public IEnumerable<ValidationError> Check(string property) {
			if (String.IsNullOrEmpty(property)) {
				yield return new ValidationError {Message = Message};
			}
		}

		#endregion
	}
}