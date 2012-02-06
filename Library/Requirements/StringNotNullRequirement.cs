using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class StringNotEmptyRequirement : AbstractRequirement<string> {
		public StringNotEmptyRequirement() {
			Message = "A value is required";
		}

		public string Message { get; set; }

		public override IEnumerable<ValidationError> Check(string value) {
			if (String.IsNullOrEmpty(value)) {
				yield return new ValidationError {Message = Message};
			}
		}
	}
}