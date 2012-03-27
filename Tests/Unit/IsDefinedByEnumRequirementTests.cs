using System.Linq;
using Xunit;
using Mios.Validation.Requirements;

namespace Tests.Unit {
	public class IsDefinedByEnumRequirementTests {
		[Fact]
		public void Returns_error_if_value_is_not_defined() {
			var requirement = new IsDefinedInEnumRequirement<int>(typeof(Values));
			Assert.NotEmpty(requirement.Check(2));
		}

		[Fact]
		public void Returns_no_error_if_value_is_defined() {
			var requirement = new IsDefinedInEnumRequirement<int>(typeof(Values));
			Assert.Empty(requirement.Check(1));
		}

		public enum Values {
			A,
			B
		}
	}

	public class RequiredRequirementTests {
		[Fact]
		public void Returns_error_if_value_is_null() {
			var requirement = new RequiredRequirement<object>();
			Assert.NotEmpty(requirement.Check(null));
		}
		[Fact]
		public void Returns_error_if_nullable_value_is_null() {
			var requirement = new RequiredRequirement<int?>();
			Assert.NotEmpty(requirement.Check(null));
		}

		[Fact]
		public void Returns_no_error_if_nullable_value_is_not_null() {
			var requirement = new RequiredRequirement<int?>();
			Assert.Empty(requirement.Check(2));
		}
		[Fact]
		public void Returns_no_error_if_value_is_not_null() {
			var requirement = new RequiredRequirement<object>();
			Assert.Empty(requirement.Check(new object()));
		}
	}
}

