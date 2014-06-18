using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.CodeDom;
using FluentCodeDom;

namespace FluentCodeDom.Sample3
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeCompileUnit compileUnit = new FluentCodeCompileUnit()
                .Namespace("Sample3")
                    .Class("Program")
                        .Method(MemberAttributes.Public | MemberAttributes.Static, "Main").Parameter(typeof(string[]), "args")
                            .ConsoleWriteLine(Expr.Primitive("Hello Fluent CodeDom")).UserData("Key", "Value")
                        .EndMethod
                    .EndClass
                .EndNamespace
            .EndFluent();

            Assembly assembly = Helper.CodeDomHelper.CompileInMemory(compileUnit);
            assembly.GetType("Sample3.Program").GetMethod("Main").Invoke(null, new object[] { null });
        }
    }

    public static class FluentCodeDomExtensions
    {
        /// <summary>
        /// Adds a Console.WriteLine Statement.
        /// </summary>
        /// <param name="textExpr"></param>
        /// <returns></returns>
        public static TThis ConsoleWriteLine<TParent, TThis>(this FluentCodeBody<TParent, TThis> body, params CodeExpression[] textExpr)
            where TThis : FluentCodeBody<TParent, TThis>
        {
            return body.CallStatic(typeof(Console), "WriteLine", textExpr);
        }

        /// <summary>
        /// Adds a keyValuePair the user data of the last added statement.
        /// </summary>
        /// <returns></returns>
        public static TThis UserData<TParent, TThis>(this FluentCodeBody<TParent, TThis> body, object key, object value)
            where TThis : FluentCodeBody<TParent, TThis>
        {
            ICodeBodyProvider bodyProvider = FluentCodeBody<TParent, TThis>.GetBodyProvider(body);
            if (bodyProvider.Statements.Count == 0)
                throw new InvalidOperationException("No statement till now added.");

            bodyProvider.Statements[bodyProvider.Statements.Count - 1].UserData.Add(key, value);

            return (TThis)(object)body;
        }
    }
}
