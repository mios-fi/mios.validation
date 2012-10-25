using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Mios.Validation {
	public class EnumerableRequirementList<TObject, TValue> : IRequirementList<TObject, TValue> {
	  private readonly List<AbstractRequirement<TValue>> requirements;
		private readonly Func<TObject, IEnumerable<TValue>> function;
		private Func<int, TValue, string> formatter = (i, k) => "[" + i + "]";
		private readonly string key;

    /// <summary>
    /// Gets or sets a value indicating if validation should stop after the first failing requirement.
    /// </summary>
    public bool ContinueOnError { get; set; }
    
    /// <summary>
		/// Initializes a new <see cref="EnumerableRequirementList{TObject,TValue}"/> class with a key determined from the supplied expression
		/// </summary>
		/// <param name="expression">An expression on an object of type <typeparamref name="TObject"/> defining the property on that object to apply requirements to</param>
		public EnumerableRequirementList(Expression<Func<TObject, IEnumerable<TValue>>> expression) 
			: this( expression.Compile(), expression.GetNameFor()) {
		}

		/// <summary>
		/// Initializes a new <see cref="EnumerableRequirementList{TObject,TValue}"/> class with the specified key. 
		/// </summary>
		/// <param name="function">A function of an object of type <typeparamref name="TObject"/> returning the value to apply requirements to</param>
		/// <param name="key">A key to identify requirements in this list by</param>
		public EnumerableRequirementList(Func<TObject, IEnumerable<TValue>> function, string key) {
			requirements = new List<AbstractRequirement<TValue>>();
			this.function = function;
			this.key = key;
		}

		/// <summary>
		/// Use the specified function to format indexer
		/// </summary>
		/// <param name="formatter">The delegate to use as formatting function</param>
		/// <returns></returns>
		public EnumerableRequirementList<TObject, TValue> FormatIndexerUsing(Func<int, TValue, string> formatter) {
			this.formatter = formatter;
			return this;
		}

		/// <summary>
		/// Adds a requirement to this requirement list
		/// </summary>
		/// <param name="requirement">The requirement to add</param>
		public void Add(AbstractRequirement<TValue> requirement) {
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
      if(@object==null) yield break;
			var enumerable = function.Invoke(@object);
      if(enumerable==null) yield break;
      var i=0;
      foreach(var item in enumerable) {
        foreach(var requirement in requirements) {
          foreach(var error in requirement.Check(@object, item)) {
            yield return new ValidationError {
						  Key = String.Concat(prefix, ".", key, formatter(i,item), ".", error.Key).Trim('.'),
						  Message = error.Message
					  };
          }
        }
        i++;
      }
		}
	}
}