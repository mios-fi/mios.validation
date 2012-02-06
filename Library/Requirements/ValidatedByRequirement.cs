using System.Collections.Generic;
using System.Linq;

namespace Mios.Validation.Requirements {
	public class ValidatedByRequirement<TValue> : AbstractRequirement<TValue> {
		private readonly Validator<TValue> validator;

		public ValidatedByRequirement(Validator<TValue> validator) {
			this.validator = validator;
		}

		public override IEnumerable<ValidationError> Check(TValue property) {
			if (typeof (TValue).IsClass && property == null) return Enumerable.Empty<ValidationError>();
			return validator.Check(property);
		}
	}
}