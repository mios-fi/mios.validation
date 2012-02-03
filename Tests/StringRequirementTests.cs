using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Mios.Validation.Requirements;

namespace Tests {
	public class StringAtMostRequirementTests {
		[Fact]
		public void Returns_null_if_value_is_smaller_or_equal_to_the_max_length() {
			var requirement = new StringAtMostRequirement(5);
			Assert.Empty(requirement.Check("1234"));
			Assert.Empty(requirement.Check("12345"));
		}
		[Fact]
		public void Returns_error_if_value_is_too_long() {
			var requirement = new StringAtMostRequirement(5);
			Assert.NotEmpty(requirement.Check("123456"));
		}
		[Fact]
		public void Returns_null_if_value_is_null() {
			var requirement = new StringAtMostRequirement(5);
			Assert.Empty(requirement.Check(null));
		}
		[Fact]
		public void Returns_error_message_with_replacements() {
			var requirement = new StringAtMostRequirement(5);
			requirement.Message = "{0}#{1}";
			Assert.Equal("5#6",requirement.Check("123456").ToArray()[0].Message);
		}
	}

	public class StringAtLeastRequirementTests {
		[Fact]
		public void Returns_null_if_value_is_longer_or_equal_to_the_min_length() {
			var requirement = new StringAtLeastRequirement(4);
			Assert.Empty(requirement.Check("1234"));
			Assert.Empty(requirement.Check("12345"));
		}
		[Fact]
		public void Returns_error_if_value_is_too_short() {
			var requirement = new StringAtLeastRequirement(5);
			Assert.NotEmpty(requirement.Check("1234"));
		}
		[Fact]
		public void Returns_null_if_value_is_null() {
			var requirement = new StringAtLeastRequirement(5);
			Assert.Empty(requirement.Check(null));
		}
		[Fact]
		public void Returns_error_message_with_replacements() {
			var requirement = new StringAtLeastRequirement(5);
			requirement.Message = "{0}#{1}";
			Assert.Equal("5#4", requirement.Check("1234").ToArray()[0].Message);
		}
	}
	public class StringNotEmptyRequirementTests {
		[Fact]
		public void Returns_null_if_value_is_present_and_not_Empty() {
			var requirement = new StringNotEmptyRequirement();
			Assert.Empty(requirement.Check("1234"));
		}
		[Fact]
		public void Returns_error_if_value_is_empty() {
			var requirement = new StringNotEmptyRequirement();
			Assert.NotEmpty(requirement.Check(""));
		}
		[Fact]
		public void Returns_error_if_value_is_null() {
			var requirement = new StringNotEmptyRequirement();
			Assert.NotEmpty(requirement.Check(null));
		}
		[Fact]
		public void Returns_error_message_with_replacements() {
			var requirement = new StringNotEmptyRequirement();
			requirement.Message = "xyz";
			Assert.Equal("xyz", requirement.Check(null).ToArray()[0].Message);
		}
	}
}
