# Serialize Tester
> A simple Unity3D utility for testing class serialization.

Quickly and easily produce serialized JSON output from any class. This is a great tool for verifying that all necessary fields are being serialized properly. I also really appreciate it as a tool for developing save/settings files that can be read and deserialized into in-game object instances.

![](https://raw.githubusercontent.com/Chaser324/unity-serialize-tester/master/sampleImage.png)

# Installation

1. This tool requires FullSerializer, a great and highly portable serialization library. You can grab it from its [GitHub repository](https://github.com/jacobdufault/fullserializer). Just import the `Assets/FullSerializer` directory into your Unity project's `Assets` directory. (If you like FullSerializer, consider purchasing [FullInspector](http://jacobdufault.github.io/fullinspector/).)
2. Import the `Assets/unity-serialize-tester` directory from this repository into your Unity project's `Assets` directory.

# Usage

From the Unity Editor, right-click on any C# script file and click `Serialize`. The JSON output will appear in the same directory. It's just that simple.

There are a few caveats to what classes can be serialized using this tool:
* The class name must match the file name, for example `public class SampleClass` in `SampleClass.cs`.
* The class must have a default constructor. Any MonoBehaviour will meet this requirement.
* Generic classes can't be directly serialized, for example `public class GenericClass<T>`, but instances of the generic class in other classes can be serialized. (Your mileage may vary with generics in general - anywhere the type isn't directly discernable may have issues.)

# License

Copyright (c) 2016 Super Systems Softworks LLC

All code in this repository ([unity-serialize-tester](https://github.com/Chaser324/unity-serialize-tester)) is made freely available under the MIT license. Please provide attribution with above copyright notice. See license file for details.