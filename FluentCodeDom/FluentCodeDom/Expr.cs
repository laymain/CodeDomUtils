using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    /// <summary>
    /// Fluent CodeDOM Expressions
    /// </summary>
    public static partial class Expr
    {
        /////////////////////////////////////////////////////////////////
        //                           New                               //
        /////////////////////////////////////////////////////////////////

        /// <summary>
        /// Declares a new variable.
        /// </summary>
        /// <returns></returns>
        public static CodeVariableDeclarationStatement Declare(Type type, string name)
        {
            return new CodeVariableDeclarationStatement(type, name);
        }

        /// <summary>
        /// Declares a new variable.
        /// </summary>
        public static CodeVariableDeclarationStatement Declare(Type type, string name, CodeExpression initExpression)
        {
            return new CodeVariableDeclarationStatement(type, name, initExpression);
        }

        /// <summary>
        /// Declares a new variable.
        /// </summary>
        /// <returns></returns>
        public static CodeVariableDeclarationStatement Declare(CodeTypeReference type, string name)
        {
            return new CodeVariableDeclarationStatement(type, name);
        }

        /// <summary>
        /// Declares a new variable.
        /// </summary>
        public static CodeVariableDeclarationStatement Declare(CodeTypeReference type, string name, CodeExpression initExpression)
        {
            return new CodeVariableDeclarationStatement(type, name, initExpression);
        }

        /// <summary>
        /// Creates a new instance of a parent.
        /// </summary>
        /// <param name="typeName">The name of the type which should be created.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static CodeObjectCreateExpression New(string typeName, params CodeExpression[] parameters)
        {
            return New(new CodeTypeReference(typeName), parameters);
        }

        /// <summary>
        /// Creates a new instance of a parent.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static CodeObjectCreateExpression New(Type type, params CodeExpression[] parameters)
        {
            return New(new CodeTypeReference(type), parameters);
        }

        /// <summary>
        /// Creates a new instance of a parent.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static CodeObjectCreateExpression New(CodeTypeReference type, params CodeExpression[] parameters)
        {
            return new CodeObjectCreateExpression(type, parameters);
        }

        /// <summary>
        /// Creates a new instance of an array.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public static CodeArrayCreateExpression NewArray(CodeTypeReference type, CodeExpression size)
        {
            return new CodeArrayCreateExpression(type, size);   
        }

        /// <summary>
        /// Creates a new instance of an array.
        /// </summary>
        /// <param name="type">The item type of the array.</param>
        /// <param name="size">The size of the array.</param>
        /// <returns></returns>
        public static CodeArrayCreateExpression NewArray(Type type, CodeExpression size)
        {
            return new CodeArrayCreateExpression(type, size);
        }

        /////////////////////////////////////////////////////////////////
        //                        Methods                              //
        /////////////////////////////////////////////////////////////////

        public static CodeMethodInvokeExpression Call(string method)
        { 
            return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(Expr.This, method));
        }

        /// <summary>
        /// Calls a property of this instance
        /// </summary>
        /// <param name="method">The name of teh method which should be called.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static CodeMethodInvokeExpression Call(string method, params CodeExpression[] parameters)
        {
            return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(Expr.This, method), parameters);
        }

        public static CodeMethodInvokeExpression Call(CodeMethodReferenceExpression method, CodeExpression[] parameters)
        {
            return new CodeMethodInvokeExpression(method, parameters);
        }

        /// <summary>
        /// Calls a method of an object.
        /// </summary>
        /// <returns></returns>
        public static CodeMethodInvokeExpression CallMember(CodeExpression targetObject, string method, params CodeExpression[] parameters)
        {
            return new CodeMethodInvokeExpression(targetObject, method, parameters);
        }

        public static CodeMethodInvokeExpression CallStatic(Type type, string methodName)
        {
            return CallStatic(new CodeTypeReferenceExpression(type), methodName);
        }
        public static CodeMethodInvokeExpression CallStatic(CodeTypeReferenceExpression type, string methodName)
        {
            return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(type, methodName));
        }


        /// <summary>
        /// Calls a static method, for example .CallStatic(typeof(Console), "WriteLine")
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static CodeMethodInvokeExpression CallStatic(Type type, string methodName, params CodeExpression[] parameters)
        {
            return CallStatic(new CodeTypeReferenceExpression(type), methodName, parameters);   
        }

        public static CodeMethodInvokeExpression CallStatic(CodeTypeReferenceExpression type, string methodName, params CodeExpression[] parameters)
        {
            return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(type, methodName), parameters);
        }

        /// <summary>
        /// Returnes the reference to a index of an array.
        /// </summary>
        /// <param name="targetObject">The array.</param>
        /// <param name="indices">The indeces in the array.</param>
        /// <returns></returns>
        public static CodeArrayIndexerExpression ArrayIndex(CodeExpression targetObject, params CodeExpression[] indices)
        {
            return new CodeArrayIndexerExpression(targetObject, indices);
        } 

        /////////////////////////////////////////////////////////////////
        //                      Conditions                             //
        /////////////////////////////////////////////////////////////////

        /// <summary>
        /// A binary operator.
        /// </summary>
        public static CodeBinaryOperatorExpression Op(CodeExpression left, CodeBinaryOperatorType @operator, CodeExpression right)
        {
            return new CodeBinaryOperatorExpression(left, @operator, right);
        }

        ///// <summary>
        ///// Checks if the values are equal
        ///// </summary>
        //public static CodeBinaryOperatorExpression OpEqual(CodeExpression left, CodeExpression right)
        //{
        //    return Op(left, CodeBinaryOperatorType.ValueEquality, right);
        //}

        //public static CodeBinaryOperatorExpression OpNotEqual(CodeExpression left, CodeExpression right)
        //{
        //    return Op(left, CodeBinaryOperatorType.BooleanAndValueEquality, right);
        //}

        ///// <summary>
        ///// Notifies the resulut of the varDeclarationExpr.
        ///// </summary>
        ///// <param name="expr"></param>
        ///// <returns></returns>
        //public static CodeBinaryOperatorType Not(CodeExpression expr)
        //{ 
        //    return new C
        //}

        /////////////////////////////////////////////////////////////////
        //                      Variables                              //
        /////////////////////////////////////////////////////////////////

        /// <summary>
        /// Gets the reference to a variable.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static CodeVariableReferenceExpression Var(string variable)
        {
            return new CodeVariableReferenceExpression(variable);
        }

        /// <summary>
        /// Gets the reference to a argument.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static CodeArgumentReferenceExpression Arg(string parameter)
        {
            return new CodeArgumentReferenceExpression(parameter);
        }

        /// <summary>
        /// Gets the property value of an object
        /// </summary>
        /// <param name="targetObject">The </param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static CodeExpression Prop(CodeExpression targetObject, string property)
        {
            return new CodePropertyReferenceExpression(targetObject, property);
        }

        /////////////////////////////////////////////////////////////////
        //                       Keywords                              //
        /////////////////////////////////////////////////////////////////

        public static CodeThisReferenceExpression This
        {
            get
            {
                return new CodeThisReferenceExpression();
            }
        }

        public static CodeTypeOfExpression TypeOf(string type)
        {
            return new CodeTypeOfExpression(type);
        }

        public static CodeTypeOfExpression TypeOf(Type type)
        {
            return new CodeTypeOfExpression(type);
        }

        public static CodeTypeOfExpression TypeOf(CodeTypeReference type)
        {
            return new CodeTypeOfExpression(type);
        }

        /// <summary>
        /// The value keyword for properties
        /// </summary>
        /// <returns></returns>
        public static CodePropertySetValueReferenceExpression Value()
        {
            return new CodePropertySetValueReferenceExpression();
        }

        /////////////////////////////////////////////////////////////////
        //                      Attributes                             //
        /////////////////////////////////////////////////////////////////

        /// <summary>
        /// Creates a Attribute argument.
        /// </summary>
        /// <param name="value">The value of the attribute argment.</param>
        /// <returns></returns>
        public static CodeAttributeArgument AttributeArg(CodeExpression value)
        {
            return new CodeAttributeArgument(value);
        }

        /// <summary>
        /// Creates a Attribute argument.
        /// </summary>
        /// <param name="name">The name of the attribute argument.</param>
        /// <param name="value">The value of the attribute argment.</param>
        /// <returns></returns>
        public static CodeAttributeArgument AttributeArg(string name, CodeExpression value)
        {
            return new CodeAttributeArgument(name, value);
        }

        /////////////////////////////////////////////////////////////////
        //                         Sonst                               //
        /////////////////////////////////////////////////////////////////

        /// <summary>
        /// A primitive parent like int, string etc.
        /// </summary>
        public static CodePrimitiveExpression Primitive(object primitive)
        {
            return new CodePrimitiveExpression(primitive);            
        }

        public static CodeCastExpression Cast(Type targetType, CodeExpression expression)
        {
            return new CodeCastExpression(targetType, expression);
        }

        public static CodeCastExpression Cast(CodeTypeReference targetType, CodeExpression expression)
        {
            return new CodeCastExpression(targetType, expression);
        }
    }
}
