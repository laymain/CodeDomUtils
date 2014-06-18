using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    /// <summary>
    /// The base class for constructors and methods
    /// </summary>
    public class FluentCodeMethod : FluentCodeMethodBase<FluentCodeMethod >
    { 
        public FluentCodeMethod()
            : base()
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
        public FluentCodeMethod(CodeMemberMethod method, FluentCodeType typeBuider)
            : base(method, typeBuider)
        {
        }

        public FluentCodeMethod ReturnType(Type type)
        {
            return ReturnType(new CodeTypeReference(type));
        }

        public FluentCodeMethod ReturnType(CodeTypeReference type)
        {
            _wrappedType.ReturnType = type;
            return ThisConverter(this);
        }
    }
}
