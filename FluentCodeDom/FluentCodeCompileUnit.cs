using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    /// <summary>
    /// A wrapper for a CodeCompileUnit
    /// </summary>
    public class FluentCodeCompileUnit : FluentTemplate<CodeCompileUnit>
    {
        public FluentCodeCompileUnit()
            : this(new CodeCompileUnit())
        { 
        }

        public FluentCodeCompileUnit(CodeCompileUnit codeCompileUnit)
            : base(codeCompileUnit)
        { 
        }

        /// <summary>
        /// Adds a new namespace to the compile unit
        /// </summary>
        /// <param name="name">The name of the namespace</param>
        public FluentCodeNamespace Namespace(string name)
        {
            var codeNamespace = new CodeNamespace(name);
            _wrappedType.Namespaces.Add(codeNamespace);

            return new FluentCodeNamespace(codeNamespace, this);
        }
    }
}
