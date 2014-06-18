using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public class FluentCodeProperty<TParent> : FluentCodeTypeMember<FluentCodeProperty<TParent>, CodeMemberProperty, TParent>
        where TParent : FluentCodeType<TParent>
    {
        public FluentCodeProperty()
            : this(new CodeMemberProperty())
        { 
        }

        public FluentCodeProperty(CodeMemberProperty property)
            : this(property, null)
        {

        }

        public FluentCodeProperty(CodeMemberProperty property, FluentCodeType<TParent> type)
            : base(property, (TParent)type)
        { 
        }

        public FluentCodeProperty<TParent> Name(string name)
        {
            _wrappedType.Name = name;
            return this;
        }

        public FluentCodeProperty<TParent> Attributes(MemberAttributes attributes)
        {
            _wrappedType.Attributes = attributes;
            return this;
        }

        public FluentCodeProperty<TParent> Type(Type type)
        {
            _wrappedType.Type = new CodeTypeReference(type);
            return this;
        }

        public FluentCodeProperty<TParent> Type(CodeTypeReference type)
        {
            _wrappedType.Type = type;
            return this;
        }

        /// <summary>
        /// Info: CodeDOM does not support different accessors for get and set.
        /// </summary>
        /// <returns></returns>
        public FluentCodeGetPropertyBody Get
        {
            get
            {
                _wrappedType.HasGet = true;
                return new FluentCodeGetPropertyBody(
                    this, new CodeBodyProvider(_wrappedType.GetStatements)
                    );
            }
        }

        public FluentCodeSetPropertyBody Set
        {
            get
            {
                _wrappedType.HasGet = true;
                return new FluentCodeSetPropertyBody(
                    this, new CodeBodyProvider(_wrappedType.SetStatements)
                    );
            }
        }

        /////////////////////////////////////////////////////////////////
        //                             End                             //
        /////////////////////////////////////////////////////////////////

        public TParent EndProperty
        {
            get
            {
                return EndInternal;
            }
        }

        /////////////////////////////////////////////////////////////////
        //                            Body                             //
        /////////////////////////////////////////////////////////////////

        public class FluentCodeGetPropertyBody : FluentCodeBody<FluentCodeProperty<TParent>, FluentCodeGetPropertyBody>
        {
            public FluentCodeGetPropertyBody(FluentCodeProperty<TParent> property, ICodeBodyProvider bodyProvider)
                : base(property, bodyProvider)
            {
            }

            /////////////////////////////////////////////////////////////////
            //                             End                             //
            /////////////////////////////////////////////////////////////////

            public FluentCodeProperty<TParent> EndGet
            {
                get
                {
                    return EndInternal;
                }
            }
        }

        public class FluentCodeSetPropertyBody : FluentCodeBody<FluentCodeProperty<TParent>, FluentCodeSetPropertyBody>
        {
            public FluentCodeSetPropertyBody(FluentCodeProperty<TParent> property, ICodeBodyProvider bodyProvider)
                : base(property, bodyProvider)
            {
            }

            /////////////////////////////////////////////////////////////////
            //                             End                             //
            /////////////////////////////////////////////////////////////////

            public FluentCodeProperty<TParent> EndSet
            {
                get
                {
                    return EndInternal;
                }
            }
        }
    }
}
