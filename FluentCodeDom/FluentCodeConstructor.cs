using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public class FluentCodeConstructor : FluentCodeMethodBase<FluentCodeConstructor>
    {
        public FluentCodeConstructor()
            : base()
        {
        }

        public FluentCodeConstructor(CodeConstructor method)
            : base(method)
        {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="method">The property.</param>
        /// <param name="typeBuider">Optional, can be null.</param>
        public FluentCodeConstructor(CodeConstructor method, FluentCodeType typeBuider)
            : base(method, typeBuider)
        {
        }

        /////////////////////////////////////////////////////////////////
        //                       Constructor                           //
        /////////////////////////////////////////////////////////////////

        /// <summary>
        /// The base constructor args
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public FluentCodeConstructor BaseArgs(params CodeExpression[] arguments)
        {
            ((CodeConstructor)_wrappedType).BaseConstructorArgs.AddRange(arguments);
            return ThisConverter(this);
        }

        /// <summary>
        /// The chained constructor args.
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public FluentCodeConstructor ThisArgs(params CodeExpression[] arguments)
        {
            ((CodeConstructor)_wrappedType).ChainedConstructorArgs.AddRange(arguments);
            return ThisConverter(this);
        }
    }
}
