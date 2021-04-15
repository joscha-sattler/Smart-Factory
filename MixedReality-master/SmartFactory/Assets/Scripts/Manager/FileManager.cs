using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class FileManager
{
    public static string toLoad;
    private static string fileName;

    public static void SaveFile(string fileName, SaveData saveData)
    {
        string destination = Application.dataPath + "/SaveFiles/" + fileName;
        FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenWrite(destination);
        }
        else
        {
            file = File.Create(destination);
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, saveData);
        file.Close();
    }

    public static SaveData LoadFile()
    {
        string destination = Application.dataPath + "/SaveFiles/" + toLoad;
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return null;
        }

        BinaryFormatter bf = new BinaryFormatter();
        SaveData saveData = (SaveData)bf.Deserialize(file);
        file.Close();
        
        fileName = Path.GetFileName(file.Name);
        return saveData;
    }

    public static string GetFileName()
    {
        return fileName;
    }
}
