using System;
using System.Collections.Generic;
using System.Text;

using System.CodeDom;

namespace FluentCodeDom
{
    public class FluentCodeType : FluentCodeTypeMember<FluentCodeType, CodeTypeDeclaration, FluentCodeNamespace>
    {
        public FluentCodeType()
            : this(new CodeTypeDeclaration())
        {
        }

        public FluentCodeType(MemberAttributes attributes, string className)
            : this(new CodeTypeDeclaration())
        {
            Name(className);
            Attributes(attributes);
            IsClass();
        }

        public FluentCodeType(CodeTypeDeclaration type)
            : this(type, null)
        {
        }

        public FluentCodeType(CodeTypeDeclaration type, FluentCodeNamespace ns)
            : base(type, ns)
        {
        }

        /// <summary>
        /// Indicates that this type inherits from anoter type.
        /// </summary>
        /// <param name="typeName">The name of the type to reference</param>
        /// <returns></returns>
        public FluentCodeType Inherits(string typeName)
        {
            return Inherits(new CodeTypeReference(typeName));
        }

        public FluentCodeType Inherits(Type type)
        {
            return Inherits(new CodeTypeReference(type));
        }

        public FluentCodeType Inherits(CodeTypeReference type)
        {
            _wrappedType.BaseTypes.Add(type);
            return this;
        }

        public FluentCodeType Name(string name)
        {
            _wrappedType.Name = name;
            return this;
        }

        public FluentCodeType Attributes(MemberAttributes attributes)
        {
            _wrappedType.Attributes = attributes;
            return this;
        }

        public FluentCodeType IsClass()
        {
            _wrappedType.IsClass = true;
            return this;
        }

        public FluentCodeType IsInterface()
        {
            _wrappedType.IsInterface = true;
            return this;
        }

        public FluentCodeConstructor Constructor()
        {
            var codeCtor = new CodeConstructor();
            _wrappedType.Members.Add(codeCtor);

            var constructor = new FluentCodeConstructor(codeCtor, this);
            return constructor;
        }

        public FluentCodeConstructor Constructor(MemberAttributes attributes)
        {
            var ctor = Constructor();
            ctor.Attributes(attributes);
            return ctor;
        }

        public FluentCodeField Field(Type type, string name)
        {
            return Field(MemberAttributes.Private, type, name);
        }

        public FluentCodeField Field(MemberAttributes attributes, Type type, string name)
        {
            return Field(attributes, type, name, null);
        }

        public FluentCodeField Field(MemberAttributes attributes, Type type, string name, CodeExpression defaultValue)
        {
            var field = new FluentCodeField(new CodeMemberField(), this);
            field.Attributes(attributes);
            field.Type(type);
            field.Name(name);

            if (defaultValue != null)
            {
                field.DefaultValue(defaultValue);
            }

            _wrappedType.Members.Add(field._wrappedType);
            return field;
        }

        public FluentCodeProperty Property(Type type, string name)
        {
            return Property(MemberAttributes.Private, type, name);
        }

        public FluentCodeProperty Property(MemberAttributes attributes, string typeName, string name)
        {
            return Property(attributes, new CodeTypeReference(typeName), name);
        }

        public FluentCodeProperty Property(MemberAttributes attributes, Type type, string name)
        {
            return Property(attributes, new CodeTypeReference(type), name);
        }

        public FluentCodeProperty Property(MemberAttributes attributes, CodeTypeReference type, string name)
        {
            var Property = new FluentCodeProperty(new CodeMemberProperty(), this);
            Property.Attributes(attributes);
            Property.Type(type);
            Property.Name(name);

            _wrappedType.Members.Add(Property._wrappedType);
            return Property;
        }

        public FluentCodeMethod Method(string name)
        { 
            return Method(MemberAttributes.Private, name);
        }

        public FluentCodeMethod Method(MemberAttributes attributes, string name)
        {
            var methodBuilder = new FluentCodeMethod(new CodeMemberMethod(), this);
            methodBuilder.Name(name);
            methodBuilder.Attributes(attributes);
            _wrappedType.Members.Add(methodBuilder._wrappedType);
            return methodBuilder;
        }
    }

    //public abstract class FluentCodeBody<TParent> : FluentTemplate<
    //{ 
    //    public FluentCodeBody()
    //    {
        
    //    }
    //}
}
