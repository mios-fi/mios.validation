using System;
using System.Linq.Expressions;

namespace Mios.Validation {
	internal static class ExpressionExtensions {
		public static string GetNameFor<T, TValue>(this Expression<Func<T, TValue>> expression) {
			return new ExpressionNameVisitor().Visit(expression.Body);
		}

		public static TValue GetValueFrom<T, TValue>(this Expression<Func<T, TValue>> expression, T viewModel) where T : class {
			try {
				return viewModel == null
				       	? default(TValue)
				       	: expression.Compile().Invoke(viewModel);
			} catch (Exception) {
				return default(TValue);
			}
		}

		public static MemberExpression GetMemberExpression<T, TValue>(this Expression<Func<T, TValue>> expression)
			where T : class {
			if (expression == null) {
				return null;
			}
			if (expression.Body is MemberExpression) {
				return (MemberExpression) expression.Body;
			}
			if (expression.Body is UnaryExpression) {
				Expression operand = ((UnaryExpression) expression.Body).Operand;
				if (operand is MemberExpression) {
					return (MemberExpression) operand;
				}
				if (operand is MethodCallExpression) {
					return ((MethodCallExpression) operand).Object as MemberExpression;
				}
			}
			return null;
		}
	}
}