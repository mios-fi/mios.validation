using System;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using Xunit;
using Mios.Validation;

namespace Tests.Unit {
	public class ValidatorTests {
		public class _Validator<T> : Validator<T> {
			public RequirementList<T, TProperty> _Require<TProperty>(Expression<Func<T, TProperty>> expression) {
				return Require(expression);
			}
		}
		public class RequireMethod {
			[Fact]
			public void Returns_a_Requirement_list_of_the_given_type() {
				var val = new _Validator<object>();
				Assert.IsAssignableFrom<RequirementList<object, string>>(val._Require(t => t.ToString()));
			}
		}
		public class CheckMetod {
			[Fact]
			public void Returns_an_empty_enumeration_when_no_requirements() {
				var val = new _Validator<object>();
				val._Require(t => t.ToString());
				Assert.Empty(val.Check(""));
			}
			[Fact]
			public void Returns_an_empty_enumeration_when_require_not_called() {
				var val = new Validator<object>();
				Assert.Empty(val.Check(""));
			}
			[Fact]
			public void Returns_an_error_for_each_failed_requirement() {
				var reqA = new Mock<IRequirement<string>>();
				reqA.Setup(t => t.Check(It.IsAny<string>())).Returns(new[] { new ValidationError { Message ="A" } });
				var reqB = new Mock<IRequirement<string>>();
				reqB.Setup(t => t.Check(It.IsAny<string>())).Returns(new ValidationError[0]);
				var reqC = new Mock<IRequirement<string>>();
				reqC.Setup(t => t.Check(It.IsAny<string>())).Returns(new[] { new ValidationError { Message="C" } });
				var val = new _Validator<object>();
				var reqs = val._Require(t => t.ToString());
				reqs.Add(reqA.Object);
				reqs.Add(reqB.Object);
				reqs.Add(reqC.Object);
				var list = val.Check("").ToList();
				Assert.Equal(2, list.Count);
			}
		}
	}
}