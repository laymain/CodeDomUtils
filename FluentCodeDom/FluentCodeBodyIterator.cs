using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public abstract partial class FluentCodeBody<TParent, TThis>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TThisIterator">The type of the iterator class</typeparam>
        public class IteratorCodeBody<TThisIterator> :
            FluentCodeBody<FluentCodeBody<TParent, TThis>, TThisIterator>, ICodeBodyProvider
            where TThisIterator : IteratorCodeBody<TThisIterator>
        {
            public IteratorCodeBody(CodeIterationStatement statement, FluentCodeBody<TParent, TThis> parent)
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

            public static CodeIterationStatement GetIteratorStatement(FluentCodeBody<TParent, TThis>.IteratorCodeBody<TThisIterator> iteratorBody)
            {
                return iteratorBody._iteratorStatement;
            }
        }
    }
}
