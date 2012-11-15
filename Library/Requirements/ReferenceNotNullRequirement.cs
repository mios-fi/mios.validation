using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class ReferenceNotNullRequirement<TValue> : AbstractRequirement<TValue> where TValue : class {
		public ReferenceNotNullRequirement() {
			Message = "ReferenceNotNull";
		}

		public string Message { get; set; }

		public override IEnumerable<ValidationError> Check(TValue value) {
			if (value == null) {
				yield return new ValidationError {Message = Message};
			}
		}
	}
}