using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    //public abstract partial class FluentCodeBody<TParent, TThis>
    //{
    //    public class ForCodeBody :
    //        FluentCodeBody<FluentCodeBody<TParent, TThis>, ForCodeBody>, ICodeBodyProvider
    //    {
    //        public ForCodeBody(CodeIterationStatement statement, FluentCodeBody<TParent, TThis> parent)
    //            : base(parent, new CodeBodyProvider(statement.Statements))
    //        {
    //            _tryCatchStatement = statement;
    //        }

    //        protected CodeIterationStatement _tryCatchStatement;

    //        #region ICodeBodyProvider Member

    //        public CodeStatementCollection Statements
    //        {
    //            get { return _tryCatchStatement.Statements; }
    //        }

    //        #endregion
    //    }
    //}

    public abstract partial class FluentCodeBody<TParent, TThis>
    {
        public class ForCodeBody :
            IteratorCodeBody<ForCodeBody>
        {
            public ForCodeBody(CodeIterationStatement iteratorStatement, FluentCodeBody<TParent, TThis> parent)
                : base(iteratorStatement, parent)
            { 
            }
        }
    }

}
