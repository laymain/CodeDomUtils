using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.CodeDom;
using LinqToCodedom.Visitors;

namespace FluentCodeDom
{
    public static partial class FluentCodeDomLinqExtensions
    {
        /////////////////////////////////////////////////////////////////
        //                            If                               //
        /////////////////////////////////////////////////////////////////

        public static FluentCodeBody<TParent, TThis>.IfCodeBody If<TParent, TThis>(FluentCodeBody<TParent, TThis> body, 
            Expression condition)
            where TThis : FluentCodeBody<TParent, TThis>
        {
            return IfEmittedBase<TParent, TThis>(body, condition);
        }

        public static FluentCodeBody<TParent, TThis>.IfCodeBody If<TParent, TThis>(this FluentCodeBody<TParent, TThis> body, 
            Expression<Func<bool>> condition)
            where TThis : FluentCodeBody<TParent, TThis>
        {
            return IfEmittedBase<TParent, TThis>(body, condition);
        }

        public static FluentCodeBody<TParent, TThis>.IfCodeBody If<TParent, TThis, T>(this FluentCodeBody<TParent, TThis> body, 
            Expression<Func<T, bool>> condition)
            where TThis : FluentCodeBody<TParent, TThis>
        {
            return IfEmittedBase<TParent, TThis>(body, condition);
        }

        public static FluentCodeBody<TParent, TThis>.IfCodeBody If<TParent, TThis, T, T2>(this FluentCodeBody<TParent, TThis> body, 
            Expression<Func<T, T2, bool>> condition)
            where TThis : FluentCodeBody<TParent, TThis>
        {
            return IfEmittedBase<TParent, TThis>(body, condition);
        }

        public static FluentCodeBody<TParent, TThis>.IfCodeBody IfEmittedBase<TParent, TThis>(FluentCodeBody<TParent, TThis> body, 
            Expression condition)
            where TThis : FluentCodeBody<TParent, TThis>
        {
            CodeExpression cond = new CodeExpressionVisitor(new VisitorContext()).Visit(condition);
            
            return body.If(cond);
        }

        /////////////////////////////////////////////////////////////////
        //                           Body                              //
        /////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////
        //                         Statement                           //
        /////////////////////////////////////////////////////////////////

        public static TThis Stmt<TParent, TThis>(this FluentCodeBody<TParent, TThis> body,
            Expression<Action> statement)
            where TThis : FluentCodeBody<TParent, TThis>
        {
            return StmtEmittedBase<TParent, TThis>(body, statement);
        }

        public static TThis Stmt<TParent, TThis, T>(this FluentCodeBody<TParent, TThis> body,
            Expression<Action<T>> statement)
            where TThis : FluentCodeBody<TParent, TThis>
        {
            return StmtEmittedBase<TParent, TThis>(body, statement);
        }

        public static TThis Stmt<TParent, TThis, T, T2, T3>(this FluentCodeBody<TParent, TThis> body,
            Expression<Action<T, T2, T3>> statement)
            where TThis : FluentCodeBody<TParent, TThis>
        {
            return StmtEmittedBase<TParent, TThis>(body, statement);
        }

        public static TThis StmtEmittedBase<TParent, TThis>(FluentCodeBody<TParent, TThis> body,
            Expression condition)
            where TThis : FluentCodeBody<TParent, TThis>
        {
            CodeExpression expr = new CodeExpressionVisitor(new VisitorContext()).Visit(condition);
            body.Stmt(expr);

            return FluentCodeBody<TParent, TThis>.ThisConverter(body);
        }

        /////////////////////////////////////////////////////////////////
        //                          Return                             //
        /////////////////////////////////////////////////////////////////

        public static TThis Return<TParent, TThis, T>(this FluentCodeBody<TParent, TThis> body,
            Expression<Func<T>> expr)
            where TThis : FluentCodeBody<TParent, TThis>
        {
            return ReturnEmittedBase<TParent, TThis>(body, expr);
        }

        public static TThis Return<TParent, TThis, T, T2>(this FluentCodeBody<TParent, TThis> body,
            Expression<Func<T, T2>> expr)
            where TThis : FluentCodeBody<TParent, TThis>
        {
            return ReturnEmittedBase<TParent, TThis>(body, expr);
        }

        public static TThis Return<TParent, TThis, T, T2, T3>(this FluentCodeBody<TParent, TThis> body,
            Expression<Func<T, T2, T3>> expr)
            where TThis : FluentCodeBody<TParent, TThis>
        {
            return ReturnEmittedBase<TParent, TThis>(body, expr);
        }

        public static TThis ReturnEmittedBase<TParent, TThis>(FluentCodeBody<TParent, TThis> body, 
            Expression condition)
            where TThis : FluentCodeBody<TParent, TThis>
        {
            CodeExpression expr = new CodeExpressionVisitor(new VisitorContext()).Visit(condition);
            body.Return(expr);

            return FluentCodeBody<TParent, TThis>.ThisConverter(body);
        }   
    }
}
