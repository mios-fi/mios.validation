using System;
using System.Collections.Generic;
using System.Linq;
using Mios.Validation;

namespace Tests.Unit {
	public class NestedRequirement<TObject> : IRequirement<TObject> {
		private readonly IRequirementList<TObject> nested;
		public Predicate<TObject> Predicate { get; set; }
		public NestedRequirement(IRequirementList<TObject> nested) {
			this.nested = nested;
			Predicate = t => true;
		}
		public IEnumerable<ValidationError> Check(TObject value) {
			return Predicate(value) 
				? nested.Check(value) 
				: Enumerable.Empty<ValidationError>();
		}
	}
}