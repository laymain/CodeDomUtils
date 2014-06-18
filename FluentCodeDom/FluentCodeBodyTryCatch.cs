using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public abstract partial class FluentCodeBody<TParent, TThis>
    {
        public class TryCodeBody :
            FluentCodeBody<FluentCodeBody<TParent, TThis>, TryCodeBody>
        {
            public TryCodeBody(CodeTryCatchFinallyStatement statement, FluentCodeBody<TParent, TThis> parent)
                : base(parent, new CodeBodyProvider(statement.TryStatements))
            {
                _tryCatchStatement = statement;
            }

            protected CodeTryCatchFinallyStatement _tryCatchStatement;

            public CatchCodeBody Catch(Type exceptionType, string localName)
            {
                return Catch(new CodeTypeReference(exceptionType), localName);
            }

            public CatchCodeBody Catch(CodeTypeReference exceptionType, string localName)
            {
                var catchClause = new CodeCatchClause(localName, exceptionType, new CodeStatement[0]);
                _tryCatchStatement.CatchClauses.Add(catchClause);

                return new CatchCodeBody(_tryCatchStatement, catchClause, _parent);
            }

            public FinallyCodeBody Finally
            {
                get
                {
                    return new FinallyCodeBody(_tryCatchStatement, _parent);
                }
            }

            public static CodeTryCatchFinallyStatement GetTryCatchFinallyStatement(FluentCodeBody<TParent, TThis>.TryCodeBody tryBody)
            {
                return tryBody._tryCatchStatement;
            }
        }

        public class CatchCodeBody : FluentCodeBody<TParent, CatchCodeBody>
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

            public CatchCodeBody Catch(Type exceptionType, string localName)
            {
                return Catch(new CodeTypeReference(exceptionType), localName);
            }

            public CatchCodeBody Catch(CodeTypeReference exceptionType, string localName)
            {
                var catchClause = new CodeCatchClause(localName, exceptionType, new CodeStatement[0]);
                _tryCatchStatement.CatchClauses.Add(catchClause);

                return new CatchCodeBody(_tryCatchStatement, catchClause, _parent);
            }

            public FinallyCodeBody Finally
            {
                get
                {
                    return new FinallyCodeBody(_tryCatchStatement, _parent);
                }
            }

            public static CodeCatchClause GetCatchClause(FluentCodeBody<TParent, TThis>.CatchCodeBody catchBody)
            {
                return catchBody._catchClause;
            }
        }

        public class FinallyCodeBody : FluentCodeBody<TParent, FinallyCodeBody>
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
        }
    }
}
