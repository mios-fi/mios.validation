using System;
using System.Collections.Generic;
using System.Linq;

namespace Mios.Validation.Requirements {
	public class NestedRequirement<TValue> : AbstractRequirement<TValue> {
		private readonly IRequirementList<TValue> nested;
		public Predicate<TValue> Predicate { get; set; }
		public NestedRequirement(IRequirementList<TValue> nested) {
			this.nested = nested;
			Predicate = t => true;
		}
		public override IEnumerable<ValidationError> Check(TValue value) {
			if(!Predicate(value)) return Enumerable.Empty<ValidationError>();
			return nested.Check(value);
		}
	}
}