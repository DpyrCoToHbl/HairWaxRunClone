using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveLevel(LevelCounter levelCounter)
    {
        BinaryFormatter formater = new BinaryFormatter();
        FileStream stream = new FileStream(GetSavePath(), FileMode.Create);
        LevelData levelData = new LevelData(levelCounter);
        formater.Serialize(stream, levelData);
        stream.Close();
    }

    public static LevelData LoadLevel()
    {
        if (File.Exists(GetSavePath()))
        {
            BinaryFormatter formater = new BinaryFormatter();
            FileStream stream = new FileStream(GetSavePath(), FileMode.Open);
            LevelData levelData = formater.Deserialize(stream) as LevelData;
            stream.Close();

            return levelData;
        }
        else
        {
            return null;
        }
    }

    private static string GetSavePath()
    {
        return $"{Application.persistentDataPath}/LevelSave.lvls";
    }

}
