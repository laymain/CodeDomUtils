using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public class FluentCodeConstructor<TParent> : FluentCodeMethodBase<FluentCodeConstructor<TParent>, CodeConstructor, TParent>
        where TParent : FluentCodeType<TParent>
    {
        public FluentCodeConstructor()
            : base(new CodeConstructor())
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
        public FluentCodeConstructor(CodeConstructor method, TParent typeBuider)
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
        public FluentCodeConstructor<TParent> BaseArgs(params CodeExpression[] arguments)
        {
            ((CodeConstructor)_wrappedType).BaseConstructorArgs.AddRange(arguments);
            return ThisConverter(this);
        }

        /// <summary>
        /// The chained constructor args.
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public FluentCodeConstructor<TParent> ThisArgs(params CodeExpression[] arguments)
        {
            ((CodeConstructor)_wrappedType).ChainedConstructorArgs.AddRange(arguments);
            return ThisConverter(this);
        }

        /////////////////////////////////////////////////////////////////
        //                             End                             //
        /////////////////////////////////////////////////////////////////

        public TParent EndConstructor
        {
            get
            {
                return (TParent)EndInternal;
            }
        }
    }
}
