using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace FluentCodeDom.Helper
{
    public class CodeDomHelper
    {
        /// <summary>
        /// Compiles the compile units with all currently loaded assemblies. Info: Dynamic
        /// assemblies cannot used for compiling and will ignored.
        /// </summary>
        /// <param name="codeCompileUnits">The compile units which should be used for compiling.</param>
        /// <returns></returns>
        public static Assembly CompileInMemory(params CodeCompileUnit[] codeCompileUnits)
        {
            // All assemblyLocations loaded.
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var assemblyLocations = new List<string>();
            foreach(Assembly assembly in assemblies)
            {
                if(IsBlacklistedAssembly(assembly.FullName))
                    continue;

                // http://stackoverflow.com/questions/1423733/how-to-tell-if-a-net-assembly-is-dynamic
                // Is dynamic assembly
                if (assembly.ManifestModule is System.Reflection.Emit.ModuleBuilder)
                {
                    continue;
                }

                string assemblyLocation;
                try
                {
                    assemblyLocation = assembly.Location;
                }

                // In case this is a dynamic generated assembly it will thrown a not supported exception.
                // This is for the case the manifest module thing doesn't work
                catch (NotSupportedException)
                {
                    continue;
                }
            }

            return CompileInMemory(
                codeCompileUnits,
                assemblyLocations.ToArray()
                );
        }

        /// <summary>
        /// Assemblies by added by visual stuido in debugging.
        /// </summary>
        /// <param assemblyFullName="assemblyFullName"></param>
        /// <returns></returns>
        protected static bool IsBlacklistedAssembly(string assemblyFullName)
        {
            return assemblyFullName.Contains("Microsoft.VisualStudio.HostingProcess.Utilities.Sync") ||
                    assemblyFullName.Contains("Microsoft.VisualStudio.Debugger.Runtime");
        }

        public static Assembly CompileInMemory(CodeCompileUnit[] codeCompileUnits, string[] assemblyNames)
        {
            // TODO: better way?
            var codeProvider = new Microsoft.CSharp.CSharpCodeProvider();
            var compileOptions = new CompilerParameters();
            compileOptions.GenerateInMemory = true;

            compileOptions.ReferencedAssemblies.AddRange(assemblyNames);
            CompilerResults results = codeProvider.CompileAssemblyFromDom(compileOptions, codeCompileUnits);
            if (results.Errors.Count > 0)
            {
                List<string> errors = new List<string>();
                foreach (CompilerError error in results.Errors)
                { 
                    errors.Add(string.Format("{0} Line {1} Column {2}", error.ErrorText, error.Line, error.Column));
                }

                StringBuilder sb = new StringBuilder();
                foreach (string error in errors)
                {
                    sb.AppendFormat("\n\n{0}", error);
                }
                string errorText = sb.ToString();

                throw CodeCompilerException.NewFormat(
                    results.Errors,
                    codeCompileUnits,
                    codeProvider,
                    "Errors in compiling CodeCompileUnits: {0}",
                    errorText
                    );
            }

            return results.CompiledAssembly;
        }

        /////////////////////////////////////////////////////////////////
        //                       Code Generation                       //
        /////////////////////////////////////////////////////////////////

        private static CodeGeneratorOptions DefaultGeneratorOptions
        {
            get
            {
                // http://msdn.microsoft.com/de-de/library/system.codedom.compiler.codegeneratoroptions.aspx
                return new CodeGeneratorOptions()
                {
                    BlankLinesBetweenMembers = true,
                    BracingStyle = "C", // Blockwise
                    IndentString = "\t"
                };
            }

        }

        public static string GenerateCodeAsString(CodeCompileUnit codeCompileUnit, CodeDomProvider codeDomProvider)
        {
            return GenerateCodeAsString(codeCompileUnit, codeDomProvider, CodeDomHelper.DefaultGeneratorOptions);
        }

        /// <summary>
        /// Gets the code as a string.
        /// </summary>
        /// <param assemblyFullName="codeDomProvider"></param>
        /// <returns></returns>
        public static string GenerateCodeAsString(CodeCompileUnit codeCompileUnit, CodeDomProvider codeDomProvider, CodeGeneratorOptions generatorOptions)
        {
            using (Stream codeStream = GenerateCodeAsStream(codeCompileUnit, codeDomProvider, generatorOptions))
            {
                var textReader = new StreamReader(codeStream);
                return textReader.ReadToEnd();
            }
        }

        public static Stream GenerateCodeAsStream(CodeCompileUnit codeCompileUnit, CodeDomProvider codeDomProvider)
        {
            return GenerateCodeAsStream(codeCompileUnit, codeDomProvider, CodeDomHelper.DefaultGeneratorOptions);
        }

        /// <summary>
        /// Gets the code generated for the code unit.
        /// </summary>
        /// <param assemblyFullName="?"></param>
        /// <returns></returns>
        public static Stream GenerateCodeAsStream(CodeCompileUnit codeCompileUnit, CodeDomProvider codeDomProvider, CodeGeneratorOptions generatorOptions)
        {
            var memStream = new MemoryStream();
            var textWriter = new StreamWriter(memStream);

            codeDomProvider.GenerateCodeFromCompileUnit(codeCompileUnit, textWriter, generatorOptions);

            textWriter.Flush();
            memStream.Position = 0;

            return memStream;
        }
    }
}


            //using (var memStream = new MemoryStream())
            //{
            //    var textWriter = new StreamWriter(memStream);

            //     http://msdn.microsoft.com/de-de/library/system.codedom.compiler.codegeneratoroptions.aspx
            //    var generatorOptions =  new CodeGeneratorOptions() 
            //    {
            //        BlankLinesBetweenMembers = true,
            //        BracingStyle = "C", // Blockwise
            //        IndentString= "\t"
            //    };

            //    var proxyGen = new ProxyGen();
            //    CodeCompileUnit codeUnit = proxyGen.GenerateProxyClass(typeof(ITestInterface));
            //    codeDomProvider.GenerateCodeFromCompileUnit(codeUnit, textWriter, generatorOptions);

            //    textWriter.Flush();
            //    memStream.Position = 0;
            //    var textReader = new StreamReader(memStream);
            //    string code = textReader.ReadToEnd();
            //    Console.Write(code);
            //}


