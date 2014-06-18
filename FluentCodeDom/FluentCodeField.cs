using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public class FluentCodeField : FluentCodeTypeMember<FluentCodeField, CodeMemberField, FluentCodeType>
    {
        public FluentCodeField()
            : this(new CodeMemberField())
        {
        }

        public FluentCodeField(CodeMemberField field)
            : this(field, null)
        {
        }

        public FluentCodeField(CodeMemberField field, FluentCodeType type)
            : base(field, type)
        {
        }

        /////////////////////////////////////////////////////////////////
        //                           Public                            //
        /////////////////////////////////////////////////////////////////

        public FluentCodeField Name(string name)
        {
            _wrappedType.Name = name;
            return this;
        }

        public FluentCodeField Type(Type type)
        {
            _wrappedType.Type = new CodeTypeReference(type);
            return this;
        }

        public FluentCodeField Type(CodeTypeReference type)
        {
            _wrappedType.Type = type;
            return this;
        }

        public FluentCodeField Attributes(MemberAttributes attributes)
        {
            _wrappedType.Attributes = attributes;
            return this;
        }

        public FluentCodeField DefaultValue(CodeExpression expression)
        {
            _wrappedType.InitExpression = expression;
            return this;
        }

        /////////////////////////////////////////////////////////////////
        //                           Static                            //
        /////////////////////////////////////////////////////////////////

        public static CodeMemberField GetCodeField(FluentCodeField field)
        {
            return field._wrappedType;
        }
    }
}
