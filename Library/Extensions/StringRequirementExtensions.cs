using Mios.Validation.Requirements;

namespace Mios.Validation.Extensions {
	public static class StringRequirementExtensions {
		public static RequirementList<T, string> AtLeast<T>(this RequirementList<T, string> list, int minLength) {
			list.Add(new StringAtLeastRequirement(minLength));
			return list;
		}

		public static RequirementList<T, string> AtMost<T>(this RequirementList<T, string> list, int maxLength) {
			list.Add(new StringAtMostRequirement(maxLength));
			return list;
		}

		public static RequirementList<T, string> NotEmpty<T>(this RequirementList<T, string> list) {
			list.Add(new StringNotEmptyRequirement());
			return list;
		}
	}
}