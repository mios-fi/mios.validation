using Mios.Validation.Requirements;
using System.Linq;
using Xunit;

namespace Tests.Unit {
	public class ValueMinimumRequirementTests {
		[Fact]
		public void Require_value_higher_than_or_equal_to_bound_if_not_exclusive() {
			var r = new ValueMinimumRequirement<int>(10);
			Assert.Empty(r.Check(11));
			Assert.Empty(r.Check(10));
			Assert.NotEmpty(r.Check(9));
		}
		[Fact]
		public void Require_value_higher_than_bound_if_exclusive() {
			var r = new ValueMinimumRequirement<int>(10, true);
			Assert.Empty(r.Check(11));
			Assert.NotEmpty(r.Check(10));
			Assert.NotEmpty(r.Check(9));
		}
		[Fact]
		public void Should_do_subsitutions_in_message() {
			var r = new ValueMinimumRequirement<int>(10, true);
			r.Message = "{0}##{1}";
			Assert.Equal("10##9",r.Check(9).First().Message);
		}
	}
}