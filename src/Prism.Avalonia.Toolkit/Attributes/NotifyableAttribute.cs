using System;

namespace Prism.Avalonia.Toolkit
{
    /// <summary>Attributes that allows property to work with INotifyPropertyChanged.</summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class NotifyableAttribute : Attribute
    {
    }
}
