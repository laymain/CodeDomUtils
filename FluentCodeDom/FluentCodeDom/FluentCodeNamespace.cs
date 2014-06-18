using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public class FluentCodeNamespace : FluentTemplate<CodeNamespace, FluentCodeCompileUnit>
    {
        public FluentCodeNamespace()
            : this(new CodeNamespace(), null)
        { 
        }

        public FluentCodeNamespace(string name)
            : this(new CodeNamespace(name), null)
        { 
        }

        public FluentCodeNamespace(CodeNamespace ns, FluentCodeCompileUnit compileUnit)
            : base(ns, compileUnit)
        { 
        }

        /////////////////////////////////////////////////////////////////
        //                           Public                            //
        /////////////////////////////////////////////////////////////////

        public FluentCodeNamespace Name(string name)
        {
            _wrappedType.Name = name;
            return this;
        }

        /// <summary>
        /// Adds a namespace which should be used in the namespace.
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        public FluentCodeNamespace Import(string ns)
        {
            _wrappedType.Imports.Add(new CodeNamespaceImport(ns));
            return this;
        }

        /// <summary>
        /// Adds a new class to the namespace
        /// </summary>
        /// <param name="name">The name of the class.</param>
        /// <returns></returns>
        public FluentCodeClass Class(string name)
        {
            return Class(MemberAttributes.FamilyAndAssembly, name);
        }

        /// <summary>
        /// Adds a new class to the namespace
        /// </summary>
        /// <param name="attributes">The accessor and other member attributes.</param>
        /// <param name="name">The name of the class.</param>
        /// <returns></returns>
        public FluentCodeClass Class(MemberAttributes attributes, string name)
        {
            var typeBuilder = new FluentCodeClass(new CodeTypeDeclaration(), this);
            typeBuilder.Name(name);
            typeBuilder.IsClass();
            typeBuilder.Attributes(attributes);
            _wrappedType.Types.Add(typeBuilder._wrappedType);
            return typeBuilder;
        }

        /// <summary>
        /// Adds a new interface to the namespace
        /// </summary>
        /// <param name="name">The name of the class.</param>
        /// <returns></returns>
        public FluentCodeInterface Interface(string name)
        {
            return Interface(MemberAttributes.FamilyAndAssembly, name);
        }

        /// <summary>
        /// Adds a new interface to the namespace
        /// </summary>
        /// <param name="attributes">The member attributes.</param>
        /// <param name="name">The name of the class.</param>
        /// <returns></returns>
        public FluentCodeInterface Interface(MemberAttributes attributes, string name)
        {
            var typeBuilder = new FluentCodeInterface(new CodeTypeDeclaration(), this);
            typeBuilder.Name(name);
            typeBuilder.IsInterface();
            typeBuilder.Attributes(attributes);
            _wrappedType.Types.Add(typeBuilder._wrappedType);
            return typeBuilder;
        }

        public FluentCodeEnum Enum(string name)
        {
            return Enum(MemberAttributes.FamilyAndAssembly, name);
        }

        /// <summary>
        /// Adds a new enum parent
        /// </summary>
        /// <param name="attributes">The accessor attributes and other member attributes.</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public FluentCodeEnum Enum(MemberAttributes attributes, string name)
        {
            var enumDeclaration =  new CodeTypeDeclaration() { IsEnum = true };
            _wrappedType.Types.Add(enumDeclaration);

            enumDeclaration.Attributes = attributes;
            enumDeclaration.Name = name;

            return new FluentCodeEnum(enumDeclaration, this);
        }

        /////////////////////////////////////////////////////////////////
        //                           Public                            //
        /////////////////////////////////////////////////////////////////

        public FluentCodeCompileUnit EndNamespace
        {
            get
            {
                return EndInternal;
            }
        }
    }
}
