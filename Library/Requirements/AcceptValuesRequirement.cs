﻿using System;
using System.Collections.Generic;

namespace Mios.Validation.Requirements {
	public class AcceptValuesRequirement<T> : IRequirement<T> where T : struct {
		private readonly HashSet<T> accepted;

		public AcceptValuesRequirement(params T[] acceptedValues) {
			accepted = new HashSet<T>(acceptedValues);
			Message = "Value {0} is not among the allowed alternatives";
		}

		public string Message { get; set; }

		#region IRequirement<T> Members

		public IEnumerable<ValidationError> Check(T property) {
			if (!accepted.Contains(property)) {
				yield return new ValidationError {Message = String.Format(Message, property)};
			}
		}

		#endregion
	}
}