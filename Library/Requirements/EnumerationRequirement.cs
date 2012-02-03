using System.Linq;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class EnumerableRequirement<T> : IRequirement<IEnumerable<T>> {
		private readonly Validator<T> validator;
		private readonly string prefix;

		public EnumerableRequirement(Validator<T> validator, string prefix ) {
			this.validator = validator;
			this.prefix = prefix;
			Message = "Items in the enumeration are not valid";
		}
		public string Message { get; set; }
		public IEnumerable<ValidationError> Check(IEnumerable<T> enumerable) {
			return enumerable.SelectMany(t => validator.Check(t, prefix));
		}
	}
}