﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionRewriting
{
    internal class RewritingVisitor : ExpressionVisitor
    {
        private readonly IDictionary<string, ParameterExpression> _argumentSubstitutions = new Dictionary<string, ParameterExpression>();

        private readonly IDictionary<Type, Type> _argumentTypeChanges;

        public RewritingVisitor(IDictionary<Type, Type> argumentTypeChanges)
        {
            if (argumentTypeChanges == null) throw new ArgumentNullException("argumentTypeChanges");
            _argumentTypeChanges = argumentTypeChanges;
        }

        public Expression<T> Rewrite<T>(Expression sourceEx)
        {
            if (sourceEx == null) throw new ArgumentNullException("sourceEx");

            return (Expression<T>) Visit(sourceEx);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_argumentTypeChanges.ContainsKey(node.Type))
            {
                if (_argumentSubstitutions.ContainsKey(node.Name))
                {
                    return _argumentSubstitutions[node.Name];
                }
                
                var substitutionParameter = Expression.Parameter(_argumentTypeChanges[node.Type], node.Name);
                _argumentSubstitutions[node.Name] = substitutionParameter;
                return substitutionParameter;
            }

            return base.VisitParameter(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            Expression expression = Visit(node.Expression);
            if (expression == node.Expression)
            {
                return node;
            }

            if (expression.Type == node.Member.DeclaringType)
            {
                return Expression.MakeMemberAccess(expression, node.Member);
            }

            var newMember = expression.Type.GetMember(node.Member.Name).FirstOrDefault();
            if (newMember == null)
            {
                throw new InvalidOperationException(string.Format("Type '{0}' does not contain member '{1}'", expression.Type, node.Member.Name));
            }

            return Expression.MakeMemberAccess(expression, newMember);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var body = Visit(node.Body);
            var parameters = VisitAndConvert(node.Parameters, "VisitLambda");

            if (body == node.Body && parameters == node.Parameters)
            { return node; }

            Type delegateType;

            var funcGenericTypes = new List<Type>(parameters.Select(p => p.Type));

            if (body.Type == typeof (void))
            {
                var actionType = typeof(Func<>).Assembly.GetTypes()
                    .Where(t => t.Name.StartsWith("Action`"))
                    .Where(t => t.IsGenericType)
                    .FirstOrDefault(t => t.GetGenericArguments().Length == funcGenericTypes.Count);

                if (actionType == null)
                {
                    throw new InvalidOperationException("Can't find corresponding Action<> type");
                }

                delegateType = actionType.MakeGenericType(funcGenericTypes.ToArray());
            }
            else
            {
                funcGenericTypes.Add(body.Type);

                var funcType = typeof(Func<>).Assembly.GetTypes()
                    .Where(t => t.Name.StartsWith("Func`"))
                    .Where(t => t.IsGenericType)
                    .FirstOrDefault(t => t.GetGenericArguments().Length == funcGenericTypes.Count);

                if (funcType == null)
                {
                    throw new InvalidOperationException("Can't find corresponding Func<> type");
                }

                delegateType = funcType.MakeGenericType(funcGenericTypes.ToArray());
            }

            return Expression.Lambda(delegateType, body, parameters);
        }
    }
}