using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using System.Reflection;
using FluentCodeDom;
using Microsoft.CSharp;

namespace FluentCodeDom.Sample2
{
    public class Program
    {
        static void Main(string[] args)
        {
            ISample sample = ImplentInterface<ISample>();
            sample.Method1("Hello");
            sample.Method2(32, 65);

            Console.ReadLine();
        }

        private static TInterface ImplentInterface<TInterface>()
        {
            Type interfaceType = typeof(TInterface);
            FluentCodeNamespace ns = new FluentCodeCompileUnit().Namespace("Sample2");

            // public class <Interface>Test : <Interface>
            string typeName = string.Format("{0}Test", interfaceType.Name);
            var type = ns.Class(typeName).Inherits(interfaceType);

            foreach (MethodInfo methodInfo in interfaceType.GetMethods())
            {
                // Console.WriteLine("<Method> called with Parameters:")
                var method =
                    type.Method(MemberAttributes.Public, methodInfo.Name)
                        .CallStatic(typeof(Console), "WriteLine", Expr.Primitive(string.Format("{0} called with parameters:", methodInfo.Name)));

                foreach (ParameterInfo paramInfo in methodInfo.GetParameters())
                {
                    method.Parameter(paramInfo.ParameterType, paramInfo.Name);

                    // Console.WriteLine("<ParamName>: <Value>")
                    method.CallStatic(typeof(Console), "WriteLine",
                            Expr.CallStatic(typeof(string), "Format",
                                Expr.Primitive(string.Format("{0}: {1}", paramInfo.Name, "{1}")),
                                Expr.CallMember(Expr.Arg(paramInfo.Name), "ToString")
                                )
                            );
                }
            }

            CodeCompileUnit compileUnit = ns.EndNamespace.EndFluent();

            // Display Code
            string code = Helper.CodeDomHelper.GenerateCodeAsString(compileUnit, new CSharpCodeProvider());
            Console.WriteLine(code);
            Assembly assembly = Helper.CodeDomHelper.CompileInMemory(compileUnit);

            return (TInterface)assembly.GetType(string.Format("Sample2.{0}", typeName))
                .GetConstructor(Type.EmptyTypes)
                .Invoke(new object[] { });
        }
    }

    public interface ISample
    {
        void Method1(string strParam);

        void Method2(int aNumber, float aFloat);
    }
}
