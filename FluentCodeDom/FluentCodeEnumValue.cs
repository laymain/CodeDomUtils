using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public class FluentCodeEnumValue : FluentCodeTypeMember<FluentCodeEnumValue, CodeMemberField, FluentCodeEnum>
    {
        public FluentCodeEnumValue(CodeMemberField enumValue, FluentCodeEnum parent)
            : base(enumValue, parent)
        { 
        
        }

        public FluentCodeEnumValue Name(string name)
        {
            _wrappedType.Name = name;
            return this;
        }

        public FluentCodeEnumValue Value(CodeExpression value)
        {
            _wrappedType.InitExpression = value;
            return this;
        }

        /////////////////////////////////////////////////////////////////
        //                             End                             //
        /////////////////////////////////////////////////////////////////

        public FluentCodeEnum EndValue
        {
            get
            {
                return EndInternal;
            }
        }
    }
}
