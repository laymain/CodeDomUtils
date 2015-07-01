﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentCodeDom;
using System.CodeDom;
using FluentCodeDom.Helper;
using FluentCodeDom.Linq;
using System.IO;
using System.Reflection;

namespace FluentCodeDom.Test
{
    public class DeclarationTest
    {
        public static void DefintionTest()
        {
            var fluent = new FluentCodeCompileUnit();
            CodeCompileUnit compileUnit = fluent
                .Namespace("Test.FluentCodeDom.Test")
                    .Import("System")
                    .Import("System.Text")

                    .Class(MemberAttributes.Public, "StringModifier")
                        .Constructor()
                        .EndConstructor
                        
                        .Field(typeof(int), "_fld").EndField
                        .Field(MemberAttributes.Private, typeof(int), "_field1").EndField
                        .Field(MemberAttributes.Private, typeof(DateTime), "_now").EndField
                        .Field(typeof(DateTime), "_now2").EndField

                        .Field(typeof(int), "_intValue").EndField
                        .Property(MemberAttributes.Public, typeof(int), "IntValue")
                            .Get
                                .Return(Expr.Var("_intValue"))
                            .EndGet
                            .Set
                                .Set(Expr.Var("_intValue"), Expr.Value())
                            .EndSet
                        .EndProperty

                        .Method(MemberAttributes.Public, "OutMethod").Parameter(typeof(int), "outParam")
                            .Set(Expr.Arg("outParam"), Expr.Primitive(55))
                        .EndMethod

                        .Method(MemberAttributes.Public, "HelloWorld")
                            .CallStatic(typeof(Console), "WriteLine", Expr.Primitive("Hallo Welt"))
                            .CallStatic(typeof(Console), "ReadLine")
                        .EndMethod

                        .Method("StringSplit").Parameter(typeof(string), "s").Parameter(typeof(char), "seperationChar").ReturnType(typeof(string))
                            .Declare(typeof(string[]), "stringArr", Expr.CallMember(Expr.Arg("s"), "Split", Expr.Arg("seperationChar")))
                            
                            .If(Expr.Op(Expr.Primitive(5), CodeBinaryOperatorType.ValueEquality, Expr.Primitive(10)))
                                .Declare(typeof(int), "abc")
                                .Set(Expr.Var("abc"), Expr.Primitive(5))
                            .Else
                                .If(Expr.Primitive(false))
                                    .Call("HelloWorld")
                                .EndIf

                                .Declare(typeof(int[]), "array", Expr.NewArray(typeof(int), Expr.Primitive(5) ))
                                .ForArray("i", Expr.Var("array"))
                                .EndFor
                            .EndIf

                            .Return(Expr.ArrayIndex(Expr.Var("stringArr"), Expr.Primitive(0)))
                        .EndMethod
                    .EndClass

                    .Enum(MemberAttributes.Public, "SuperEnum")
                        .Value("Unit", 1).EndValue
                        .Value("Testing", 2).EndValue
                        .Value("Sucks").EndValue
                    .EndEnum
                .EndNamespace
            .EndFluent();

            TestGenerated(compileUnit);
        }

        public static void ConstructorTest()
        { 
            FluentCodeCompileUnit compileUnit = new FluentCodeCompileUnit();
            compileUnit.Namespace("TestNamespace")
                .Class("AClass").Inherits("BaseClass")
                    .Constructor(MemberAttributes.Public)
                        .BaseArgs()
                    .EndConstructor

                    .Constructor(MemberAttributes.Public).Parameter(typeof(int), "IntArg")
                        .ThisArgs(Expr.Arg("IntArg"), Expr.Primitive("Hello"))
                    .EndConstructor

                    .Constructor(MemberAttributes.Public).Parameter(typeof(int), "IntArg").Parameter(typeof(string), "StringArg")
                        .BaseArgs(Expr.Arg("StringArg"))
                    .EndConstructor
                .EndClass

                .Class("BaseClass")
                    .Constructor(MemberAttributes.Public)
                    .EndConstructor

                    .Constructor(MemberAttributes.Public).Parameter(typeof(string), "TextToPrint")
                        .CallStatic(typeof(Console), "WriteLine", Expr.Arg("TextToPrint"))
                    .EndConstructor
                .EndClass
            .EndFluent();

            Assembly assembly = TestGenerated(compileUnit.EndFluent());

            object instance = GetClassInstance("TestNamespace.AClass", assembly);
            instance.GetType().GetConstructor(new Type[] { typeof(int)}).Invoke(instance, new object[] { 32 });
        }

