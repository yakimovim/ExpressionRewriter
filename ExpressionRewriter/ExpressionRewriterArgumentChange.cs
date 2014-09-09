using System;
using System.Diagnostics;

namespace ExpressionRewriting
{
    public sealed class ExpressionRewriterArgumentChange<TSource>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ExpressionRewriter _expressionRewriter;

        [DebuggerStepThrough]
        public ExpressionRewriterArgumentChange(ExpressionRewriter expressionRewriter)
        {
            if (expressionRewriter == null) throw new ArgumentNullException("expressionRewriter");
            _expressionRewriter = expressionRewriter;
        }

        public void To<TTarget>()
        {
            _expressionRewriter.AddArgumentTypeChange(typeof (TSource), typeof (TTarget));
        }
    }
}