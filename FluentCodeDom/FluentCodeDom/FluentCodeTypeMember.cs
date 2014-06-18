using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace FluentCodeDom
{
    /// <summary>
    /// Base class for items which can have attributes
    /// </summary>
    /// <typeparam name="TThis">This parent.</typeparam>
    /// <typeparam name="TWrappedType">The CodeDOM Type which this is a mapper for</typeparam>
    /// <typeparam name="TParent"></typeparam>
    public abstract class FluentCodeTypeMember<TThis, TWrappedType, TParent> : FluentTemplate<TWrappedType, TParent>
        where TThis : FluentCodeTypeMember<TThis, TWrappedType, TParent>
        where TWrappedType : CodeTypeMember
    {
        public FluentCodeTypeMember(TWrappedType wrappedType)
            : this(wrappedType, default(TParent))
        { 
        
        }

        public FluentCodeTypeMember(TWrappedType wrappedType, TParent parent)
            : base(wrappedType, parent)
        { 
        
        }

        /// <summary>
        /// Adds a non documentation comment.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public TThis Comment(string comment)
        {
            return Comment(new CodeCommentStatement(comment));
        }

        /// <summary>
        /// Comments the specified comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <param name="documentationComment"><c>true</c> if the comment is a documentation comment. otherwise <c>false</c>.</param>
        /// <returns></returns>
        public TThis Comment(string comment, bool documentationComment)
        {
            return Comment(new CodeCommentStatement(comment, documentationComment));
        }

        public TThis Comment(CodeCommentStatement commentStatment)
        {
            _wrappedType.Comments.Add(commentStatment);
            return ThisConverter(this);
        }

        public TThis CustomAttribute(Type attributeType, params CodeAttributeArgument[] arguments)
        {
            return CustomAttribute(new CodeTypeReference(attributeType), arguments);
        }

        public TThis CustomAttribute(CodeTypeReference attributeType, params CodeAttributeArgument[] arguments)
        {
            _wrappedType.CustomAttributes.Add(new CodeAttributeDeclaration(attributeType, arguments));
            return ThisConverter(this);
        }

        /// <summary>
        /// This method is a little bit of a hack, because normaly you coudn't convert 
        /// FluetnCodeDom Of TParent, TThis into TThis, this function converts it first
        /// into object and then back into TThis
        /// </summary>
        /// <param name="thisInstance"></param>
        /// <returns></returns>
        public static TThis ThisConverter(FluentCodeTypeMember<TThis, TWrappedType, TParent> thisInstance)
        {
            return (TThis)(object)thisInstance;
        }
    }

    
}
