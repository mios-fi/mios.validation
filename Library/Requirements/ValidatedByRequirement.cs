using System.Collections.Generic;
using System.Linq;

namespace Mios.Validation.Requirements {
	public class ValidatedByRequirement<TProperty> : IRequirement<TProperty> {
		private Validator<TProperty> validator;

		public ValidatedByRequirement(Validator<TProperty> validator) {
			this.validator = validator;
		}

		#region IRequirement<TProperty> Members

		public IEnumerable<ValidationError> Check(TProperty property) {
			if (typeof (TProperty).IsClass && property == null) return Enumerable.Empty<ValidationError>();
			return validator.Check(property);
		}

		#endregion
	}
}