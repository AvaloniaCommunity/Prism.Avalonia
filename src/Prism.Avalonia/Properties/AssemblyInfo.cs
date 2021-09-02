using System;
using System.Runtime.InteropServices;
using Avalonia.Metadata;

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

// -----  Legacy -----
[assembly: XmlnsDefinition("http://www.codeplex.com/prism", "Prism.Regions")]
[assembly: XmlnsDefinition("http://www.codeplex.com/prism", "Prism.Regions.Behaviors")]
[assembly: XmlnsDefinition("http://www.codeplex.com/prism", "Prism.Mvvm")]
[assembly: XmlnsDefinition("http://www.codeplex.com/prism", "Prism.Interactivity")]
// -----  Legacy -----

[assembly: XmlnsDefinition("http://prismlibrary.com/", "Prism.Regions")]
[assembly: XmlnsDefinition("http://prismlibrary.com/", "Prism.Regions.Behaviors")]
[assembly: XmlnsDefinition("http://prismlibrary.com/", "Prism.Mvvm")]
[assembly: XmlnsDefinition("http://prismlibrary.com/", "Prism.Interactivity")]
[assembly: XmlnsDefinition("http://prismlibrary.com/", "Prism.Services.Dialogs")]
[assembly: XmlnsDefinition("http://prismlibrary.com/", "Prism.Ioc")]
