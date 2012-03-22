using System;
using System.Linq;
using System.Text;
using Mios.Validation;
using Moq;
using Xunit;

namespace Tests.Unit {
	public class EnumerableRequirementListTests {
		[Fact]
		public void KeyOfErrorsAreGivenIndexed() {
			var req = new Mock<AbstractRequirement<string>> { CallBase = true };
			req.Setup(t => t.Check(It.IsAny<string>()))
				.Returns(new[] { new ValidationError { Key = "", Message = "Error" } });
			var list = new EnumerableRequirementList<T, string>(t => t.V);
			list.Add(req.Object);
			var errors = list.Check(new T { V = new[] { "" } });
			Assert.Equal("V[0]", errors.First().Key);
		}

		public class T {
			public string[] V { get; set; }
		}
	}
}
