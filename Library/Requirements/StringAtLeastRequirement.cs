using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class StringAtLeastRequirement : AbstractRequirement<string> {
		private readonly int minLength;

		public StringAtLeastRequirement(int minLength) {
			this.minLength = minLength;
			Message = "Value is too short ({1} characters while {0} are required)";
		}

		public string Message { get; set; }

		public override IEnumerable<ValidationError> Check(string value) {
		  if(value == null || value.Length >= minLength)
        yield break;
		  yield return new ValidationError {Message = String.Format(Message, minLength, value.Length)};
		}
	}
}