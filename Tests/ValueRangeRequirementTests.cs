using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mios.Validation;
using Mios.Validation.Requirements;
using Xunit;

namespace Tests {
	public class ValueRangeRequirementTests {
		public class When_exclusive_not_set {
			[Fact]
			public void If_lower_bound_is_set_require_value_higher_than_or_equal_to_bound() {
				var r = new ValueRangeRequirement<int>();
				r.Lower = 10;
				Assert.NotEmpty(r.Check(9));
				Assert.Empty(r.Check(10));
				Assert.Empty(r.Check(11));
			}
			[Fact]
			public void If_upper_bound_is_set_require_value_less_than_or_equal_to_bound() {
				var r = new ValueRangeRequirement<int>();
				r.Upper = 10;
				Assert.NotEmpty(r.Check(11));
				Assert.Empty(r.Check(10));
				Assert.Empty(r.Check(9));
			}
			[Fact]
			public void If_no_bounds_are_set_return_null() {
				var r = new ValueRangeRequirement<int>();
				Assert.Empty(r.Check(-100));
				Assert.Empty(r.Check(100));
				Assert.Empty(r.Check(1));
			}
		}
		public class When_exclusive_set {
			[Fact]
			public void If_lower_bound_is_set_require_value_higher_than_bound() {
				var r = new ValueRangeRequirement<int>();
				r.Lower = 10;
				r.LowerExclusive = true;
				Assert.Empty(r.Check(11));
				Assert.NotEmpty(r.Check(10));
				Assert.NotEmpty(r.Check(9));
			}
			[Fact]
			public void If_upper_bound_is_set_require_value_less_than_bound() {
				var r = new ValueRangeRequirement<int>();
				r.Upper = 10;
				r.UpperExclusive = true;
				Assert.Empty(r.Check(9));
				Assert.NotEmpty(r.Check(10));
				Assert.NotEmpty(r.Check(11));
			}
			[Fact]
			public void If_no_bounds_are_set_return_null() {
				var r = new ValueRangeRequirement<int>();
				r.LowerExclusive = true;
				r.UpperExclusive = true;
				Assert.Empty(r.Check(-100));
				Assert.Empty(r.Check(100));
				Assert.Empty(r.Check(1));
			}
		}
	}
}
