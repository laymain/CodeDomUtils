using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public class IfCodeBody<TParent> : FluentCodeBody<TParent, IfCodeBody<TParent>>, ICodeBodyProvider
    {
        public IfCodeBody(CodeConditionStatement condition, TParent parent)
            : base(parent, new CodeBodyProvider(condition.TrueStatements))
        {
            _codeConditionStatement = condition;
        }

        protected CodeConditionStatement _codeConditionStatement;

        public ElseCodeBody<TParent> Else
        {
            get
            {
                return new ElseCodeBody<TParent>(_codeConditionStatement, _parent);
            }
        }

        /////////////////////////////////////////////////////////////////
        //                             End                             //
        /////////////////////////////////////////////////////////////////

        public TParent EndIf
        {
            get
            {
                return (TParent)(object)EndInternal;
            }
        }

        #region ICodeBodyProvider Member

        CodeStatementCollection ICodeBodyProvider.Statements
        {
            get { return _codeConditionStatement.TrueStatements; }
        }

        #endregion

        public static CodeConditionStatement GetCodeConditionStatement(IfCodeBody<TParent> ifCodeBody)
        {
            return ifCodeBody._codeConditionStatement;
        }
    }

    public class ElseCodeBody<TParent> : FluentCodeBody<TParent, ElseCodeBody<TParent>>, ICodeBodyProvider
    {
        public ElseCodeBody(CodeConditionStatement condition, TParent parent)
            : base(parent, new CodeBodyProvider(condition.FalseStatements))
        {
            _codeConditionStatement = condition;
        }

        protected CodeConditionStatement _codeConditionStatement;

        /////////////////////////////////////////////////////////////////
        //                             End                             //
        /////////////////////////////////////////////////////////////////

        public TParent EndIf
        {
            get
            {
                return EndInternal;
            }
        }

        #region ICodeBodyProvider Member

        CodeStatementCollection ICodeBodyProvider.Statements
        {
            get { return _codeConditionStatement.FalseStatements; }
        }

        #endregion
    }
}