        public static void EnumTest()
        {
            FluentCodeCompileUnit compileUnit = new FluentCodeCompileUnit();
            compileUnit.Namespace("TestNamespace")
                .Enum("TestEnum").CustomAttribute(typeof(FlagsAttribute))
                    .Value("Value1", Expr.Primitive(1)).EndValue
                    .Value("Value2", Expr.Primitive(2)).EndValue
                    .Value("Value3", Expr.Primitive(4)).EndValue
                .EndEnum
            .EndFluent();

            TestGenerated(compileUnit.EndFluent());
        }

        public static void ClassDeclaration()
        {
            CodeTypeDeclaration cls = new FluentCodeClass()
                .Method("TestMethod").Attributes(MemberAttributes.Public).Parameter(typeof(string), "parameter1").ReturnType(typeof(int))
                    .Declare(typeof(DateTime), "supervar", Expr.Primitive(5))
                    .Call("method")
                    .Return()
                .EndMethod
            .EndFluent();

            var type2 = new FluentCodeClass().Name("MathType");
            type2.Method("Add").Parameter(typeof(int), "IntA").Parameter(typeof(int), "IntB")
                .Return((int a, int b) => a + b)
            .EndFluent();

            type2.Method("Subtract").Parameter(typeof(int), "IntA").Parameter(typeof(int), "IntB")
                .Return((int a, int b) => a - b)
            .EndFluent();

            var compileUnit = new CodeCompileUnit();
            var ns = new CodeNamespace();
            ns.Imports.Add(new CodeNamespaceImport("Test"));
            compileUnit.Namespaces.Add(ns);

            TestGenerated(compileUnit);
        }

        public static void TryCatchTest()
        {
            var ns = new FluentCodeCompileUnit().Namespace("TestNamespace")
                .Import("System");

            FluentCodeClass type = ns.Class(MemberAttributes.Public, "TryCatchClass");

            type.Method(MemberAttributes.Public, "TryCatchMethod")
                .Try
                    .Throw(Expr.New(typeof(Exception), Expr.Primitive("Evil Exception")))
                .Catch(typeof(Exception), "ex")
                    .CallStatic(typeof(Console), "WriteLine", Expr.Primitive("Catch Block"))
                .Finally
                    .CallStatic(typeof(Console), "WriteLine", Expr.Primitive("Finally Block"))
                .EndTry
            .EndMethod.EndFluent();

            CodeCompileUnit compileUnit = ns.EndNamespace.EndFluent();
            TestGenerated(compileUnit);
        }

        public static void LinqTest()
        {
            var ns = new FluentCodeCompileUnit().Namespace("TestNamespace");
            ns.Class("AClass")
                .Method(MemberAttributes.Public, "LinqTest").ReturnType(typeof(bool))
                    .Declare(typeof(int), "a", Expr.Primitive(5))
                    .Declare(typeof(int), "b", Expr.Primitive(6))
                    .If((int a, int b) => a > b)
                        .Declare(typeof(string), "text", Expr.Primitive("A is bigger than B"))
                        .Stmt((string text) => Console.WriteLine(text))
                        .Stmt(ExprLinq.Expr(() => Console.Read()))
                    .Else
                        .Declare(typeof(string), "text", Expr.Primitive("B is bigger or equal A"))
                        .Stmt((string text) => Console.WriteLine(text))
                    .EndIf

                    .Return((int a, int b) => a > b)
                .EndMethod
            .EndFluent();

            CodeCompileUnit compileUnit = ns.EndNamespace.EndFluent();
            Assembly assembly = TestGenerated(compileUnit);

            object instance = GetClassInstance("TestNamespace.AClass", assembly);
            instance.GetType().GetMethod("LinqTest").Invoke(instance, new object[] { });
        }

