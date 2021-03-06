﻿using FullSerializer;
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
    private enum Serializer
    {
        FullSerializer,
        JsonUtility
    }

    [MenuItem("Assets/Serialize/FullSerializer")]
    private static void SerializeFileFS()
    {
        SerializeFile(Serializer.FullSerializer);
    }

    [MenuItem("Assets/Serialize/JsonUtility")]
    private static void SerializeFileUnity()
    {
        SerializeFile(Serializer.JsonUtility);
    }

    [MenuItem("Assets/Serialize/FullSerializer", true)]
    [MenuItem("Assets/Serialize/JsonUtility", true)]
    private static bool SerializeFileValidate()
    {
        return (Selection.activeObject != null && Selection.activeObject.GetType() == typeof(MonoScript));
    }

    private static void SerializeFile(Serializer serializer)
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
        string dataStr = null;
        switch (serializer)
        {
            case Serializer.FullSerializer:
                fsData data;
                fsSerializer fs = new fsSerializer();
                fs.TrySerialize(type, obj, out data);

                if (data == null)
                {
                    Debug.LogError("Could not serialize class " + Selection.activeObject.name);
                    return;
                }

                dataStr = fsJsonPrinter.PrettyJson(data);

                break;
            case Serializer.JsonUtility:
                dataStr = JsonUtility.ToJson(obj, true);
                break;
        }

        // Convert data to text format for output.
        byte[] dataBytes = Encoding.UTF8.GetBytes(dataStr);

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