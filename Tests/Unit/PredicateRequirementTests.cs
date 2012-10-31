using System.Linq;
using Xunit;
using Mios.Validation.Requirements;

namespace Tests.Unit {
	public class PredicateRequirementTests {
		[Fact]
		public void Successful_predicate_evaluation_returns_empty_enumeration() {
			var req = new PredicateRequirement<object>(t => true);
			Assert.Empty(req.Check(new object()));
		}
		[Fact]
		public void Unsuccessful_predicate_evaluation_returns_non_empty_enumeration() {
			var req = new PredicateRequirement<object>(t => false);
			Assert.NotEmpty(req.Check(new object()));
		}
		[Fact]
		public void Returns_defined_error_message_if_unsuccessfully_evaluated() {
			var req = new PredicateRequirement<object>(t => false);
			req.Message = "X";
			Assert.Equal("X",req.Check(new object()).First().Message);
		}
	}
}