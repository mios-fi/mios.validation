using System;
using Mios.Validation.Requirements;

namespace Mios.Validation.Extensions {
	public static class GenericRequirementExtensions {
		public static RequirementList<T, TProperty> NotNull<T, TProperty>(this RequirementList<T, TProperty> list)
			where TProperty : class {
			list.Add(new ReferenceNotNullRequirement<TProperty>());
			return list;
		}

		public static RequirementList<T, TProperty> Accept<T, TProperty>(this RequirementList<T, TProperty> list,
			params TProperty[] accepted) where TProperty : struct {
			list.Add(new AcceptValuesRequirement<TProperty>(accepted));
			return list;
		}

		public static RequirementList<T, TProperty> Reject<T, TProperty>(this RequirementList<T, TProperty> list,
			params TProperty[] rejected) where TProperty : struct {
			list.Add(new RejectValuesRequirement<TProperty>(rejected));
			return list;
		}

		public static RequirementList<T, TProperty> Gte<T, TProperty>(this RequirementList<T, TProperty> list,
			TProperty lowerBound) where TProperty : struct, IComparable<TProperty> {
			list.Add(new ValueMinimumRequirement<TProperty>(lowerBound,false));
			return list;
		}

		public static RequirementList<T, TProperty> Lte<T, TProperty>(this RequirementList<T, TProperty> list,
			TProperty upperBound) where TProperty : struct, IComparable<TProperty> {
			list.Add(new ValueMaximumRequirement<TProperty>(upperBound,false));
			return list;
		}

		public static RequirementList<T, TProperty> Gt<T, TProperty>(this RequirementList<T, TProperty> list,
			TProperty lowerBound) where TProperty : struct, IComparable<TProperty> {
			list.Add(new ValueMinimumRequirement<TProperty>(lowerBound, true));
			return list;
		}

		public static RequirementList<T, TProperty> Lt<T, TProperty>(this RequirementList<T, TProperty> list,
			TProperty upperBound) where TProperty : struct, IComparable<TProperty> {
			list.Add(new ValueMaximumRequirement<TProperty>(upperBound, true));
			return list;
		}

		public static RequirementList<T, TProperty> ValidatedBy<T, TProperty>(this RequirementList<T, TProperty> list,
			Validator<TProperty> validator) {
			list.Add(new ValidatedByRequirement<TProperty>(validator));
			return list;
		}

		public static RequirementList<T, TProperty> ValidatedBy<T, TProperty, TValidator>(this RequirementList<T, TProperty> list) 
			where TValidator : Validator<TProperty>, new() {
			list.Add(new ValidatedByRequirement<TProperty>(new TValidator()));
			return list;
		}

		public static RequirementList<T, TProperty> Satisfies<T, TProperty>(this RequirementList<T, TProperty> list,
			Predicate<TProperty> predicate) {
			list.Add(new PredicateRequirement<TProperty>(predicate));
			return list;
		}

		public static RequirementList<T, TProperty> Satisfies<T, TProperty>(this RequirementList<T, TProperty> list,
			Predicate<TProperty> predicate, string message) {
			var req = new PredicateRequirement<TProperty>(predicate);
			req.Message = message;
			list.Add(req);
			return list;
		}

		public static RequirementList<TProperty, TProperty> If<T, TProperty>(this RequirementList<T, TProperty> list, Predicate<TProperty> predicate) {
			var nested = new RequirementList<TProperty, TProperty>(t => t, String.Empty);
			list.Add(new NestedRequirement<TProperty>(nested) { Predicate = predicate });
			return nested;
		}
		public static RequirementList<T, TProperty> If<T, TProperty>(this RequirementList<T, TProperty> list, Predicate<TProperty> predicate, Action<RequirementList<TProperty,TProperty>> initializer) {
			var nested = new RequirementList<TProperty, TProperty>(t => t, String.Empty);
			initializer(nested);
			list.Add(new NestedRequirement<TProperty>(nested) { Predicate = predicate });
			return list;
		}
	}
}