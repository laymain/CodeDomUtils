using System;
using System.Collections.Generic;
using System.Text;
using FluentCodeDom;
using System.CodeDom;
using System.Reflection;

namespace FluentCodeDom.Sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeCompileUnit compileUnit = new FluentCodeCompileUnit()
                .Namespace("Sample1")
                    .Class("Program")
                        .Method(MemberAttributes.Public | MemberAttributes.Static, "Main").Parameter(typeof(string[]), "args").Body
                            .CallStatic(typeof(Console), "WriteLine", Expr.Primitive("Hello Fluent CodeDom"))
                        .End.End
                    .End
                .End
            .EndFluent();

            Assembly assembly = Helper.CodeDomHelper.CompileInMemory(compileUnit);
            assembly.GetType("Sample1.Program").GetMethod("Main").Invoke(null, new object[] { null });
        }
    }
}
