using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mios.Validation;
using Mios.Validation.Requirements;
using Xunit;

namespace Tests.Unit {
	public class PropertyRequirementTests {
		[Fact]
		public void ShouldApplyRequirementToItems() {
			var bob = new Person { Tags = new[] { "cool", "dumb", "boring" } };
			var req = new EnumerableRequirement<Person, string>(p => p.Tags, new StringAtLeastRequirement(5));
			var err = req.Check(bob).ToArray();
			Assert.Equal(2, err.Length);
			Assert.Equal("Tags[0]", err[0].Key);
			Assert.Equal("Tags[1]", err[1].Key);

		}

		public class EnumerableRequirement<TObject, TProperty> : IRequirement<TObject> {
			private readonly Func<TObject, IEnumerable<TProperty>> fn;
			private readonly IRequirement<TProperty> requirement;
			private readonly string key;

			public EnumerableRequirement(Expression<Func<TObject,IEnumerable<TProperty>>> expression, IRequirement<TProperty> requirement) {
				this.requirement = requirement;
				fn = expression.Compile();
				key = expression.GetNameFor();
			}

			public IEnumerable<ValidationError> Check(TObject value) {
				var enumerable = fn(value);
				return enumerable.SelectMany((t,i) => 
					requirement.Check(t).Select(e => new ValidationError { 
						Key = (key+"["+i+"]."+e.Key).Trim('.'), 
						Message = e.Message 
					})
				);
			}
		}

		public class PropertyRequirement<TObject, TProperty> : IRequirement<TObject> {
			private readonly Func<TObject, TProperty> fn;
			private readonly IRequirementList<TProperty> requirements;
			private readonly string key;

			public PropertyRequirement(Expression<Func<TObject, TProperty>> expression, IRequirementList<TProperty> requirements) {
				this.requirements = requirements;
				fn = expression.Compile();
				key = expression.GetNameFor();
			}

			public IEnumerable<ValidationError> Check(TObject value) {
				var propertyValue = fn(value);
				return requirements.Check(propertyValue).Select(t=>new ValidationError { Key = (key+"."+t.Key).Trim('.'), Message = t.Message });
			}
		}

		public class Person {
			public string Name { get; set; }
			public string[] Tags { get; set; }
		}
	}
}
