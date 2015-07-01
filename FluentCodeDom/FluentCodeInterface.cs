namespace FluentCodeDom
{
    using System;
    using System.CodeDom;

    public class FluentCodeInterface : FluentCodeType<FluentCodeInterface>
    {
        public FluentCodeInterface() : this(new CodeTypeDeclaration())
        {
        }

        public FluentCodeInterface(CodeTypeDeclaration type) : this(type, null)
        {
        }

        public FluentCodeInterface(CodeTypeDeclaration type, FluentCodeNamespace ns) : base(type, ns)
        {
        }

        public FluentCodeInterface(MemberAttributes attributes, string interfaceName) : this(new CodeTypeDeclaration())
        {
            base.Name(interfaceName);
            base.Attributes(attributes);
            base.IsInterface();
        }

        public FluentCodeNamespace EndInterface
        {
            get
            {
                return this.EndInternal;
            }
        }
    }
}

