using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public abstract partial class FluentCodeBody<TParent, TThis>
    {
        public class UsingEmuCodeBody :
            FluentCodeBody<FluentCodeBody<TParent, TThis>, UsingEmuCodeBody>
        {
            public UsingEmuCodeBody(CodeVariableDeclarationStatement varDeclarationExpr, FluentCodeBody<TParent, TThis> parent)
                : this(varDeclarationExpr, parent, new CodeTryCatchFinallyStatement())
            { 
                
            }

            public UsingEmuCodeBody(CodeVariableReferenceExpression varReferenceExpr, FluentCodeBody<TParent, TThis> parent)
                : this(varReferenceExpr, parent, new CodeTryCatchFinallyStatement())
            {
            }

            protected UsingEmuCodeBody(CodeVariableReferenceExpression expression, FluentCodeBody<TParent, TThis> parent, CodeTryCatchFinallyStatement tryCatchFinallyStmt)
                : base(parent, new CodeBodyProvider(tryCatchFinallyStmt.TryStatements))
            {
                CtorBase(tryCatchFinallyStmt, expression.VariableName);
            }

            protected UsingEmuCodeBody(CodeVariableDeclarationStatement expression, FluentCodeBody<TParent, TThis> parent, CodeTryCatchFinallyStatement tryCatchFinallyStmt)
                : base(parent, new CodeBodyProvider(tryCatchFinallyStmt.TryStatements))
            {
                _parent.AddStatement(expression);

                CtorBase(tryCatchFinallyStmt, expression.Name);
            }

            private void CtorBase(CodeTryCatchFinallyStatement tryCatchFinallyStmt, string usingVarName)
            {
                _tryCatchStatement = tryCatchFinallyStmt;

                // Call Dispose. This must be casted becuase IDisposable can be implemented implicit
                _tryCatchStatement.FinallyStatements.Add(Expr.CallMember(Expr.Cast(typeof(IDisposable), Expr.Var(usingVarName)), "Dispose"));
            }

            protected CodeTryCatchFinallyStatement _tryCatchStatement;

            public static CodeTryCatchFinallyStatement GetTryCatchFinallyStatement(FluentCodeBody<TParent, TThis>.UsingEmuCodeBody codeBody)
            {
                return codeBody._tryCatchStatement;
            }

            //public UsingEmuCodeBody(CodeVariableReferenceExpression varDeclarationExpr, FluentCodeBody<TParent, TThis> parent)
            //    : this(varDeclarationExpr, new TryCodeBody(new CodeTryCatchFinallyStatement(), parent), parent)
            //{ 
            
            //}

            //protected UsingEmuCodeBody(CodeVariableReferenceExpression varDeclarationExpr, TryCodeBody tryCodeBody, FluentCodeBody<TParent, TThis> parent)
            //    : base(parent, 
            //        new CodeBodyProvider(TryCodeBody.GetTryCatchFinallyStatement(tryCodeBody).TryStatements)
            //{
            //    _tryCodeBody = tryCodeBody;
            //}

            //protected TryCodeBody _tryCodeBody;
        }
    }
}
