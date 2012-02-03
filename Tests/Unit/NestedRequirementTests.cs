using Mios.Validation;
using Mios.Validation.Extensions;
using Xunit;

namespace Tests.Unit {
	public class NestedRequirementTests {
		[Fact]
		public void ShouldCheckNestedRequirements() {
			var requirements = new RequirementList<Name, string>(t => t.First).AtLeast(5);
			var value = new Name { First = "Bob" };
			Assert.NotEmpty(requirements.Check(value));
			var nestedRequirement = new NestedRequirement<Name>(requirements);
			Assert.NotEmpty(nestedRequirement.Check(value));
			nestedRequirement.Predicate = a => a.First != "Bob";
			Assert.Empty(nestedRequirement.Check(value));
		}

		public class Name {
			public string First { get; set; }
		}
	}
}
