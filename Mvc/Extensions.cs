namespace Mios.Validation.Mvc {
	using System.Collections.Generic;
	using System.Web.Mvc;

	public static class Extensions {
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
			return !found;
		}
    public static bool Check<T>(this Validator<T> validator, T @object, ModelStateDictionary modelState) {
      var errors = validator.Check(@object); var isValid = true;
      foreach(var error in errors) {
        modelState.AddModelError(error.Key, error.Message);
        isValid = false;
      }
      return isValid;
    }
	}
}
