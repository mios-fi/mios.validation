using Xunit;
using Mios.Validation.Requirements;

namespace Tests.Unit {
	public class RejectValuesRequirementTests {
		public enum Choices {
			Yes,
			No,
			Maybe
		}
		[Fact]
		public void Returns_error_if_enum_value_is_contained_in_rejected_set() {
			var req = new RejectValuesRequirement<Choices>(Choices.Yes, Choices.No);
			Assert.NotEmpty(req.Check(Choices.No));
		}
		[Fact]
		public void Returns_null_if_enum_value_is_not_contained_in_rejected_set() {
			var req = new RejectValuesRequirement<Choices>(Choices.Yes, Choices.No);
			Assert.Empty(req.Check(Choices.Maybe));
		}
	}
}