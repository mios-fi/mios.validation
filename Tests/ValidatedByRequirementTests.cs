using System;
using Mios.Validation;
using System.Collections.Generic;
using Mios.Validation.Requirements;
using Moq;
using System.Linq;
using Xunit;

namespace Tests {
	public class ValidatedByRequirementTests {
		[Fact]
		public void Returns_empty_enumeration_for_successful_validation() {
			var val = new Mock<Validator<object>>();
			val.Setup(t => t.Check(It.IsAny<object>())).Returns(Enumerable.Empty<ValidationError>());
			var req = new ValidatedByRequirement<object>(val.Object);
			val.Verify();
			Assert.Empty(req.Check(new object()));
		}
		[Fact]
		public void Returns_empty_enumeration_for_null_reference() {
			var val = new Mock<Validator<object>>();
			val.Setup(t => t.Check(It.IsAny<object>())).Returns(Enumerable.Empty<ValidationError>());
			var req = new ValidatedByRequirement<object>(val.Object);
			Assert.Empty(req.Check(null));
		}
		[Fact]
		public void Does_not_invoke_validator_for_null_reference() {
			var val = new Mock<Validator<object>>();
			val.Setup(t => t.Check(It.IsAny<object>())).Returns(Enumerable.Empty<ValidationError>());
			var req = new ValidatedByRequirement<object>(val.Object);
			req.Check(null);
			val.Verify(t => t.Check(It.IsAny<object>()), Times.Never());
		}
		[Fact]
		public void Returns_errors_from_validator() {
			var val = new Mock<Validator<object>>();
			val.Setup(t => t.Check(It.IsAny<object>())).Returns(new[] { new ValidationError() });
			var req = new ValidatedByRequirement<object>(val.Object);
			val.Verify();
			Assert.NotEmpty(req.Check(new object()));
		}
	}
}
