using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class IsDefinedInEnumRequirement<T> : AbstractRequirement<T> where T : struct {
		private readonly Type typeOfEnum;

		public IsDefinedInEnumRequirement(Type typeOfEnum) {
			this.typeOfEnum = typeOfEnum;
			Message = "Must be one of the values defined in "+typeOfEnum.Name;
		}

		public string Message { get; set; }

		public override IEnumerable<ValidationError> Check(T value) {
			if(!typeOfEnum.IsEnum || Enum.IsDefined(typeOfEnum, value)) {
				yield break;
			}
			yield return new ValidationError {
				Message = Message
			};
		}
	}
}