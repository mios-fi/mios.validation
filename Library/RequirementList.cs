using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mios.Validation {

	public class RequirementList<TObject, TProperty> : IRequirementList<TObject, TProperty> {
		private readonly List<AbstractRequirement<TProperty>> requirements;
		private readonly Func<TObject, TProperty> function;
		private readonly string key;

	  public bool ContinueOnError { get; set; }

	  /// <summary>
		/// Initializes a new <see cref="RequirementList{TObject,TProperty}"/> class with a key determined from the supplied expression
		/// </summary>
		/// <param name="expression">An expression on an object of type <typeparamref name="TObject"/> defining the property on that object to apply requirements to</param>
		public RequirementList(Expression<Func<TObject, TProperty>> expression) 
			: this( expression.Compile(), expression.GetNameFor()) {
		}

		/// <summary>
		/// Initializes a new <see cref="RequirementList{TObject,TProperty}"/> class with the specified key. 
		/// </summary>
		/// <param name="function">A function of an object of type <typeparamref name="TObject"/> returning the value to apply requirements to</param>
		/// <param name="key">A key to identify requirements in this list by</param>
		public RequirementList(Func<TObject, TProperty> function, string key) {
			requirements = new List<AbstractRequirement<TProperty>>();
			this.function = function;
			this.key = key;
		}

		/// <summary>
		/// Adds a requirement to this requirement list
		/// </summary>
		/// <param name="requirement">The requirement to add</param>
		public void Add(AbstractRequirement<TProperty> requirement) {
			requirements.Add(requirement);
		}

		/// <summary>
		/// Verifies all requirements on a given object
		/// </summary>
		/// <param name="object">The object to verify requirements on</param>
		/// <returns>An enumeration of <see cref="ValidationError"/> values listing the failed requirements</returns>
		public IEnumerable<ValidationError> Check(TObject @object) {
			return Check(@object, String.Empty);
		}
			
		public IEnumerable<ValidationError> Check(TObject @object, string prefix) {
      var property = default(TProperty);
      if((object)@object!=null) {
        property = function.Invoke(@object);
      }
      var errorFound = false;
      foreach(var requirement in requirements) {
        foreach(var error in requirement.Check(@object, property)) {
          errorFound = true;
          yield return new ValidationError {
            Key = String.Concat(prefix,".",key,".",error.Key).Trim('.'),
            Message = error.Message
          };
        }
        if(!ContinueOnError && errorFound) yield break;
      }
		}
	}
}