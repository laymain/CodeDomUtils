using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public class ForCodeBody<TParent> :
        IteratorCodeBody<TParent, ForCodeBody<TParent>>
    {
        public ForCodeBody(CodeIterationStatement iteratorStatement, TParent parent)
            : base(iteratorStatement, parent)
        { 
        }

        /////////////////////////////////////////////////////////////////
        //                           Public                            //
        /////////////////////////////////////////////////////////////////

        public TParent EndFor
        {
            get
            {
                return EndInternal;
            }
        }
    }
}
