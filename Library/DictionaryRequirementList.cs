using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Mios.Validation {
	public class DictionaryRequirementList<TObject,TKey,TValue> : IRequirementList<TObject, TValue> {
		private readonly List<AbstractRequirement<TValue>> requirements;
		private readonly Func<TObject, IDictionary<TKey,TValue>> function;
		private Func<int, TKey, string> formatter = (i,k)=>"["+i+"]";
		private readonly string key;


		/// <summary>
		/// Initializes a new <see cref="DictionaryRequirementList{TObject,TKey,TValue}"/> class with a key determined from the supplied expression
		/// </summary>
		/// <param name="expression">An expression on an object of type <typeparamref name="TObject"/> defining the property on that object to apply requirements to</param>
		public DictionaryRequirementList(Expression<Func<TObject, IDictionary<TKey,TValue>>> expression) 
			: this( expression.Compile(), expression.GetNameFor()) {
		}

		/// <summary>
		/// Initializes a new <see cref="DictionaryRequirementList{TObject,TKey,TValue}"/> class with the specified key. 
		/// </summary>
		/// <param name="function">A function of an object of type <typeparamref name="TObject"/> returning the value to apply requirements to</param>
		/// <param name="key">A key to identify requirements in this list by</param>
		public DictionaryRequirementList(Func<TObject, IDictionary<TKey,TValue>> function, string key) {
			requirements = new List<AbstractRequirement<TValue>>();
			this.function = function;
			this.key = key;
		}

		/// <summary>
		/// Use the specified function to format indexer
		/// </summary>
		/// <param name="formatter">The delegate to use as formatting function</param>
		/// <returns></returns>
		public DictionaryRequirementList<TObject, TKey, TValue> FormatIndexerUsing(Func<int, TKey, string> formatter) {
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
			if(@object==null) return Enumerable.Empty<ValidationError>();
			var enumerable = function.Invoke(@object);
			if(enumerable==null) return Enumerable.Empty<ValidationError>();
			return enumerable
				.SelectMany((property,i) => requirements
				.SelectMany(t => t.Check(@object, property.Value).Select(e => new ValidationError {
					Key = String.Concat(prefix, ".", key, formatter(i,property.Key)+".", e.Key).Trim('.'),
					Message = e.Message
				})
				)
			);
		}
	}
}