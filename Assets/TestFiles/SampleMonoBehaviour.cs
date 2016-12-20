using UnityEngine;

namespace SuperSystems.UnityTools.Test
{

public class SampleMonoBehaviour : MonoBehaviour
{
    public int integerValue = 123456;
    public float floatValue = 123.456f;
    public string stringValue = "This is a string.";
    public SampleClass sampleClass = new SampleClass();

    void Start()
    {
        // Note that no methods are called prior to serialiazation.
        integerValue = 789;
    }
}

}