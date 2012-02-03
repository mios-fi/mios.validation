using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class ReferenceNotNullRequirement<T> : IRequirement<T> where T : class {
		public ReferenceNotNullRequirement() {
			Message = "Required reference is missing";
		}

		public string Message { get; set; }

		#region IRequirement<T> Members

		public IEnumerable<ValidationError> Check(T property) {
			if (property == null) {
				yield return new ValidationError {Message = Message};
			}
		}

		#endregion
	}
}