using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.Linq.Expressions;
using LinqToCodedom.Visitors;

namespace FluentCodeDom
{
    public static partial class ExprLinq
    {
        public static CodeExpression Expr(Expression<Action> expression)
        {
            return ExprInner(expression);
        }

        public static CodeExpression Expr<T>(Expression<Action<T>> expression)
        { 
            return ExprInner(expression);
        }

        private static CodeExpression ExprInner(Expression expression)
        {
            CodeExpression expr = new CodeExpressionVisitor(new VisitorContext()).Visit(expression);
            return expr;
        }
    }
}
