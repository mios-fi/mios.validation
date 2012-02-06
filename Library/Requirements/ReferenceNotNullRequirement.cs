using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class ReferenceNotNullRequirement<TValue> : AbstractRequirement<TValue> where TValue : class {
		public ReferenceNotNullRequirement() {
			Message = "Required reference is missing";
		}

		public string Message { get; set; }

		public override IEnumerable<ValidationError> Check(TValue value) {
			if (value == null) {
				yield return new ValidationError {Message = Message};
			}
		}
	}
}