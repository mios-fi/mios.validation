using System.Linq;
using Xunit;
using Mios.Validation.Requirements;

namespace Tests.Unit {
	public class IsDefinedByEnumRequirementTests {
		[Fact]
		public void Returns_error_if_value_is_not_defined() {
			var requirement = new IsDefinedInEnumRequirement<int>(typeof(Values));
			Assert.NotEmpty(requirement.Check(3));
		}
		[Fact]
		public void Returns_no_error_if_value_is_defined() {
			var requirement = new IsDefinedInEnumRequirement<int>(typeof(Values));
			Assert.NotEmpty(requirement.Check(2));
		}
		public enum Values {
			A, B
		}
	}
}