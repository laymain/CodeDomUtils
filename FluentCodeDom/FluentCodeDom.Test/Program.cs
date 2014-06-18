using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using Microsoft.VisualBasic;

namespace FluentCodeDom.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var codeDomProvider = new CodeDomProvider[]
            {
                new CSharpCodeProvider(),
                new VBCodeProvider()
            };

            foreach(CodeDomProvider provider in codeDomProvider)
            {
                DeclarationTest.CodeDomProvider = provider;

                DeclarationTest.DefintionTest();
                DeclarationTest.ConstructorTest();
                DeclarationTest.TryCatchTest();
                DeclarationTest.LoopTest();
                DeclarationTest.EnumTest();
                DeclarationTest.UsingTest();
                DeclarationTest.LinqTest();
            }
            Console.ReadLine();
        }
    }
}
