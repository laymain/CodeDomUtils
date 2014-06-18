using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.Reflection;
using FluentCodeDom;
using Microsoft.CSharp;

namespace FluentCodeDom.Sample4
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeCompileUnit compileUnit = new FluentCodeCompileUnit()
                .Namespace("Sample4")
                    .Class("Program")
                        .Method(MemberAttributes.Public | MemberAttributes.Static, "Main").Parameter(typeof(string[]), "args").Body
                            .Stmt(ExprLinq.Expr(() => Console.WriteLine("Hello Linq2CodeDOM")))
                            .Declare(typeof(int), "random", ExprLinq.Expr(() => new Random().Next(10)))
                            .If((int random) => random <= 5)
                                .Stmt(ExprLinq.Expr(() => Console.WriteLine("Smaller or equal to 5.")))
                            .Else
                                .Stmt(ExprLinq.Expr(() => Console.WriteLine("Bigger than 5.")))
                            .End
                        .End.End
                    .End
                .End
            .EndFluent();

            Console.WriteLine(Helper.CodeDomHelper.GenerateCodeAsString(compileUnit, new CSharpCodeProvider()));
            Assembly assembly = Helper.CodeDomHelper.CompileInMemory(compileUnit);
            assembly.GetType("Sample4.Program").GetMethod("Main").Invoke(null, new object[] { null });
        }
    }
}
