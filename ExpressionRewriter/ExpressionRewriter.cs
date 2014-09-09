using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionRewriting
{
    public class ExpressionRewriter
    {
        private readonly IDictionary<Type, Type> _argumentTypeChanges = new Dictionary<Type, Type>(); 

        public ExpressionRewriterArgumentChange<TSource> ChangeArgumentType<TSource>()
        {
            return new ExpressionRewriterArgumentChange<TSource>(this);
        }

        public ExpressionRewriterPropertyChange ChangeProperty<T>(Expression<Func<T, object>> ex)
        {
            return new ExpressionRewriterPropertyChange(this, ex.Body);
        }

        internal void AddArgumentTypeChange(Type sourceType, Type targetType)
        {
            _argumentTypeChanges[sourceType] = targetType;
        }

        internal void AddPropertyChange(Expression sourceBody, Expression targetBody)
        {
        }

        public Expression<T> Rewrite<T>(Expression sourceEx)
        {
            if (sourceEx == null) throw new ArgumentNullException("sourceEx");

            var rewriter = new RewritingVisitor(_argumentTypeChanges);

            return rewriter.Rewrite<T>(sourceEx);
        }
    }
}