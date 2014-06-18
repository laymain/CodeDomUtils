using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public class CodeBodyProvider : ICodeBodyProvider
    {
        public CodeBodyProvider(CodeStatementCollection statements)
        {
            Statements = statements;
        }

        #region ICodeBodyProvider Member

        public CodeStatementCollection Statements
        {
            get;
            protected set;
        }

        #endregion
    }
}
