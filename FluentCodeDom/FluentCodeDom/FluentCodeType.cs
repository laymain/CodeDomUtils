using System;
using System.Collections.Generic;
using System.Text;

using System.CodeDom;

namespace FluentCodeDom
{
    /// <summary>
    /// A fluent wrapper for a CodeTypeDefinition.
    /// </summary>
    public abstract class FluentCodeType<TThis> : FluentCodeTypeMember<FluentCodeType<TThis>, CodeTypeDeclaration, FluentCodeNamespace>
        where TThis : FluentCodeType<TThis>
    {
        public FluentCodeType()
            : this(new CodeTypeDeclaration())
        {
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
        public FluentCodeType<TThis> Inherits(string typeName)
        {
            return Inherits(new CodeTypeReference(typeName));
        }

        public FluentCodeType<TThis> Inherits(Type type)
        {
            return Inherits(new CodeTypeReference(type));
        }

        public FluentCodeType<TThis> Inherits(CodeTypeReference type)
        {
            _wrappedType.BaseTypes.Add(type);
            return this;
        }

        /// <summary>
        /// The name of the type.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public FluentCodeType<TThis> Name(string name)
        {
            _wrappedType.Name = name;
            return this;
        }

        public FluentCodeType<TThis> Attributes(MemberAttributes attributes)
        {
            _wrappedType.Attributes = attributes;
            return this;
        }

        public TThis IsClass()
        {
            _wrappedType.IsClass = true;
            return (TThis)this;
        }

        public TThis IsInterface()
        {
            _wrappedType.IsInterface = true;
            return (TThis)this;
        }

        public FluentCodeConstructor<TThis> Constructor()
        {
            var codeCtor = new CodeConstructor();
            _wrappedType.Members.Add(codeCtor);

            var constructor = new FluentCodeConstructor<TThis>(codeCtor, (TThis)this);
            return constructor;
        }

        public FluentCodeConstructor<TThis> Constructor(MemberAttributes attributes)
        {
            var ctor = Constructor();
            ctor.Attributes(attributes);
            return ctor;
        }

        public FluentCodeField<TThis> Field(Type type, string name)
        {
            return Field(MemberAttributes.Private, type, name);
        }

        public FluentCodeField<TThis> Field(MemberAttributes attributes, Type type, string name)
        {
            return Field(attributes, type, name, null);
        }

        public FluentCodeField<TThis> Field(MemberAttributes attributes, Type type, string name, CodeExpression defaultValue)
        {
            var field = new FluentCodeField<TThis>(new CodeMemberField(), this);
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

        public FluentCodeProperty<TThis> Property(Type type, string name)
        {
            return Property(MemberAttributes.Private, type, name);
        }

        public FluentCodeProperty<TThis> Property(MemberAttributes attributes, string typeName, string name)
        {
            return Property(attributes, new CodeTypeReference(typeName), name);
        }

        public FluentCodeProperty<TThis> Property(MemberAttributes attributes, Type type, string name)
        {
            return Property(attributes, new CodeTypeReference(type), name);
        }

        public FluentCodeProperty<TThis> Property(MemberAttributes attributes, CodeTypeReference type, string name)
        {
            var Property = new FluentCodeProperty<TThis>(new CodeMemberProperty(), this);
            Property.Attributes(attributes);
            Property.Type(type);
            Property.Name(name);

            _wrappedType.Members.Add(Property._wrappedType);
            return Property;
        }

        public FluentCodeMethod<TThis> Method(string name)
        { 
            return Method(MemberAttributes.Private, name);
        }

        public FluentCodeMethod<TThis> Method(MemberAttributes attributes, string name)
        {
            var methodBuilder = new FluentCodeMethod<TThis>(new CodeMemberMethod(), this);
            methodBuilder.Name(name);
            methodBuilder.Attributes(attributes);
            _wrappedType.Members.Add(methodBuilder._wrappedType);
            return methodBuilder;
        }

        /// <summary>
        /// Defines a entry point where a .exe should start.
        /// </summary>
        /// <param name="attributes">The attributes for the method</param>
        /// <param name="name">The name of the method.</param>
        /// <returns></returns>
        public FluentCodeMethod<TThis> EntryPointMethod(MemberAttributes attributes, string name)
        {
            var methodBuilder = new FluentCodeMethod<TThis>(new CodeEntryPointMethod(), this);
            methodBuilder.Name(name);
            methodBuilder.Attributes(attributes);
            _wrappedType.Members.Add(methodBuilder._wrappedType);
            return methodBuilder;
        }

        /// <summary>
        /// Adds a base type. Its possible to define multiple base types.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public TThis BaseType(Type type)
        {
            _wrappedType.BaseTypes.Add(type);
            return (TThis)this;
        }

        /// <summary>
        /// Adds a base type. Its possible to define multiple base types.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public TThis BaseType(CodeTypeReference type)
        {
            _wrappedType.BaseTypes.Add(type);
            return (TThis)this;
        }

        public static CodeTypeDeclaration GetCodeType(FluentCodeType<TThis> type)
        {
            return type._wrappedType;
        }
    }

    //public abstract class FluentCodeBody<TParent> : FluentTemplate<
    //{ 
    //    public FluentCodeBody()
    //    {
        
    //    }
    //}
}
