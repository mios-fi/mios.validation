using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {

	public class PredicateRequirement<TContext, TValue> : PredicateRequirement<TValue> {
		private readonly Func<TContext, TValue, bool> predicateWithContext;

		public PredicateRequirement(Func<TContext, TValue, bool> predicate)
			: base(t => predicate(default(TContext), t)) {
			predicateWithContext = predicate;
		}

		public override IEnumerable<ValidationError> Check(object context, TValue value) {
			var typedContext = default(TContext);
			if(context is TContext) {
				typedContext = (TContext)context;
			}
			if(!predicateWithContext.Invoke(typedContext, value)) {
				yield return new ValidationError { Message = Message };
			}
		}
	}

	public class PredicateRequirement<TValue> : AbstractRequirement<TValue> {
		private readonly Predicate<TValue> predicate;

		public PredicateRequirement(Predicate<TValue> predicate) {
			this.predicate = predicate;
			Message = "Predicate";
		}

		public string Message { get; set; }

		public override IEnumerable<ValidationError> Check(TValue value) {
			if(!predicate.Invoke(value)) {
				yield return new ValidationError {Message = Message};
			}
		}
		public override IEnumerable<ValidationError> Check(object context, TValue value) {
			if(!predicate.Invoke(value)) {
				yield return new ValidationError { Message = Message };
			}
		}
	}
}