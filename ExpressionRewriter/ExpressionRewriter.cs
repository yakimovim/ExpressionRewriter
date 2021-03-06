﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionRewriting
{
    public class ExpressionRewriter
    {
        private readonly IDictionary<Type, Type> _argumentTypeChanges = new Dictionary<Type, Type>(); 
        private readonly IList<PropertiesChange> _propertiesChanges = new List<PropertiesChange>();

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

        internal void AddPropertyChange(PropertiesSequence source, PropertiesSequence target)
        {
            _propertiesChanges.Add(new PropertiesChange(source, target));
        }

        public Expression<T> Rewrite<T>(Expression sourceEx)
        {
            if (sourceEx == null) throw new ArgumentNullException("sourceEx");

            var rewriter = new RewritingVisitor(_argumentTypeChanges, _propertiesChanges);

            return rewriter.Rewrite<T>(sourceEx);
        }
    }
}