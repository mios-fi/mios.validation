using System.Linq;
using Mios.Validation.Requirements;
using Xunit;

namespace Tests.Unit {
	public class ValueMaximumRequirementTests {
		[Fact]
		public void Require_value_lower_than_or_equal_to_bound_if_not_exclusive() {
			var r = new ValueMaximumRequirement<int>(10);
			Assert.Empty(r.Check(9));
			Assert.Empty(r.Check(10));
			Assert.NotEmpty(r.Check(11));
		}
		[Fact]
		public void Require_value_lower_than_bound_if_exclusive() {
			var r = new ValueMaximumRequirement<int>(10,true);
			Assert.Empty(r.Check(9));
			Assert.NotEmpty(r.Check(10));
			Assert.NotEmpty(r.Check(11));
		}
		[Fact]
		public void Should_do_subsitutions_in_message() {
			var r = new ValueMaximumRequirement<int>(10, true);
			r.Message = "{0}##{1}";
			Assert.Equal("10##11", r.Check(11).First().Message);
		}
	}
}