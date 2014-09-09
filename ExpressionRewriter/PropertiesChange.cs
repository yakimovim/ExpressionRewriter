using System;
using System.Diagnostics;
using System.Linq.Expressions;

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
            foreach (var propertyInfo in _source.Properties)
            {
                var memberExpression = expression as MemberExpression;
                if (memberExpression == null)
                { return false; }

                if(memberExpression.Member.Name != propertyInfo.Name)
                { return false; }
                if (memberExpression.Type != propertyInfo.ResultType)
                { return false; }

                expression = memberExpression.Expression;
            }

            return expression.Type == _source.SequenceOriginType;
        }
    }
}