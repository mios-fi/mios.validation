using Mios.Validation.Requirements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.Unit {
	public class PredicateRequirementWithContextTests {
		[Fact]
		public void Requirement_with_context_passes_context_to_predicate() {
			object passedContext = new object(), receivedContext = null;
			var req = new PredicateRequirement<object, object>((context, t) => {
				receivedContext = context;
				return true;
			});
			Assert.Empty(req.Check(passedContext, new object()));
			Assert.Same(passedContext, receivedContext);
		}
		[Fact]
		public void Requirement_without_context_passes_default_context_to_predicate() {
			object receivedContext = null;
			var req = new PredicateRequirement<object, object>((context, t) => {
				receivedContext = context;
				return true;
			});
			Assert.Empty(req.Check(new object()));
			Assert.Same(null, receivedContext);
		}
	}
}
