# Prism.Avalonia
Prism (https://github.com/PrismLibrary/Prism) framework support for Avalonia UI

This library actually copies functionality of Prism for WPF implementation, which can be found here:
https://github.com/PrismLibrary/Prism/tree/master/Source/Wpf
  
Logic and approach for development your applications remained the same as it was for Prism.Wpf library. 

Also I should say, that because this port was made in few days in rush-mode without deep knowledge
of Avalonia internal mechanisms and because of differences in runtime platforms not everything was 
ported (list below), also of course you can confront with various bugs. 
I will use this port in my internalwork projects, so by developing applications I'll fix bugs 
or invalid behaviours which occur on my path, but issues and pull requests are welcome too!

Not ported/supported features and other problems:
- Prism.Avalonia not supports ability to bind Region to ItemsSource/Selector element
- ViewInjection for regions will not work
- DirectoryCatalog implementation of ModuleCatalog
- Not all tests are reproducible because of runtime or avalonia capabilities

| ModulesSample on Ubuntu 16.04 | ModulesSample on Windows 10 |
|---|---|
| <img width='300' src='https://i.imgur.com/DkwcIkR.png'></a> | <img width='300' src='https://i.imgur.com/IKI87pv.png'></a> |
