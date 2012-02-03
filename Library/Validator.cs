using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Mios.Validation {
	public class Validator<TTarget> {
		private readonly IList<IRequirementList<TTarget>> requirements;

		/// <summary>
		/// Initializes a new <see cref="Validator{TObject}"/> instance.
		/// </summary>
		public Validator() {
			requirements = new List<IRequirementList<TTarget>>();
		}

		/// <summary>
		/// Creates a new list of requirements associated with this <see cref="Validator{TObject}"/>
		/// </summary>
		/// <typeparam name="TProperty">The type of the property to validate</typeparam>
		/// <param name="expression">An expression defining the property to validate</param>
		/// <returns>A <see cref="IRequirementList{TObject,TProperty}"/> associated with the specified property</returns>
		protected RequirementList<TTarget, TProperty> Require<TProperty>(Expression<Func<TTarget, TProperty>> expression) {
			var list = new RequirementList<TTarget, TProperty>(expression);
			requirements.Add(list);
			return list;
		}

		/// <summary>
		/// Creates a new list of requirements associated with this <see cref="Validator{TObject}"/> with a specified key
		/// </summary>
		/// <typeparam name="TProperty">The type of the property to validate</typeparam>
		/// <param name="expression">An expression defining the property to validate</param>
		/// <param name="key">The key to identify requirements in this list by</param>
		/// <returns>A <see cref="IRequirementList{TObject,TProperty}"/> associated with the specified property</returns>
		protected RequirementList<TTarget, TProperty> Require<TProperty>(Func<TTarget, TProperty> expression, string key) {
			var list = new RequirementList<TTarget, TProperty>(expression,key);
			requirements.Add(list);
			return list;
		}

		/// <summary>
		/// Evaluates all requirement lists associated with this <see cref="Validator{TObject}"/> on a given target.
		/// </summary>
		/// <param name="target">The target to evaluate</param>
		/// <returns>An enumeration of <see cref="ValidationError"/> values representing each falied requirement on the target</returns>
		public virtual IEnumerable<ValidationError> Check(TTarget target) {
			return Check(target, String.Empty);
		}

		/// <summary>
		/// Evaluates all requirement lists associated with this <see cref="Validator{TObject}"/> on a given target.
		/// </summary>
		/// <param name="target">The target to evaluate</param>
		/// <returns>An enumeration of <see cref="ValidationError"/> values representing each falied requirement on the target</returns>
		public virtual IEnumerable<ValidationError> Check(TTarget target, string prefix) {
			return requirements.SelectMany(t => t.Check(target, prefix));
		}
	}
}