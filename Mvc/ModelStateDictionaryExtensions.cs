namespace Mios.Validation.Mvc {
	using System.Collections.Generic;
	using System.Web.Mvc;

	public static class ModelStateDictionaryExtensions {
		public static void AddErrors(this ModelStateDictionary modelState, IEnumerable<ValidationError> errors) {
			foreach(var error in errors) {
				modelState.AddModelError(error.Key, error.Message);
			}
		}
		public static bool Into(this IEnumerable<ValidationError> errors, ModelStateDictionary modelState) {
			var found = false;
			foreach(var error in errors) {
				modelState.AddModelError(error.Key, error.Message);
				found = true;
			}
			return found;
		}
	}
}
