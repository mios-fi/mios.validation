﻿using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class AcceptValuesRequirement<T> : AbstractRequirement<T> {
		private readonly HashSet<T> accepted;

		public AcceptValuesRequirement(IEnumerable<T> acceptedValues) {
			accepted = new HashSet<T>(acceptedValues);
			Message = "Value {0} is not among the allowed alternatives";
		}

		public string Message { get; set; }

		public override IEnumerable<ValidationError> Check(T property) {
			if (!accepted.Contains(property)) {
				yield return new ValidationError {Message = String.Format(Message, property)};
			}
		}
	}
}