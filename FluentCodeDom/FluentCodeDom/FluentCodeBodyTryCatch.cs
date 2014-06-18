using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public class TryCodeBody<TParent> :
        FluentCodeBody<TParent, TryCodeBody<TParent>>
    {
        public TryCodeBody(CodeTryCatchFinallyStatement statement, TParent parent)
            : base(parent, new CodeBodyProvider(statement.TryStatements))
        {
            _tryCatchStatement = statement;
        }

        protected CodeTryCatchFinallyStatement _tryCatchStatement;

        public CatchCodeBody<TParent> Catch(Type exceptionType, string localName)
        {
            return Catch(new CodeTypeReference(exceptionType), localName);
        }

        public CatchCodeBody<TParent> Catch(CodeTypeReference exceptionType, string localName)
        {
            var catchClause = new CodeCatchClause(localName, exceptionType, new CodeStatement[0]);
            _tryCatchStatement.CatchClauses.Add(catchClause);

            return new CatchCodeBody<TParent>(_tryCatchStatement, catchClause, _parent);
        }

        public FinallyCodeBody<TParent> Finally
        {
            get
            {
                return new FinallyCodeBody<TParent>(_tryCatchStatement, _parent);
            }
        }

        /////////////////////////////////////////////////////////////////
        //                             End                             //
        /////////////////////////////////////////////////////////////////

        public static CodeTryCatchFinallyStatement GetTryCatchFinallyStatement(TryCodeBody<TParent> tryBody)
        {
            return tryBody._tryCatchStatement;
        }
    }

    public class CatchCodeBody<TParent> : FluentCodeBody<TParent, CatchCodeBody<TParent>>
    {
        /// <summary>
        /// The container for a Catch Body
        /// </summary>
        /// <param name="statement">The CodeTryCatchStatement.</param>
        /// <param name="catchClause">The catch clause used for this. Info this catch clause must be still added to the CodeTryCatchStatement.</param>
        /// <param name="parent">The parent of this tryp catch block.</param>
        public CatchCodeBody(CodeTryCatchFinallyStatement statement, CodeCatchClause catchClause, TParent parent)
            : base(parent, new CodeBodyProvider(catchClause.Statements))
        {
            _tryCatchStatement = statement;
            _catchClause = catchClause;
        }

        protected CodeTryCatchFinallyStatement _tryCatchStatement;
        protected CodeCatchClause _catchClause;

        public CatchCodeBody<TParent> Catch(Type exceptionType, string localName)
        {
            return Catch(new CodeTypeReference(exceptionType), localName);
        }

        public CatchCodeBody<TParent> Catch(CodeTypeReference exceptionType, string localName)
        {
            var catchClause = new CodeCatchClause(localName, exceptionType, new CodeStatement[0]);
            _tryCatchStatement.CatchClauses.Add(catchClause);

            return new CatchCodeBody<TParent>(_tryCatchStatement, catchClause, _parent);
        }

        public FinallyCodeBody<TParent> Finally
        {
            get
            {
                return new FinallyCodeBody<TParent>(_tryCatchStatement, _parent);
            }
        }

        /////////////////////////////////////////////////////////////////
        //                             End                             //
        /////////////////////////////////////////////////////////////////

        public TParent EndTry
        {
            get
            {
                return EndInternal;
            }
        }

        public static CodeCatchClause GetCatchClause(CatchCodeBody<TParent> catchBody)
        {
            return catchBody._catchClause;
        }
    }

    public class FinallyCodeBody<TParent> : FluentCodeBody<TParent, FinallyCodeBody<TParent>>
    { 
                /// <summary>
        /// The container for a Catch Body
        /// </summary>
        /// <param name="statement">The CodeTryCatchStatement.</param>
        /// <param name="catchClause">The catch clause used for this. Info this catch clause must be still added to the CodeTryCatchStatement.</param>
        /// <param name="parent">The parent of this tryp catch block.</param>
        public FinallyCodeBody(CodeTryCatchFinallyStatement statement, TParent parent)
            : base(parent, new CodeBodyProvider(statement.FinallyStatements))
        {
        }

        /////////////////////////////////////////////////////////////////
        //                             End                             //
        /////////////////////////////////////////////////////////////////

        public TParent EndTry
        {
            get
            {
                return EndInternal;
            }
        }
    }
}
