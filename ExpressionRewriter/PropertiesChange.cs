using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionRewriting
{
    internal class PropertiesChange
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PropertiesSequence _source;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PropertiesSequence _target;

        [DebuggerStepThrough]
        public PropertiesChange(PropertiesSequence source, PropertiesSequence target)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (target == null) throw new ArgumentNullException("target");
            _source = source;
            _target = target;
            if(_source.Properties[0].ResultType != _target.Properties[0].ResultType)
                throw new ArgumentException("Sequences of properties must have the same result type.");
        }

        public bool SourceCorrespondsTo(Expression expression)
        {
            expression = GetSequenceOriginExpression(expression);

            return expression != null && expression.Type == _source.SequenceOriginType;
        }

        public Expression GetSequenceOriginExpression(Expression expression)
        {
            foreach (var propertyInfo in _source.Properties)
            {
                var memberExpression = expression as MemberExpression;
                if (memberExpression == null)
                { return null; }

                if (memberExpression.Member.Name != propertyInfo.Name)
                { return null; }
                if (memberExpression.Type != propertyInfo.ResultType)
                { return null; }

                expression = memberExpression.Expression;
            }

            return expression;
        }

        public Expression GetNewPropertiesSequence(Expression sequenceOrigin)
        {
            if (sequenceOrigin == null) throw new ArgumentNullException("sequenceOrigin");

            if(sequenceOrigin.Type != _target.SequenceOriginType)
                throw new ArgumentException("Type of rewritten properties sequence is incorrect.", "sequenceOrigin");

            foreach (var propertyInfo in _target.Properties.Reverse())
            {
                var memberInfo = sequenceOrigin.Type.GetMember(propertyInfo.Name).FirstOrDefault(m => m.MemberType == MemberTypes.Field || m.MemberType == MemberTypes.Property);
                if(memberInfo == null)
                    throw new InvalidOperationException("Unable to create rewritten properties sequence");

                sequenceOrigin = Expression.MakeMemberAccess(sequenceOrigin, memberInfo);
            }

            return sequenceOrigin;
        }
    }
}