using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public class FluentCodeField<TParent> : FluentCodeTypeMember<FluentCodeField<TParent>, CodeMemberField, TParent>
        where TParent : FluentCodeType<TParent>
    {
        public FluentCodeField()
            : this(new CodeMemberField())
        {
        }

        public FluentCodeField(CodeMemberField field)
            : this(field, null)
        {
        }

        public FluentCodeField(CodeMemberField field, FluentCodeType<TParent> type)
            : base(field, (TParent)type)
        {
        }

        /////////////////////////////////////////////////////////////////
        //                           Public                            //
        /////////////////////////////////////////////////////////////////

        public FluentCodeField<TParent> Name(string name)
        {
            _wrappedType.Name = name;
            return this;
        }

        public FluentCodeField<TParent> Type(Type type)
        {
            _wrappedType.Type = new CodeTypeReference(type);
            return this;
        }

        public FluentCodeField<TParent> Type(CodeTypeReference type)
        {
            _wrappedType.Type = type;
            return this;
        }

        public FluentCodeField<TParent> Attributes(MemberAttributes attributes)
        {
            _wrappedType.Attributes = attributes;
            return this;
        }

        public FluentCodeField<TParent> DefaultValue(CodeExpression expression)
        {
            _wrappedType.InitExpression = expression;
            return this;
        }

        /////////////////////////////////////////////////////////////////
        //                             End                             //
        /////////////////////////////////////////////////////////////////

        public TParent EndField
        {
            get
            {
                return EndInternal;
            }
        }

        /////////////////////////////////////////////////////////////////
        //                           Static                            //
        /////////////////////////////////////////////////////////////////

        public static CodeMemberField GetCodeField(FluentCodeField<TParent> field)
        {
            return field._wrappedType;
        }
    }
}
