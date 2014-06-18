using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public abstract partial class FluentCodeBody<TParent, TThis>
    {
        public class WhileEmuCodeBody : IteratorCodeBody<WhileEmuCodeBody>
        {
            public WhileEmuCodeBody(CodeExpression testExpression, FluentCodeBody<TParent, TThis> parent)
                : base(BuildIteratorStatement(testExpression), parent)
            {
            }

            private static CodeIterationStatement BuildIteratorStatement(CodeExpression testExpression)
            {
                var whileStmt = new CodeIterationStatement();
                whileStmt.InitStatement = new CodeSnippetStatement(" ");
                whileStmt.TestExpression = testExpression;
                whileStmt.IncrementStatement = new CodeSnippetStatement(" ");

                return whileStmt;
            }

            public override FluentCodeBody<TParent, TThis> End
            {
                get
                {
                    return base.End;
                }
            }
        }
    }
}
