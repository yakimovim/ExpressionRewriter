using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ExpressionRewriting
{
    internal class PropertiesSequence
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly List<PropertyInfo> _properties = new List<PropertyInfo>();

        public PropertyInfo[] Properties
        {
            [DebuggerStepThrough]
            get { return _properties.ToArray(); }
        }

        public Type SequenceOriginType { get; private set; }

        public PropertiesSequence(Expression expression)
        {
            ExtractPropertiesInfo(expression);
        }

        private void ExtractPropertiesInfo(Expression expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");

            var convertExpression = expression as UnaryExpression;
            if (convertExpression != null && convertExpression.NodeType == ExpressionType.Convert)
            {
                ExtractPropertiesInfo(convertExpression.Operand);
                return;
            }

            var memberExpression = expression as MemberExpression;
            if (memberExpression == null) throw new ArgumentException("Only field or property accesses are allowed", "expression");

            _properties.Add(new PropertyInfo { Name = memberExpression.Member.Name, ResultType = memberExpression.Type });

            var memberOwnerExpression = memberExpression.Expression;

            var parameterExpression = memberOwnerExpression as ParameterExpression;

            if (parameterExpression != null)
            {
                SequenceOriginType = parameterExpression.Type;
            }
            else
            {
                ExtractPropertiesInfo(memberOwnerExpression);
            }
        }
    }

    internal struct PropertyInfo
    {
        public Type ResultType { get; set; }
        public string Name { get; set; }
    }
}