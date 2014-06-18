using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public interface ICodeBodyProvider
    {
        CodeStatementCollection Statements { get; }
    }
}
