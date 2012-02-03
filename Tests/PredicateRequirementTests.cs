using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Mios.Validation;

namespace Tests {
	public class PredicateRequirementTests {
		[Fact]
		public void Successful_predicate_evaluation_returns_empty_enumeration() {
			var req = new PredicateRequirement<object>(t => true);
			Assert.Empty(req.Check(new object()));
		}
		[Fact]
		public void Unsuccessful_predicate_evaluation_returns_non_empty_enumeration() {
			var req = new PredicateRequirement<object>(t => false);
			Assert.NotEmpty(req.Check(new object()));
		}
		[Fact]
		public void Returns_defined_error_message_if_unsuccessfully_evaluated() {
			var req = new PredicateRequirement<object>(t => false);
			req.Message = "X";
			Assert.Equal("X",req.Check(new object()).First().Message);
		}
	}
	public class PredicateRequirement<T> : IRequirement<T> {
		public string Message { get; set; }
		private readonly Predicate<T> predicate;
		public PredicateRequirement( Predicate<T> predicate ) {
			this.predicate = predicate;
		}
		public IEnumerable<ValidationError> Check(T property) {
			if(!predicate.Invoke(property)) {
				yield return new ValidationError { Message = Message };
			}
		}
	}
}
