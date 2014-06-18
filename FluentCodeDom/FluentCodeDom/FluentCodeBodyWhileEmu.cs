using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public class WhileEmuCodeBody<TParent> : IteratorCodeBody<TParent ,WhileEmuCodeBody<TParent>>
    {
        public WhileEmuCodeBody(CodeExpression testExpression, TParent parent)
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

        /////////////////////////////////////////////////////////////////
        //                             End                             //
        /////////////////////////////////////////////////////////////////

        public TParent EndWhile
        {
            get
            {
                return EndInternal;
            }
        }
    }
}
