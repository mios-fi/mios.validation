﻿using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class IsDefinedInEnumRequirement<T> : AbstractRequirement<T> {
		private readonly Type typeOfEnum;

		public IsDefinedInEnumRequirement(Type typeOfEnum) {
			this.typeOfEnum = typeOfEnum;
			Message = "IsDefinedBy";
		}

		public string Message { get; set; }

		public override IEnumerable<ValidationError> Check(T value) {
			if(value==null || !typeOfEnum.IsEnum || Enum.IsDefined(typeOfEnum, value)) {
				yield break;
			}
			yield return new ValidationError {
				Message = Message
			};
		}
	}
}