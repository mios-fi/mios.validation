using System.Collections.Generic;
using System.Linq;
using Mios.Validation;
using Mios.Validation.Requirements;
using Moq;
using Xunit;

namespace Tests.Unit {
	public class NestedRequirementTests {
		[Fact]
		public void Should_apply_nested_requirements() {
			var obj = new Model { Value = "Bob" };
			var req = new StringRequirement();
			var list = new RequirementList<Model, string>(t => t.Value);
			list.Add(req);
			var nestedRequirement = new NestedRequirement<Model>(list);
			nestedRequirement.Check(obj);
			Assert.Same(obj, req.Container);
			Assert.Same(obj.Value, req.Value);
		}

		public class Model {
			public string Value { get; set; }
		}

		public class StringRequirement : AbstractRequirement<string> {
			public override IEnumerable<ValidationError> Check(object container, string value) {
				Container = container;
				Value = value;
				return Check(value);
			}

			public string Value { get; set; }
			public object Container { get; set; }

			public override IEnumerable<ValidationError> Check(string value) {
				return Enumerable.Empty<ValidationError>();
			}
		}
	}
}
