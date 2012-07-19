using System.Collections.Generic;

namespace Mios.Validation {
	public interface IRequirementList<in T> {
		/// <summary>
		/// Verifies all requirements on a given object
		/// </summary>
		/// <param name="object">The object to verify requirements on</param>
		/// <returns>An enumeration of <see cref="ValidationError"/> values listing the failed requirements</returns>
		IEnumerable<ValidationError> Check(T @object);
    
    /// <summary>
    /// Verifies all requirements on a given object
    /// </summary>
    /// <param name="object">The object to verify requirements on</param>
    /// <returns>An enumeration of <see cref="ValidationError"/> values listing the failed requirements</returns>
    IEnumerable<ValidationError> Check(T @object, string prefix);

    /// <summary>
    /// Gets or sets a value indicating if validation should stop after the first failing requirement.
    /// </summary>
    bool ContinueOnError { get; set; }
  }

  public interface IRequirementList<in TObject, TProperty> : IRequirementList<TObject> {
		/// <summary>
		/// Adds a requirement to this requirement list
		/// </summary>
		/// <param name="requirement">The requirement to add</param>
		void Add(AbstractRequirement<TProperty> requirement);
	}
}