namespace Mios.Validation {
	/// <summary>
	/// Represents a failed requirement
	/// </summary>
	public struct ValidationError {
		/// <summary>
		/// A key identifying the property that failed the requirement
		/// </summary>
		public string Key;
		/// <summary>
		/// A readable error message describing why the requirement failed
		/// </summary>
		public string Message;
	}
}