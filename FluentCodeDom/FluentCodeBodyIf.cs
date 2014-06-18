using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public abstract partial class FluentCodeBody<TParent, TThis>
    {
        public class IfCodeBody :
            FluentCodeBody<FluentCodeBody<TParent, TThis>, IfCodeBody>, ICodeBodyProvider
        {
            public IfCodeBody(CodeConditionStatement condition, FluentCodeBody<TParent, TThis> parent)
                : base(parent, new CodeBodyProvider(condition.TrueStatements))
            {
                _codeConditionStatement = condition;
            }

            protected CodeConditionStatement _codeConditionStatement;

            public ElseCodeBody Else
            {
                get
                {
                    return new ElseCodeBody(_codeConditionStatement, _parent);
                }
            }

            #region ICodeBodyProvider Member

            CodeStatementCollection ICodeBodyProvider.Statements
            {
                get { return _codeConditionStatement.TrueStatements; }
            }

            #endregion

            public static CodeConditionStatement GetCodeConditionStatement(IfCodeBody ifCodeBody)
            {
                return ifCodeBody._codeConditionStatement;
            }
        }

        public class ElseCodeBody : FluentCodeBody<TParent, ElseCodeBody>, ICodeBodyProvider
        {
            public ElseCodeBody(CodeConditionStatement condition, TParent parent)
                : base(parent, new CodeBodyProvider(condition.FalseStatements))
            {
                _codeConditionStatement = condition;
            }

            protected CodeConditionStatement _codeConditionStatement;

            #region ICodeBodyProvider Member

            CodeStatementCollection ICodeBodyProvider.Statements
            {
                get { return _codeConditionStatement.FalseStatements; }
            }

            #endregion
        }
    }
}
