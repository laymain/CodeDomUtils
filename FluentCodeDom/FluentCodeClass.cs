namespace FluentCodeDom
{
    using System;
    using System.CodeDom;

    public class FluentCodeClass : FluentCodeType<FluentCodeClass>
    {
        public FluentCodeClass() : this(new CodeTypeDeclaration())
        {
        }

        public FluentCodeClass(CodeTypeDeclaration type) : this(type, null)
        {
        }

        public FluentCodeClass(CodeTypeDeclaration type, FluentCodeNamespace ns) : base(type, ns)
        {
        }

        public FluentCodeClass(MemberAttributes attributes, string className) : this(new CodeTypeDeclaration())
        {
            base.Name(className);
            base.Attributes(attributes);
            base.IsClass();
        }

        public FluentCodeNamespace EndClass
        {
            get
            {
                return this.EndInternal;
            }
        }
    }
}

