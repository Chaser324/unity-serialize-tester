using UnityEngine;
using System.Collections;
using FullSerializer;

namespace SuperSystems.UnityTools.Test
{

public class SampleClass
{
    public enum SampleEnum
    {
        Value1,
        Value2,
        Value3
    }

    public int autoProperty { get; set; }

    public int integerValue = 123456;
    public float floatValue = 123.456f;
    public string stringValue = "This is a string.";
    public SampleEnum enumValue = SampleEnum.Value2;
    public int[] intArray = new int[] { 1, 2, 3 };
    public AnotherClass anotherClassInstance = new AnotherClass(654321);

    [fsProperty]
    private string privateButSerialized = "I'm a private field with serializable attribute.";

    [fsIgnore] // Equivalent to [System.NonSerialized] attribute.
    public string publicButNotSerialized = "You won't see this in serialized output.";

    private string nonSerializedPrivateField = "You won't see this in serialized output.";
}

// Note that this class won't be the one directly serialized
// because its name doesn't match the file name.
public class AnotherClass
{
    public string testString = "This is from an instance of AnotherClass!";
    public int testInteger = 789;
    public int valuePassedInFromConstructor;

    public AnotherClass(int someValue)
    {
        this.valuePassedInFromConstructor = someValue;
    }
}

}