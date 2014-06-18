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
    public abstract class FluentCodeMethodBase<TThis> : FluentCodeTypeMember<TThis, CodeMemberMethod, FluentCodeType>, ICodeBodyProvider
        where TThis : FluentCodeMethodBase<TThis>
    {
        public FluentCodeMethodBase()
            : this(new CodeMemberMethod())
        {
        }

        public FluentCodeMethodBase(CodeMemberMethod method)
            : this(method, null)
        {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="method">The property.</param>
        /// <param name="typeBuider">Optional, can be null.</param>
        public FluentCodeMethodBase(CodeMemberMethod method, FluentCodeType typeBuider)
            : base(method, typeBuider)
        {
        }

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

        private FluentMethodBody _body;
        public FluentMethodBody Body
        {
            get
            {
                if (_body == null)
                {
                    _body = new FluentMethodBody(ThisConverter(this));
                }

                return _body;
            }
        }

        /////////////////////////////////////////////////////////////////
        //                           Static                            //
        /////////////////////////////////////////////////////////////////

        public static CodeMemberMethod GetCodeMethod(FluentCodeMethodBase<TThis> builder)
        {
            return builder._wrappedType;
        }

        /// <summary>
        /// Returnes the TypeBuilder of the property. Attention this property can return null.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static FluentCodeType GetPrarent(FluentCodeMethodBase<TThis> builder)
        {
            return builder._parent;
        }

        /////////////////////////////////////////////////////////////////
        //                            Body                             //
        /////////////////////////////////////////////////////////////////

        #region IBodyProvider Member

        CodeStatementCollection ICodeBodyProvider.Statements
        {
            get
            {
                return FluentCodeMethodBase<TThis>.GetCodeMethod(this).Statements;
            }
        }

        #endregion

        public class FluentMethodBody : FluentCodeBody<TThis, FluentMethodBody>
        {
            public FluentMethodBody(TThis method)
                : base(method, method)
            {
            }
        }
    }
}
