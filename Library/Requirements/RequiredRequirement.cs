using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class RequiredRequirement<T> : AbstractRequirement<T> {
		public RequiredRequirement() {
			Message = "Required";
		}

		public string Message { get; set; }

		public override IEnumerable<ValidationError> Check(T value) {
			if(value!=null) {
				yield break;
			}
			yield return new ValidationError {
				Message = Message
			};
		}
	}
}