using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public abstract partial class FluentCodeBody<TParent, TThis> : FluentWrapperlessTemplate<TParent>, ICodeBodyProvider where TThis : FluentCodeBody<TParent, TThis>
    {
        public FluentCodeBody(TParent parent, ICodeBodyProvider bodyProvider)
            : base(parent)
        {
            _bodyProvider = bodyProvider;
        }

        protected ICodeBodyProvider _bodyProvider;

        /////////////////////////////////////////////////////////////////
        //                           Public                            //
        /////////////////////////////////////////////////////////////////

        /// <summary>
        /// Declares a new variable
        /// </summary>
        /// <returns></returns>
        public TThis Declare(Type type, string name)
        {
            return AddStatement(new CodeVariableDeclarationStatement(type, name));   
        }

        /// <summary>
        /// Declares a new variable
        /// </summary>
        public TThis Declare(Type type, string name, CodeExpression initExpression)
        {
            return AddStatement(Expr.Declare(type, name, initExpression));
        }

        /// <summary>
        /// Declares a new variable
        /// </summary>
        /// <returns></returns>
        public TThis Declare(CodeTypeReference type, string name)
        {
            return AddStatement(Expr.Declare(type, name));
        }

        /// <summary>
        /// Declares a new variable
        /// </summary>
        public TThis Declare(CodeTypeReference type, string name, CodeExpression initExpression)
        {
            return AddStatement(Expr.Declare(type, name, initExpression));
        }

        /// <summary>
        /// Calls the specified property.
        /// </summary>
        /// <param name="methodName">The property name.</param>
        /// <returns></returns>
        public TThis Call(string methodName)
        {
            return AddStatement(Expr.Call(methodName));
        }

        /// <summary>
        /// Calls a property
        /// </summary>
        /// <returns></returns>
        public TThis Call(string methodName, params CodeExpression[] parameters)
        {
            return AddStatement(Expr.Call(methodName));
        }

        public TThis Call(CodeMethodReferenceExpression method, params CodeExpression[] parameters)
        {
            return AddStatement(Expr.Call(method, parameters));
        }

        /// <summary>
        /// Calls a method of an object.
        /// </summary>
        /// <returns></returns>
        public TThis CallMember(CodeExpression targetObject, string method, params CodeExpression[] parameters)
        {
            return AddStatement(Expr.CallMember(targetObject, method, parameters));
        }

        public TThis CallStatic(CodeTypeReferenceExpression type, string methodName)
        {
            return AddStatement(Expr.CallStatic(type, methodName));
        }

        /// <summary>
        /// Calls a static method
        /// </summary>
        public TThis CallStatic(Type type, string methodName, params CodeExpression[] parameters)
        {
            return AddStatement(Expr.CallStatic(type, methodName, parameters));
        }

        /// <summary>
        /// Calls a static method
        /// </summary>
        public TThis CallStatic(CodeTypeReferenceExpression type, string methodName, params CodeExpression[] parameters)
        {
            return AddStatement(Expr.CallStatic(type, methodName, parameters));
        }

        public TThis Set(string variableName, CodeExpression value)
        {
            var variableRefExpr = new CodeVariableReferenceExpression(variableName);
            var assignStmt = new CodeAssignStatement(variableRefExpr, value);
            return AddStatement(assignStmt);
        }

        public TThis Set(CodeExpression left, CodeExpression right)
        {
            var assignStmt = new CodeAssignStatement(left, right);
            return AddStatement(assignStmt);
        }

        /////////////////////////////////////////////////////////////////
        //                            Sonst                            //
        /////////////////////////////////////////////////////////////////

        /// <summary>
        /// Represents a statement using a literal code fragment.
        /// </summary>
        /// <param name="code">The literal code fragment.</param>
        /// <returns></returns>
        public TThis Snippet(string code)
        {
            return AddStatement(new CodeSnippetStatement(code));
        }

        /// <summary>
        /// Adds a CodeStatement
        /// </summary>
        /// <returns></returns>
        public TThis Stmt(CodeStatement expression)
        {
            return AddStatement(expression);
        }
        /// <summary>
        /// Adds a CodeExpression as Statement
        /// </summary>
        /// <returns></returns>
        public TThis Stmt(CodeExpression expression)
        {
            return AddStatement(expression);
        }

        /// <summary>
        /// Throws an exception
        /// </summary>
        /// <returns></returns>
        public TThis Throw(CodeExpression exceptionToThrow)
        {
            return AddStatement(new CodeThrowExceptionStatement(exceptionToThrow));
        }

        public TThis Return()
        {
            return AddStatement(new CodeMethodReturnStatement());
        }

        public TThis Return(CodeExpression value)
        {
            return AddStatement(new CodeMethodReturnStatement(value));
        }

        public TThis Comment(string comment)
        {
            return AddStatement(new CodeCommentStatement(comment));
        }

        /////////////////////////////////////////////////////////////////
        //                           Helper                            //
        /////////////////////////////////////////////////////////////////

        /// <summary>
        /// Adds a statement
        /// </summary>
        /// <returns></returns>
        protected virtual TThis AddStatement(CodeStatement expression)
        {
            _bodyProvider.Statements.Add(expression);
            return FluentCodeBody<TParent, TThis>.ThisConverter(this);
        }

        protected virtual TThis AddStatement(CodeExpression expression)
        {
            _bodyProvider.Statements.Add(expression);
            return FluentCodeBody<TParent, TThis>.ThisConverter(this);
        }

        /// <summary>
        /// This method is a little bit of a hack, because normaly you coudn't convert 
        /// FluetnCodeDom Of TParent, TThis into TThis, this function converts it first
        /// into object and then back into TThis
        /// </summary>
        /// <param name="thisInstance"></param>
        /// <returns></returns>
        public static TThis ThisConverter(FluentCodeBody<TParent, TThis> thisInstance)
        {
            return (TThis)(object)thisInstance;
        }

        /////////////////////////////////////////////////////////////////
        //                           Static                            //
        /////////////////////////////////////////////////////////////////

        public static ICodeBodyProvider GetBodyProvider(FluentCodeBody<TParent, TThis> fluentBody)
        {
            return fluentBody._bodyProvider;
        }

        public static TParent GetParent(FluentCodeBody<TParent, TThis> fluentBody)
        {
            return fluentBody._parent;
        }

        /////////////////////////////////////////////////////////////////
        //                            If                               //
        /////////////////////////////////////////////////////////////////

        public IfCodeBody<TThis> If(CodeExpression condition)
        {
            CodeConditionStatement statement = new CodeConditionStatement();
            statement.Condition = condition;
            AddStatement(statement);
            return new IfCodeBody<TThis>(statement, ThisConverter(this));
        }

        /////////////////////////////////////////////////////////////////
        //                           For                               //
        /////////////////////////////////////////////////////////////////

        public static readonly CodeTypeReference IntCodeType = new CodeTypeReference(typeof(int));

        /// <summary>
        /// Creates a for loop for every element of an array collection. For example for(int i = 0; i LesserThan array.Lengh; i = i + 1) { ... }
        /// </summary>
        public ForCodeBody<TThis> ForArray(string initVariableName, CodeExpression array)
        { 
            return ForArray(IntCodeType, initVariableName, array);
        }

        /// <summary>
        /// Creates a for loop for every element of an array collection. For example for(int i = 0; i LesserThan array.Lengh; i = i + 1) { ... }
        /// </summary>
        public ForCodeBody<TThis> ForArray(CodeTypeReference arrayElementType, string initVariableName, CodeExpression array)
        { 
            var initExpr = new CodeVariableDeclarationStatement(arrayElementType, initVariableName, Expr.Primitive(0));
            var testExpr = Expr.Op(Expr.Var(initVariableName), CodeBinaryOperatorType.LessThan, Expr.Prop(array, "Length"));
            var incrementStmt = new CodeAssignStatement(Expr.Var(initVariableName), Expr.Op(Expr.Var(initVariableName), CodeBinaryOperatorType.Add, Expr.Primitive(1)));

            return For(initExpr, testExpr, incrementStmt);
        }

        /// <summary>
        /// Creates a for statement.
        /// </summary>
        /// <param name="initStatement">A System.CodeDom.CodeStatement containing the loop initialization statement</param>
        /// <param name="testExpression">A System.CodeDom.CodeExpression containing the varDeclarationExpr to test for exit condition.</param>
        /// <param name="incrementStatment">An array of type System.CodeDom.CodeStatement containing the statements within the loop.</param>
        public ForCodeBody<TThis> For(CodeStatement initStatement, CodeExpression testExpression, CodeStatement incrementStatment)
        {
            //CodeStatement[] statements = new CodeStatement[]

            var iteratorStmt = new CodeIterationStatement();
            iteratorStmt.InitStatement = initStatement;
            iteratorStmt.TestExpression = testExpression;
            iteratorStmt.IncrementStatement = incrementStatment;

            AddStatement(iteratorStmt);
            return new ForCodeBody<TThis>(iteratorStmt, ThisConverter(this));
        }

        /////////////////////////////////////////////////////////////////
        //                           Try                               //
        /////////////////////////////////////////////////////////////////

        public TryCodeBody<TThis> Try
        {
            get
            { 
                var stmt = new CodeTryCatchFinallyStatement();
                AddStatement(stmt);
                return new TryCodeBody<TThis>(stmt, ThisConverter(this));
            }
        }

        /////////////////////////////////////////////////////////////////
        //                         While                               //
        /////////////////////////////////////////////////////////////////

        /// <summary>
        /// A while statement emulated with a for loop.
        /// </summary>
        /// <param name="testExpression"></param>
        /// <returns></returns>
        public WhileEmuCodeBody<TThis> WhileEmu(CodeExpression testExpression)
        {
            var whileStmt = new WhileEmuCodeBody<TThis>(testExpression, ThisConverter(this));
            AddStatement(IteratorCodeBody<TThis, WhileEmuCodeBody<TThis>>.GetIteratorStatement(whileStmt));
            return whileStmt;
        }

        /////////////////////////////////////////////////////////////////
        //                    Foreach Emitted                          //
        /////////////////////////////////////////////////////////////////

        /// <summary>
        /// A foreach statement emulated with a for loop.
        /// </summary>
        /// <param name="itemType">The type of the collection items.</param>
        /// <param name="itemVarName">The name of the iterator variable</param>
        /// <param name="collectionExpr">The collection which should be enumerated.</param>
        /// <returns></returns>
        public ForeachEmuCodeBody<TThis> ForeachEmu(Type itemType, string itemVarName, CodeExpression collectionExpr)
        {
            return ForeachEmu(new CodeTypeReference(itemType), itemVarName, collectionExpr);
        }

        /// <summary>
        /// A foreach statement emulated with a for loop.
        /// </summary>
        /// <param name="itemType">The type of the collection items.</param>
        /// <param name="itemVarName">The name of the iterator variable</param>
        /// <param name="collectionExpr">The collection which should be enumerated.</param>
        /// <returns></returns>
        public ForeachEmuCodeBody<TThis> ForeachEmu(CodeTypeReference itemType, string itemVarName, CodeExpression collectionExpr)
        { 
            // Adds its statements by itself
            var foreachCodeBody = new ForeachEmuCodeBody<TThis>(itemType, itemVarName, collectionExpr, ThisConverter(this));
            foreachCodeBody.AddStatements();
            return foreachCodeBody;
        }

        /////////////////////////////////////////////////////////////////
        //                      Using Emitted                          //
        /////////////////////////////////////////////////////////////////

        /// <summary>
        /// A using statement emulated with a try-finally block.
        /// </summary>
        /// <returns></returns>
        public UsingEmuCodeBody<TThis> UsingEmu(CodeVariableDeclarationStatement variableExpr)
        {
            var usingStmt = new UsingEmuCodeBody<TThis>(variableExpr, ThisConverter(this));
            AddStatement(UsingEmuCodeBody<TThis>.GetTryCatchFinallyStatement(usingStmt));
            return usingStmt;
        }

        /// <summary>
        /// A using statement emulated with a try-finally block.
        /// </summary>
        /// <returns></returns>
        public UsingEmuCodeBody<TThis> UsingEmu(CodeVariableReferenceExpression variableExpr)
        {
            var usingStmt = new UsingEmuCodeBody<TThis>(variableExpr, ThisConverter(this));
            AddStatement(UsingEmuCodeBody<TThis>.GetTryCatchFinallyStatement(usingStmt));
            return usingStmt;
        }

        /////////////////////////////////////////////////////////////////
        //                   ICodeBodyProvider                         //
        /////////////////////////////////////////////////////////////////

        #region ICodeBodyProvider Member

        CodeStatementCollection ICodeBodyProvider.Statements
        {
            get { return _bodyProvider.Statements; }
        }

        #endregion
    }

}
