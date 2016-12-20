using FullSerializer;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace SuperSystems.UnityTools
{

public class SerializeTester
{
    [MenuItem("Assets/Serialize")]
    private static void SerializeFile()
    {
        // Get type from class name.
        string className = Selection.activeObject.name;
        Type type = GetType(className);

        if (type == null)
        {
            Debug.LogError("Can't find class with name " + Selection.activeObject.name + ".");
            return;
        }

        // Create an instance of the class.
        var obj = Activator.CreateInstance(type);

        if (obj == null)
        {
            Debug.LogError("Could not instatiate class " + Selection.activeObject.name + ". It may lack a default constructor.");
            return;
        }

        // Serialize data.
        fsData data;
        fsSerializer serializer = new fsSerializer();
        serializer.TrySerialize(type, obj, out data);

        if (data == null)
        {
            Debug.LogError("Could not serialize class " + Selection.activeObject.name);
            return;
        }

        // Convert data to text format for output.
        byte[] dataBytes = Encoding.UTF8.GetBytes(fsJsonPrinter.PrettyJson(data));

        // Get the filepath and delete any pre-existing file.
        string filepath =
            AssetDatabase.GetAssetPath(Selection.activeObject);

        filepath = Path.GetDirectoryName(filepath) + Path.DirectorySeparatorChar +
            Path.GetFileNameWithoutExtension(filepath) + ".json";

        if (File.Exists(filepath))
            File.Delete(filepath);

        // Write serialized data to file.
        using (FileStream fs = File.OpenWrite(filepath))
        {
            fs.Write(dataBytes, 0, dataBytes.Length);
        }

        // Refresh AssetDatabase so the new file is visible in Unity Editor.
        AssetDatabase.Refresh();
    }

    [MenuItem("Assets/Serialize", true)]
    private static bool SerializeFileValidate()
    {
        Type selectedType = Selection.activeObject.GetType();
        return (selectedType == typeof(MonoScript));
    }

    private static Type GetType(string className)
    {
        // Get list of all app assemblies.
        Assembly[] assemblyList = AppDomain.CurrentDomain.GetAssemblies();

        // Search the assemblies until a type is found that matches the provided class name.
        Type type = null;
        foreach (Assembly assembly in assemblyList)
        {
            Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                if (types[i].Name == className)
                {
                    type = types[i];
                    break;
                }
            }

            if (type != null)
                break;
        }

        return type;
    }
}

}