        public static void LoopTest()
        { 
            var ns = new FluentCodeCompileUnit().Namespace("TestNamespace");
            ns.Class("AClass")
                .Method(MemberAttributes.Public, "ForArrayTest")
                    .CallStatic(typeof(Console), "WriteLine", Expr.Primitive("ForArrayTest:"))
                    .Declare(typeof(string[]), "sArr", Expr.NewArray(typeof(string), Expr.Primitive(2)))
                    .Set(Expr.ArrayIndex(Expr.Var("sArr"), Expr.Primitive(0)), Expr.Primitive("Hallo1"))
                    .Set(Expr.ArrayIndex(Expr.Var("sArr"), Expr.Primitive(1)), Expr.Primitive("Hallo2"))

                    .ForArray("i", Expr.Var("sArr"))
                        .CallStatic(typeof(Console), "WriteLine", Expr.ArrayIndex(Expr.Var("sArr"), Expr.Var("i")))
                    .EndFor
                .EndMethod

                .Method(MemberAttributes.Public, "ForeachTest")
                    .CallStatic(typeof(Console), "WriteLine", Expr.Primitive("ForeachTest:"))
                    .Declare(typeof(string[]), "sArr", Expr.NewArray(typeof(string), Expr.Primitive(2)))
                    .Set(Expr.ArrayIndex(Expr.Var("sArr"), Expr.Primitive(0)), Expr.Primitive("Hallo1"))
                    .Set(Expr.ArrayIndex(Expr.Var("sArr"), Expr.Primitive(1)), Expr.Primitive("Hallo2"))

                    .ForeachEmu(typeof(string), "s", Expr.Var("sArr"))
                        .CallStatic(typeof(Console), "WriteLine", Expr.Var("s"))
                    .EndForeach
                .EndMethod

                .Method(MemberAttributes.Public, "WhileTest")
                    .CallStatic(typeof(Console), "WriteLine", Expr.Primitive("WhileTest:"))
                    .Declare(typeof(int), "a", Expr.Primitive(1))
                    .WhileEmu(Expr.Op(Expr.Var("a"), CodeBinaryOperatorType.LessThanOrEqual, Expr.Primitive(10)))
                        .CallStatic(typeof(Console), "WriteLine", Expr.CallStatic(typeof(string), "Format", Expr.Primitive("Value: {0}"), Expr.Var("a")))
                        .Set(Expr.Var("a"), Expr.Op(Expr.Var("a"), CodeBinaryOperatorType.Add, Expr.Primitive(1)))
                    .EndWhile
                .EndMethod
            .EndFluent();

            CodeCompileUnit compileUnit = ns.EndNamespace.EndFluent();
            Assembly assembly = TestGenerated(compileUnit);

            object instance = GetClassInstance("TestNamespace.AClass", assembly);
            instance.GetType().GetMethod("ForArrayTest").Invoke(instance, new object[] { });
            instance.GetType().GetMethod("ForeachTest").Invoke(instance, new object[] { });
            instance.GetType().GetMethod("WhileTest").Invoke(instance, new object[] { });
        }

        private static object GetClassInstance(string className, Assembly assembly)
        {
            Type aType = assembly.GetType(className);
            object instance = aType.GetConstructor(new Type[] { }).Invoke(new object[] { });

            return instance;
        }

        public static void UsingTest()
        {
            string executingAssemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executingAssemblyPath) + "\\..\\..\\test.txt";

            var ns = new FluentCodeCompileUnit().Namespace("TestNamespace");
            ns.Class("AClass")
                .Method(MemberAttributes.Public, "UsingTest")
                    .Declare(typeof(System.IO.FileStream), "fs", Expr.CallStatic(typeof(System.IO.File), "OpenRead", Expr.Primitive(path)))
                    .UsingEmu(Expr.Var("fs"))
                        .UsingEmu(Expr.Declare(typeof(StreamReader), "sr", Expr.New(typeof(StreamReader), Expr.Var("fs"))))
                            .CallStatic(typeof(Console), "WriteLine", Expr.CallMember(Expr.Var("sr"), "ReadToEnd"))
                        .EndUsing
                    .EndUsing
                .EndMethod
            .EndFluent();

            CodeCompileUnit compileUnit = ns.EndNamespace.EndFluent();
            Assembly assembly = TestGenerated(compileUnit);

            object instance = GetClassInstance("TestNamespace.AClass", assembly);
            instance.GetType().GetMethod("UsingTest").Invoke(instance, new object[] { });
        }

        public static System.CodeDom.Compiler.CodeDomProvider CodeDomProvider = new Microsoft.CSharp.CSharpCodeProvider();

        private static Assembly TestGenerated(CodeCompileUnit compileUnit)
        {
            string code = CodeDomHelper.GenerateCodeAsString(compileUnit, DeclarationTest.CodeDomProvider);
            Console.Write(code);

            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            Assembly assembly = CodeDomHelper.CompileInMemory(new CodeCompileUnit[] { compileUnit });

            sw.Stop();
            Console.WriteLine("Required Time to compile {0} milliseconds", sw.ElapsedMilliseconds);
            sw.Reset();

            return assembly;
        }
    }
}