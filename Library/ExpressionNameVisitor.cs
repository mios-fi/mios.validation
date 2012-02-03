using System.Linq.Expressions;

namespace Mios.Validation {
	internal class ExpressionNameVisitor {
		public string Visit(Expression expression) {
			if (expression is UnaryExpression) {
				expression = ((UnaryExpression) expression).Operand;
			}
			if (expression is MethodCallExpression) {
				return Visit((MethodCallExpression) expression);
			}
			if (expression is MemberExpression) {
				return Visit((MemberExpression) expression);
			}
			if (expression is BinaryExpression && expression.NodeType == ExpressionType.ArrayIndex) {
				return Visit((BinaryExpression) expression);
			}
			return null;
		}

		private string Visit(BinaryExpression expression) {
			string result = null;
			if (expression.Left is MemberExpression) {
				result = Visit((MemberExpression) expression.Left);
			}
			object index = Expression.Lambda(expression.Right).Compile().DynamicInvoke();
			return result + string.Format("[{0}]", index);
		}

		private string Visit(MemberExpression expression) {
			string name = expression.Member.Name;
			string ancestorName = Visit(expression.Expression);
			if (ancestorName != null) {
				name = ancestorName + "." + name;
			}
			return name;
		}

		private string Visit(MethodCallExpression expression) {
			string name = null;
			if (expression.Object is MemberExpression) {
				name = Visit((MemberExpression) expression.Object);
			}

			//TODO: Is there a more certain way to determine if this is an indexed property?
			if (expression.Method.Name == "get_Item" && expression.Arguments.Count == 1) {
				object index = Expression.Lambda(expression.Arguments[0]).Compile().DynamicInvoke();
				name += string.Format("[{0}]", index);
			}
			return name;
		}
	}
}