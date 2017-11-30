using System;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace Prism.Regions
{
    /// <summary>
    /// Provides a hint from a view to a region on how to sort the view.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class ViewSortHintAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewSortHintAttribute"/> class.
        /// </summary>
        /// <param name="hint">The hint to use for sorting.</param>
        public ViewSortHintAttribute(string hint)
        {            
            this.Hint = hint;
        }

        /// <summary>
        /// Gets  the hint.
        /// </summary>
        /// <value>The hint to use for sorting.</value>
        public string Hint { get; private set; }
    }
}
