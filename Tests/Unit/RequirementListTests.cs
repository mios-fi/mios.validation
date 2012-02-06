using System.Linq;
using Moq;
using Xunit;
using Mios.Validation;
namespace Tests {
	public class RequirementListTests {
		protected class Model {
			public string Name { get; set; }
			public int Age { get; set; }
			public string[] Siblings { get; set; }
		}

		public class CheckMethod {
			[Fact]
			public void Returns_an_error_string_for_each_failed_requirement() {
				var reqA = new Mock<AbstractRequirement<object>>() { CallBase = true };
				reqA.Setup(t => t.Check(It.IsAny<object>())).Returns(new[] { new ValidationError { Message = "errorA" } }).Verifiable();
				var reqB = new Mock<AbstractRequirement<object>>() { CallBase = true };
				reqB.Setup(t => t.Check(It.IsAny<object>())).Returns(new ValidationError[0]).Verifiable();
				var reqC = new Mock<AbstractRequirement<object>>() { CallBase = true };
				reqC.Setup(t => t.Check(It.IsAny<object>())).Returns(new[] { new ValidationError { Message = "errorC" } }).Verifiable();
				var list = new RequirementList<object, object>(t=>ToString());
				list.Add(reqA.Object);
				list.Add(reqB.Object);
				list.Add(reqC.Object);

				var errors = list.Check("").ToArray();
				reqA.Verify();
				reqB.Verify();
				reqC.Verify();
				Assert.Equal(2, errors.Count());
				Assert.NotEmpty(errors.Where(t => t.Message=="errorA"));
				Assert.NotEmpty(errors.Where(t => t.Message=="errorC"));
			}
			[Fact]
			public void Each_returned_error_has_a_key_that_reflects_the_tested_member() {
				var req1 = new Mock<AbstractRequirement<int>>() { CallBase = true };
				req1.Setup(t => t.Check(It.IsAny<int>())).Returns(new[] { new ValidationError { Message= "A" } });
				var req2 = new Mock<AbstractRequirement<int>>() { CallBase = true };
				req2.Setup(t => t.Check(It.IsAny<int>())).Returns(new[] { new ValidationError { Message= "B" } });
				var req3 = new Mock<AbstractRequirement<int>>() { CallBase = true };
				req3.Setup(t => t.Check(It.IsAny<int>())).Returns(new[] { new ValidationError { Message= "C" } });
				var list = new RequirementList<string, int>(t => t.Length);
				list.Add(req1.Object);
				list.Add(req2.Object);
				list.Add(req3.Object);

				var errors = list.Check("").ToArray();
				Assert.Equal("Length", errors[0].Key);
				Assert.Equal("Length", errors[1].Key);
				Assert.Equal("Length", errors[2].Key);
			}
			[Fact]
			public void If_a_key_is_specified_it_overrides_the_generated_key() {
				var req = new Mock<AbstractRequirement<int>>() { CallBase = true };
				req.Setup(t => t.Check(It.IsAny<int>())).Returns(new[] { new ValidationError { Message= "A" } });
				var list = new RequirementList<string, int>(t => t.Length, "Overridden");
				list.Add(req.Object);

				var errors = list.Check("").ToArray();
				Assert.Equal("Overridden", errors[0].Key);
			}
			[Fact]
			public void If_error_from_requirement_already_has_a_key_that_key_is_appended_to_the_member_name_separated_by_a_dot() {
				var req1 = new Mock<AbstractRequirement<int>>() { CallBase = true };
				req1.Setup(t => t.Check(It.IsAny<int>())).Returns(new[] { new ValidationError { Message= "A", Key="X" } });
				var list = new RequirementList<string, int>(t => t.Length);
				list.Add(req1.Object);

				var errors = list.Check("").ToArray();
				Assert.Equal("Length.X", errors[0].Key);
			}
			[Fact]
			public void Each_returned_error_contains_the_message_returned_by_the_requirement() {
				var req1 = new Mock<AbstractRequirement<int>>()  { CallBase = true };
				req1.Setup(t => t.Check(It.IsAny<int>())).Returns(new[] { new ValidationError { Message= "A" } });
				var req2 = new Mock<AbstractRequirement<int>>() { CallBase = true };
				req2.Setup(t => t.Check(It.IsAny<int>())).Returns(new[] { new ValidationError { Message= "B" } });
				var req3 = new Mock<AbstractRequirement<int>>() { CallBase = true };
				req3.Setup(t => t.Check(It.IsAny<int>())).Returns(new[] { new ValidationError { Message= "C" } });
				var list = new RequirementList<string, int>(t => t.Length);
				list.Add(req1.Object);
				list.Add(req2.Object);
				list.Add(req3.Object);

				var errors = list.Check("").ToArray();
				Assert.Equal("A", errors[0].Message);
				Assert.Equal("B", errors[1].Message);
				Assert.Equal("C", errors[2].Message);
			}
			[Fact]
			public void Containing_object_is_passed_as_context_to_property_requirements() {
				var req = new Mock<AbstractRequirement<string>>() { CallBase = true };
				var list = new RequirementList<Model, string>(t => t.Name);
				list.Add(req.Object);

				var obj = new Model {Name = "Bob"};
				list.Check(obj).ToArray();
				req.Verify(t => t.Check(obj, It.IsAny<string>()));
			}
		}
	}
}
