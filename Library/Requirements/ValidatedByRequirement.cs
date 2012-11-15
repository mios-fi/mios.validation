using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class ValidatedByRequirement<T> : AbstractRequirement<T> {
		private readonly Validator<T> validator;

		public ValidatedByRequirement(Validator<T> validator) {
			this.validator = validator;
      Message = "ValidatedBy";
    }

    public string Message { get; set; }

		public override IEnumerable<ValidationError> Check(T @object) {
      if(typeof(T).IsClass && @object == null) yield break;
      var hasErrors = false;
      foreach(var error in validator.Check(@object)) {
        yield return error;
        hasErrors = true;
      }
      if(hasErrors) {
        yield return new ValidationError { Key = "", Message = Message };
      }
		}
	}
}