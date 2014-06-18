using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.Reflection;
using FluentCodeDom;
using Microsoft.CSharp;

namespace FluentCodeDom.Sample5
{
    class Program
    {
        static void Main(string[] args)
        {
            var ns = new FluentCodeCompileUnit().Namespace("Sample5");
            ns.Class(MemberAttributes.Public | MemberAttributes.Static, "Program")
                .Method(MemberAttributes.Public | MemberAttributes.Static, "ForArrayTest")
                    .CallStatic(typeof(Console), "WriteLine", Expr.Primitive("ForArrayTest:"))
                    .Declare(typeof(string[]), "sArr", Expr.NewArray(typeof(string), Expr.Primitive(2)))
                    .Set(Expr.ArrayIndex(Expr.Var("sArr"), Expr.Primitive(0)), Expr.Primitive("Hallo1"))
                    .Set(Expr.ArrayIndex(Expr.Var("sArr"), Expr.Primitive(1)), Expr.Primitive("Hallo2"))

                    .ForArray("i", Expr.Var("sArr"))
                        .CallStatic(typeof(Console), "WriteLine", Expr.ArrayIndex(Expr.Var("sArr"), Expr.Var("i")))
                    .EndFor

                .EndMethod

                .Method(MemberAttributes.Public | MemberAttributes.Static, "ForeachTest")
                    .CallStatic(typeof(Console), "WriteLine", Expr.Primitive("ForeachTest:"))
                    .Declare(typeof(string[]), "sArr", Expr.NewArray(typeof(string), Expr.Primitive(2)))
                    .Set(Expr.ArrayIndex(Expr.Var("sArr"), Expr.Primitive(0)), Expr.Primitive("Hallo1"))
                    .Set(Expr.ArrayIndex(Expr.Var("sArr"), Expr.Primitive(1)), Expr.Primitive("Hallo2"))

                    .ForeachEmu(typeof(string), "s", Expr.Var("sArr"))
                        .CallStatic(typeof(Console), "WriteLine", Expr.Var("s"))
                    .EndForeach
                .EndMethod

                .Method(MemberAttributes.Public | MemberAttributes.Static, "WhileTest")
                    .CallStatic(typeof(Console), "WriteLine", Expr.Primitive("WhileTest:"))
                    .Declare(typeof(int), "a", Expr.Primitive(1))
                    .WhileEmu(Expr.Op(Expr.Var("a"), CodeBinaryOperatorType.LessThanOrEqual, Expr.Primitive(10)))
                        .CallStatic(typeof(Console), "WriteLine", Expr.CallStatic(typeof(string), "Format", Expr.Primitive("Value: {0}"), Expr.Var("a")))
                        .Set(Expr.Var("a"), Expr.Op(Expr.Var("a"), CodeBinaryOperatorType.Add, Expr.Primitive(1)))
                    .EndWhile
                .EndMethod
            .EndFluent();

            CodeCompileUnit compileUnit = ns.EndNamespace.EndFluent();
            Console.WriteLine(Helper.CodeDomHelper.GenerateCodeAsString(compileUnit, new CSharpCodeProvider()));

            Assembly assembly = Helper.CodeDomHelper.CompileInMemory(compileUnit);
            assembly.GetType("Sample5.Program").GetMethod("ForArrayTest").Invoke(null, new object[] { });
            assembly.GetType("Sample5.Program").GetMethod("ForeachTest").Invoke(null, new object[] { });
            assembly.GetType("Sample5.Program").GetMethod("WhileTest").Invoke(null, new object[] { });
        }
    }
}
