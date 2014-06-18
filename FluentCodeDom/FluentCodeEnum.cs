using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    public class FluentCodeEnum : FluentCodeTypeMember<FluentCodeEnum, CodeTypeDeclaration, FluentCodeNamespace>
    {
        public FluentCodeEnum()
            : this(new CodeTypeDeclaration() { IsEnum = true }, null)
        {

        }

        public FluentCodeEnum(CodeTypeDeclaration enumType)
            : this(enumType, null)
        { 
        
        }

        public FluentCodeEnum(CodeTypeDeclaration enumType, FluentCodeNamespace parent)
            : base(enumType, parent)
        { 
        
        }

        public FluentCodeEnum Name(string name)
        {
            _wrappedType.Name = name;
            return this;
        }

        /// <summary>
        /// Defines a value for the enum
        /// </summary>
        public FluentCodeEnumValue Value(string key)
        {
            return Value(key, null);
        }

        /// <summary>
        /// Defines a value for the enum
        /// </summary>
        /// <param name="key">The name of the enum value</param>
        /// <param name="value">The enum number</param>
        /// <returns></returns>
        public FluentCodeEnumValue Value(string key, CodeExpression value)
        {
            var field = new CodeMemberField();
            var enumValue = new FluentCodeEnumValue(field, this);
            enumValue.Name(key);

            if (value != null)
            {
                enumValue.Value(value);
            }

            _wrappedType.Members.Add(field);

            return enumValue;
        }

        public FluentCodeEnumValue Value(string key, int value)
        {
            return Value(key, Expr.Primitive(value));
        }
    }
}
