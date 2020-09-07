# Issue report

I am seeing build failures when using a nuget package that contains shared source code in a WPF project.

This failure occurs when targetting .NET Core 3.1 or .NET Framework 4.8 (I haven't tried any other targets). I have included a very simplified example that demonstrates the problem.

## Code
The `Shared` folder contains a nuget package called `MySharedEnums`, and the source to build it. The nuget package just contains a C# source file (`SharedEnums.cs`) and a .props file to ensure that the C# file is compiled as part of the consuming project.

The `SharedSourceFail` is a WPF project that contains a Window and a Class. The Class uses the enum defined in `SharedEnums.cs`. This project uses the `MySharedEnums` package - the nuget.config uses the `Shared` folder as a package source.

## The problem
As it stands, the project does not build. 
```
Program.cs(4,11): error CS0246: The type or namespace name 'Shared' could not be found (are you missing a using directive or an assembly reference?) [C:\Users\olimau\source\experiments\SharedSourceFail\SharedSourceFail\SharedSourceFail_b4vktjvh_wpftmp.csproj]
```

The Window contains a namespace declaration `xmlns:local="clr-namespace:MyNamespace"`. If this namespace declaration is removed, then the project builds.

Alternatively, if the assembly is specified - `xmlns:local="clr-namespace:MyNamespace;assembly=SharedSourceFail"`, the project also builds. It should not be necessary to specify the current assembly.

So, the problem appears to be that if a `clr-namespace` is used without specifying the assembly, then shared source files from nuget packages are ignored!

## Background
I am upgrading a large solution, moving shared code into nuget packages, and updating the projects to the new SDK style format.
We used Gemalto Sentinel dongles for licencing protection. The code to enable this needs to be compiled into each project that uses it - it can't be called from a separate DLL so I have been including the source files in a nuget package. This worked for most projects, but failed for some. It turns out that all the projects that failed to build contain WPF components.