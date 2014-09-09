using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ExpressionRewriting
{
    public sealed class ExpressionRewriterPropertyChange
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ExpressionRewriter _expressionRewriter;
        private readonly PropertiesSequence _sourceSequence;

        [DebuggerStepThrough]
        public ExpressionRewriterPropertyChange(ExpressionRewriter expressionRewriter, Expression sourceBody)
        {
            if (expressionRewriter == null) throw new ArgumentNullException("expressionRewriter");
            if (sourceBody == null) throw new ArgumentNullException("sourceBody");
            _expressionRewriter = expressionRewriter;
            _sourceSequence = new PropertiesSequence(sourceBody); 
        }

        public void To<T>(Expression<Func<T, object>> target)
        {
            _expressionRewriter.AddPropertyChange(_sourceSequence, new PropertiesSequence(target.Body));
        }
    }
}