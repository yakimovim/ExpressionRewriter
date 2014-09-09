using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ExpressionRewriting
{
    public class ExpressionRewriterPropertyChange
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ExpressionRewriter _expressionRewriter;
        private readonly Expression _sourceBody;

        [DebuggerStepThrough]
        public ExpressionRewriterPropertyChange(ExpressionRewriter expressionRewriter, Expression sourceBody)
        {
            if (expressionRewriter == null) throw new ArgumentNullException("expressionRewriter");
            if (sourceBody == null) throw new ArgumentNullException("sourceBody");
            _expressionRewriter = expressionRewriter;
            _sourceBody = sourceBody;
        }

        public void To<T>(Expression<Func<T, object>> target)
        {
            _expressionRewriter.AddPropertyChange(_sourceBody, target.Body);
        }
    }
}