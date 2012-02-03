using System.Collections.Generic;

namespace Mios.Validation {
	public interface IRequirementList<TObject> {
		/// <summary>
		/// Verifies all requirements on a given object
		/// </summary>
		/// <param name="object">The object to verify requirements on</param>
		/// <returns>An enumeration of <see cref="ValidationError"/> values listing the failed requirements</returns>
		IEnumerable<ValidationError> Check(TObject @object);
		IEnumerable<ValidationError> Check(TObject @object, string prefix);
	}
	public interface IRequirementList<TObject, TProperty> : IRequirementList<TObject> {
		/// <summary>
		/// Adds a requirement to this requirement list
		/// </summary>
		/// <param name="requirement">The requirement to add</param>
		void Add(IRequirement<TProperty> requirement);
	}
}