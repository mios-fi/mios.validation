using Mios.Validation.Requirements;

namespace Mios.Validation.Extensions {
	public static class StringRequirementExtensions {
		public static IRequirementList<T, string> AtLeast<T>(this IRequirementList<T, string> list, int minLength) {
			list.Add(new StringAtLeastRequirement(minLength));
			return list;
		}

		public static IRequirementList<T, string> AtMost<T>(this IRequirementList<T, string> list, int maxLength) {
			list.Add(new StringAtMostRequirement(maxLength));
			return list;
		}

		public static IRequirementList<T, string> NotEmpty<T>(this IRequirementList<T, string> list) {
			list.Add(new StringNotEmptyRequirement());
			return list;
		}
	}
}