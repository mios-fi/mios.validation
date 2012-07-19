using System;
using System.Collections.Generic;
using System.Linq;

namespace Mios.Validation.Requirements {
	public class NestedRequirement<T,TValue> : AbstractRequirement<TValue> where T : class {
		private readonly IRequirementList<TValue> nested;
		public Func<T,TValue,bool> Predicate { get; set; }
		public NestedRequirement(IRequirementList<TValue> nested) {
			this.nested = nested;
			Predicate = (c,t) => true;
		}
    public override IEnumerable<ValidationError> Check(object container, TValue value) {
      if(!Predicate(container as T, value)) return Enumerable.Empty<ValidationError>();
      return nested.Check(value);
    }
    public override IEnumerable<ValidationError> Check(TValue value) {
      return Check(null,value);
    }
  }
}