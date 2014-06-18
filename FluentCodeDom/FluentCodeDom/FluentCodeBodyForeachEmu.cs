using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using System.Collections;

namespace FluentCodeDom
{
    public class ForeachEmuCodeBody<TParent> : IteratorCodeBody<TParent, ForeachEmuCodeBody<TParent>>
        where TParent : ICodeBodyProvider
    {
        public ForeachEmuCodeBody(CodeTypeReference itemType, string iteratorVariableName, CodeExpression collectionExpr, TParent parent)
            : this(new CodeIterationStatement(), parent)
        {
            _itemType = itemType;
            _iteratorVariableName = iteratorVariableName;
            _collectionExpr = collectionExpr;
        }

        public static readonly CodeTypeReference IEnumeratorTypeReference = new CodeTypeReference(typeof(IEnumerator));
        public static readonly CodeTypeReference IEnumeratableTypeReference = new CodeTypeReference(typeof(IEnumerator));

        protected CodeTypeReference _itemType;
        protected string _iteratorVariableName;
        protected CodeExpression _collectionExpr;
        protected CodeIterationStatement _iteratorStmt;

        private ForeachEmuCodeBody(CodeIterationStatement iteratorStatement, TParent parent)
            : base(iteratorStatement, parent)
        {
            _iteratorStatement = iteratorStatement;
        }

        /// <summary>
        /// Adds the statements to the parents body.
        /// </summary>
        /// <param name="parent"></param>
        public virtual void AddStatements()
        {
            string enumeratorVarName = GetEnumeratorVariableName(_iteratorVariableName);

            // Todo: remove mem leak of IDisposable

            // IEnuerator iteratorVariableEnumerator = collection.GetEnumerator();
            _parent.Statements.Add(
                Expr.Declare(IEnumeratorTypeReference, enumeratorVarName, Expr.Cast(IEnumeratableTypeReference, Expr.CallMember(_collectionExpr, "GetEnumerator"))));

            // for(;iteratorVariableEnumerator.MoveNext();)
            _iteratorStatement.InitStatement = new CodeSnippetStatement(" ");
            _iteratorStatement.TestExpression = Expr.CallMember(Expr.Var(enumeratorVarName), "MoveNext");
            _iteratorStatement.IncrementStatement = new CodeSnippetStatement(" ");
            _parent.Statements.Add(_iteratorStatement);

            // ItemType iteratorVariable = (ItemType)iteratorVariableEnumerator.Current
            _iteratorStatement.Statements.Add(
                Expr.Declare(_itemType, _iteratorVariableName, Expr.Cast(_itemType, Expr.Prop(Expr.Var(enumeratorVarName), "Current"))));
        }

        /////////////////////////////////////////////////////////////////
        //                             End                             //
        /////////////////////////////////////////////////////////////////

        public TParent EndForeach
        {
            get
            {
                return (TParent)(object)EndInternal;
            }
        }

        protected override TParent EndInternal
        {
            get
            {
                return base.EndInternal;
            }
        }

        protected virtual string GetEnumeratorVariableName(string variableName)
        {
            return string.Format("{0}Enumerator", variableName);
        }
    }
}
