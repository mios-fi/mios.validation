using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class StringAtMostRequirement : AbstractRequirement<string> {
		private readonly int maxLength;

		public StringAtMostRequirement(int maxLength) {
			this.maxLength = maxLength;
			Message = "Value is too long ({1} characters while {0} are permitted)";
		}

		public string Message { get; set; }

		public override IEnumerable<ValidationError> Check(string value) {
		  if(value == null || value.Length <= maxLength)
        yield break;
		  yield return new ValidationError {Message = String.Format(Message, maxLength, value.Length)};
		}
	}
}