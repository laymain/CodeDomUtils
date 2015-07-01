using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    /// <summary>
    /// The base class for constructors and methods.
    /// </summary>
    public class FluentCodeMethod<TParent> : FluentCodeMethodBase<FluentCodeMethod<TParent>, CodeMemberMethod, TParent>
        where TParent : FluentCodeType<TParent>
    { 
        public FluentCodeMethod()
            : base(new CodeMemberMethod())
        {
        }

        public FluentCodeMethod(CodeMemberMethod method)
            : base(method)
        {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="method">The property.</param>
        /// <param name="typeBuider">Optional, can be null.</param>
        public FluentCodeMethod(CodeMemberMethod method, FluentCodeType<TParent> typeBuider)
            : base(method, typeBuider)
        {
        }

        public FluentCodeMethod<TParent> ReturnType(Type type)
        {
            return ReturnType(new CodeTypeReference(type));
        }

        public FluentCodeMethod<TParent> ReturnType(CodeTypeReference type)
        {
            _wrappedType.ReturnType = type;
            return ThisConverter(this);
        }

        /////////////////////////////////////////////////////////////////
        //                             End                             //
        /////////////////////////////////////////////////////////////////

        public TParent EndMethod
        {
            get
            {
                return (TParent)EndInternal;
            }
        }
    }
}
