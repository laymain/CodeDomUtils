using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentCodeDom.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            DeclarationTest.DefintionTest();
            DeclarationTest.ConstructorTest();
            DeclarationTest.TryCatchTest();
            DeclarationTest.LoopTest();
            DeclarationTest.EnumTest();
            DeclarationTest.UsingTest();
            DeclarationTest.LinqTest();

            Console.ReadLine();
        }
    }
}
