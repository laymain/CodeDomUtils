using System;
using System.Collections.Generic;
using System.Text;

namespace FluentCodeDom
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TWrappedType">The class for which this is a wrapper</typeparam>
    /// <typeparam name="TParent">The parent fluent parent</typeparam>
    public abstract class FluentTemplate<TWrappedType, TParent> : FluentTemplate<TWrappedType>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumType">The wrapped parent.</param>
        /// <param name="wrappedType"></param>
        /// <param name="parent">The parent of the fluent template, this can be null</param>
        public FluentTemplate(TWrappedType wrappedType, TParent parent)
            : base(wrappedType)
        {
            _parent = parent;
        }

        protected internal TParent _parent;

        protected virtual TParent EndInternal
        {
            get
            {
                CheckParentExist();

                return _parent;
            }
        }

        /// <summary>
        /// Throws exception if the parent does not exist
        /// </summary>
        private void CheckParentExist()
        {
            if (_parent == null)
                throw new InvalidOperationException(string.Format("Cannot go up to parent because no parent is defined. Object Type: \"{0}\"", this.GetType().FullName));
        }
    }

    public abstract class FluentWrapperlessTemplate<TParent>
    {
        public FluentWrapperlessTemplate(TParent parent)
        {
            _parent = parent;
        }

        protected internal TParent _parent;

        protected virtual TParent EndInternal
        {
            get
            {
                CheckParentExist();

                return _parent;
            }
        }

        /// <summary>
        /// Throws exception if the parent does not exist
        /// </summary>
        protected void CheckParentExist()
        {
            if (_parent == null)
                throw FluentException.NewFormat(this, "Cannot go up to parent because no parent is defined. Object Type: \"{0}\"", this.GetType().FullName);
        }
    }

    /// <summary>
    /// A template class for Fluent classes
    /// </summary>
    /// <typeparam name="TWrappedType">The class for which this is a wrapper</typeparam>
    public abstract class FluentTemplate<TWrappedType>
    {
        public FluentTemplate(TWrappedType wrappedType)
        {
            _wrappedType = wrappedType;
        }

        protected internal TWrappedType _wrappedType;

        // This is a method because when its a property its value MUST be saved into a variable.
        /// <summary>
        /// Ends the fluent configuration and returns the parent arround the wrapper
        /// </summary>
        public virtual TWrappedType EndFluent()
        {
                return _wrappedType;
        }
    }
}
