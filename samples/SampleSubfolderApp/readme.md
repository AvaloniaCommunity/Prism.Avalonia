# App Patcher Sample

> _WORK-IN-PROGRESS_
> **NOTE:** Currently, this requires the use of the .NET Framework and is not compatabile with .NET (Core) applications. Solutions will still need to be looked into as .NET SDK [Issue #10366](https://github.com/dotnet/sdk/issues/10366) is still open.

This example demonstrates how to place your host application in the root folder and the supporting libraries (`.DLL`) files in the `/bin/` folder.

## Credit

This approach is inspired by the [dnSpyEx](https://github.com/dnSpyEx/dnSpy) project.

Other tools exist such as, [NetBeauty2](https://github.com/nulastudio/NetBeauty2) which aims to place dependencies in a subdirectory. This tool allows for `.csproj` integration and is based on the Go language.
