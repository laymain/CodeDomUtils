using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom.Compiler;
using System.CodeDom;

namespace FluentCodeDom.Helper
{
    /// <summary>
    /// Exception which will thrown when a CodeCompileUnit contains errors.
    /// </summary>
    public class CodeCompilerException : Exception
    {
        public CodeCompilerException(
            string message,
            CodeCompileUnit[] codeCompileUnits,
            CompilerErrorCollection errors,
            CodeDomProvider codeDomProvider
            )
            : base(message)
        {
            CodeCompileUnit = codeCompileUnits;
            CompilerErros = errors;
        }

        public CodeCompilerException(
            string message,
            Exception innerException,
            CodeCompileUnit[] codeCompileUnits,
            CompilerErrorCollection errors,
            CodeDomProvider codeDomProvider
            )
            : base(message, innerException)
        {
            CodeCompileUnit = codeCompileUnits;
            CompilerErros = errors;
        }

        public CompilerErrorCollection CompilerErros { get; protected set; }

        /// <summary>
        /// Can be null
        /// </summary>
        public CodeCompileUnit[] CodeCompileUnit { get; protected set; }

        public CodeDomProvider CodeDomProvider { get; protected set; }

        public static CodeCompilerException NewFormat(
            CompilerErrorCollection errors, 
            CodeCompileUnit[] codeCompileUnits, 
            CodeDomProvider codeDomProvider,
            string message, 
            params object[] args
            )
        {
            return new CodeCompilerException(
                string.Format(message, args),
                codeCompileUnits,
                errors,
                codeDomProvider
                );
        }
    }
}
