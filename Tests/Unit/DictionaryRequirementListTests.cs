using System.Collections.Generic;
using System.Linq;
using Mios.Validation;
using Moq;
using Xunit;

namespace Tests.Unit {
	public class DictionaryRequirementListTests {
		[Fact]
		public void KeyOfErrorsAreGivenIndexedByKey() {
			var req = new Mock<AbstractRequirement<int>> { CallBase = true };
			req.Setup(t => t.Check(It.IsAny<int>()))
				.Returns(new[] { new ValidationError { Key = "", Message = "Error" } });
			var list = new DictionaryRequirementList<T, string, int>(t => t.V);
			list.Add(req.Object);
			var errors = list.Check(new T { V = new Dictionary<string, int> { { "a", 0 } } });
			Assert.Equal("V[0]", errors.First().Key);
		}
		[Fact]
		public void CanUseLambdaToFormatKey() {
			var req = new Mock<AbstractRequirement<int>> { CallBase = true };
			req.Setup(t => t.Check(It.IsAny<int>()))
				.Returns(new[] { new ValidationError { Key = "", Message = "Error" } });
			var list = new DictionaryRequirementList<T, string, int>(t => t.V);
			list.Add(req.Object);
			list.FormatIndexerUsing((ix, key) => "["+ix+","+key+"]");
			var errors = list.Check(new T { V = new Dictionary<string, int> { { "a", 0 } } });
			Assert.Equal("V[0,a]", errors.First().Key);
		}

		public class T {
			public Dictionary<string,int> V { get; set; }
		}
	}
}