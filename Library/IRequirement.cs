using System.Collections.Generic;

namespace Mios.Validation {
	public abstract class AbstractRequirement<TValue> : IRequirement<TValue> {
		public abstract IEnumerable<ValidationError> Check(TValue value);
		public virtual IEnumerable<ValidationError> Check(object container, TValue value) {
			return Check(value);
		}
	}

	/// <summary>
	/// Represents a requirement on a value
	/// </summary>
	/// <typeparam name="TValue"></typeparam>
	public interface IRequirement<in TValue> {
		/// <summary>
		/// Verifies the given value against the requirement returning one or more <see cref="ValidationError"/> on failure
		/// </summary>
		/// <param name="value">The value to verify</param>
		/// <returns>An enumeration of <see cref="ValidationError"/> representing any failed requirements</returns>
		IEnumerable<ValidationError> Check(TValue value);
	}
}