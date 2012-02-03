using System.Collections.Generic;

namespace Mios.Validation {
	/// <summary>
	/// Represents a requirement on a value
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IRequirement<T> {
		/// <summary>
		/// Verifies the given value against the requirement returning one or more <see cref="ValidationError"/> on failure
		/// </summary>
		/// <param name="value">The value to verify</param>
		/// <returns>An enumeration of <see cref="ValidationError"/> representing any failed requirements</returns>
		IEnumerable<ValidationError> Check(T value);
	}
}