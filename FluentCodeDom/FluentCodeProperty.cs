using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public class FluentCodeProperty : FluentCodeTypeMember<FluentCodeProperty, CodeMemberProperty, FluentCodeType>
    {
        public FluentCodeProperty()
            : this(new CodeMemberProperty())
        { 
        }

        public FluentCodeProperty(CodeMemberProperty property)
            : this(property, null)
        {

        }

        public FluentCodeProperty(CodeMemberProperty property, FluentCodeType type)
            : base(property, type)
        { 
        }

        public FluentCodeProperty Name(string name)
        {
            _wrappedType.Name = name;
            return this;
        }

        public FluentCodeProperty Attributes(MemberAttributes attributes)
        {
            _wrappedType.Attributes = attributes;
            return this;
        }

        public FluentCodeProperty Type(Type type)
        {
            _wrappedType.Type = new CodeTypeReference(type);
            return this;
        }

        public FluentCodeProperty Type(CodeTypeReference type)
        {
            _wrappedType.Type = type;
            return this;
        }

        /// <summary>
        /// Info: CodeDOM does not support different accessors for get and set.
        /// </summary>
        /// <returns></returns>
        public FluentCodePropertyBody Get
        {
            get
            {
                _wrappedType.HasGet = true;
                return new FluentCodePropertyBody(
                    this, new CodeBodyProvider(_wrappedType.GetStatements)
                    );
            }
        }

        public FluentCodePropertyBody Set
        {
            get
            {
                _wrappedType.HasGet = true;
                return new FluentCodePropertyBody(
                    this, new CodeBodyProvider(_wrappedType.SetStatements)
                    );
            }
        }

        /////////////////////////////////////////////////////////////////
        //                            Body                             //
        /////////////////////////////////////////////////////////////////

        public class FluentCodePropertyBody : FluentCodeBody<FluentCodeProperty, FluentCodePropertyBody>
        {
            public FluentCodePropertyBody(FluentCodeProperty property, ICodeBodyProvider bodyProvider)
                : base(property, bodyProvider)
            {
            }
        }
    }
}
