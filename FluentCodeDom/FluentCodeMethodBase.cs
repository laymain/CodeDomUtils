using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    /// <summary>
    /// The base class for constructors and methods
    /// </summary>
    /// <typeparam name="TThis"></typeparam>
    /// <typeparam name="TWrappedType">The type wrapped by this class. Must inherit CodeMemberMethod</typeparam>
    /// <typeparam name="TParent"></typeparam>
    public abstract class FluentCodeMethodBase<TThis, TWrappedType, TParent> : FluentCodeBody<FluentCodeType<TParent>, TThis>, ICodeBodyProvider
        where TThis : FluentCodeMethodBase<TThis, TWrappedType, TParent>
        where TWrappedType : CodeMemberMethod
        where TParent : FluentCodeType<TParent>
    {
         //: FluentCodeTypeMember<TThis, TWrappedType, FluentCodeType>

        public FluentCodeMethodBase(TWrappedType method)
            : this(method, null)
        {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="method">The wrapped type.</param>
        /// <param name="parentType">Optional, can be null.</param>
        public FluentCodeMethodBase(TWrappedType method, FluentCodeType<TParent> parentType)
            : base(parentType, new CodeBodyProvider(method.Statements))
        {
            _codeTypeMember = new FluentTypeMemberMethodDummy(method);
            _wrappedType = method;
        }

        protected internal TWrappedType _wrappedType;

        /// <summary>
        /// Usage of FluentCodeProperty as TTHis becuas method does not implement CodeTypeMember
        /// </summary>
        protected FluentTypeMemberMethodDummy _codeTypeMember;

        /////////////////////////////////////////////////////////////////
        //                           Public                            //
        /////////////////////////////////////////////////////////////////

        public TThis Name(string name)
        {
            _wrappedType.Name = name;
            return ThisConverter(this);
        }

        public TThis Attributes(MemberAttributes attributes)
        {
            _wrappedType.Attributes = attributes;
            return ThisConverter(this);
        }

        /// <summary>
        /// Adds a parameter.
        /// </summary>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <returns></returns>
        public TThis Parameter(Type type, string name)
        {
            return Parameter(FieldDirection.In, new CodeTypeReference(type), name);
        }

        /// <summary>
        /// Adds a parameter.
        /// </summary>
        /// <param name="typeName">The type of the parameter.</param>
        /// <param name="name">The typename of the parameter.</param>
        /// <returns></returns>
        public TThis Parameter(string typeName, string name)
        {
            return Parameter(FieldDirection.In, new CodeTypeReference(typeName), name);
        }

        /// <summary>
        /// Adds a Parameter
        /// </summary>
        public TThis Parameter(FieldDirection direction, CodeTypeReference typeReference, string name)
        {
            var paramExpr = new CodeParameterDeclarationExpression(typeReference, name);
            paramExpr.Direction = direction;
            _wrappedType.Parameters.Add(paramExpr);
            return ThisConverter(this);
        }

        /// <summary>
        /// Adds a parameter
        /// </summary>
        /// <param name="direction">The direction if its a imput, output or parameter for both directions.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <returns></returns>
        public TThis Parameter(FieldDirection direction, Type type, string name)
        {
            var paramExpr = new CodeParameterDeclarationExpression(type, name);
            paramExpr.Direction = direction;
            _wrappedType.Parameters.Add(paramExpr);
            return ThisConverter(this);
        }

        //private FluentMethodBody _body;
        //public FluentMethodBody Body
        //{
        //    get
        //    {
        //        if (_body == null)
        //        {
        //            _body = new FluentMethodBody(ThisConverter(this));
        //        }

        //        return _body;
        //    }
        //}

        /////////////////////////////////////////////////////////////////
        //                         CodeTypeMember                      //
        /////////////////////////////////////////////////////////////////

        /// <summary>
        /// Adds a non documentation comment.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public TThis MethodComment(string comment)
        {
            _codeTypeMember.Comment(comment);
            return ThisConverter(this);
        }

        /// <summary>
        /// Comments the specified comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <param name="documentationComment"><c>true</c> if the comment is a documentation comment. otherwise <c>false</c>.</param>
        /// <returns></returns>
        public TThis MethodComment(string comment, bool documentationComment)
        {
            _codeTypeMember.Comment(comment, documentationComment);
            return ThisConverter(this);
        }

        public TThis MethodComment(CodeCommentStatement commentStatment)
        {
            _codeTypeMember.Comment(commentStatment);
            return ThisConverter(this);
        }

        public TThis CustomAttribute(Type attributeType, params CodeAttributeArgument[] arguments)
        {
            _codeTypeMember.CustomAttribute(attributeType, arguments);
            return ThisConverter(this);
        }

        public TThis CustomAttribute(CodeTypeReference attributeType, params CodeAttributeArgument[] arguments)
        {
            _codeTypeMember.CustomAttribute(attributeType, arguments);
            return ThisConverter(this);
        }

        public class FluentTypeMemberMethodDummy : FluentCodeTypeMember<FluentTypeMemberMethodDummy, TWrappedType, FluentCodeType<TParent>>
        {
            public FluentTypeMemberMethodDummy(TWrappedType wrappedMethod)
                : base(wrappedMethod)
            { }
        }

        /////////////////////////////////////////////////////////////////
        //                             End                             //
        /////////////////////////////////////////////////////////////////

        public TWrappedType EndFluent()
        {
            return _wrappedType;
        }

        /////////////////////////////////////////////////////////////////
        //                           Static                            //
        /////////////////////////////////////////////////////////////////

        public static CodeMemberMethod GetCodeMethod(FluentCodeMethodBase<TThis, TWrappedType, TParent> builder)
        {
            return builder._wrappedType;
        }

        /// <summary>
        /// Returnes the TypeBuilder of the property. Attention this property can return null.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static FluentCodeType<TParent> GetPrarent(FluentCodeMethodBase<TThis, TWrappedType, TParent> builder)
        {
            return builder._parent;
        }

        public static FluentTypeMemberMethodDummy GetFluentCodeTypeMember(FluentCodeMethodBase<TThis, TWrappedType, TParent> builder)
        {
            return builder._codeTypeMember;
        }
        /////////////////////////////////////////////////////////////////
        //                            Body                             //
        /////////////////////////////////////////////////////////////////

        #region IBodyProvider Member

        CodeStatementCollection ICodeBodyProvider.Statements
        {
            get
            {
                return FluentCodeMethodBase<TThis, TWrappedType, TParent>.GetCodeMethod(this).Statements;
            }
        }

        #endregion
    }
}
