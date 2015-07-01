using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TParent"></typeparam>
    /// <typeparam name="TThis"></typeparam>
    public class IteratorCodeBody<TParent, TThis> : FluentCodeBody<TParent, TThis>, ICodeBodyProvider
        where TThis : IteratorCodeBody<TParent, TThis>
    {
        public IteratorCodeBody(CodeIterationStatement statement, TParent parent)
            : base(parent, new CodeBodyProvider(statement.Statements))
        {
            _iteratorStatement = statement;
        }

        protected CodeIterationStatement _iteratorStatement;

        #region ICodeBodyProvider Member

        public CodeStatementCollection Statements
        {
            get { return _iteratorStatement.Statements; }
        }

        #endregion

        public static CodeIterationStatement GetIteratorStatement(IteratorCodeBody<TParent, TThis> iteratorBody)
        {
            return iteratorBody._iteratorStatement;
        }
    }
}
