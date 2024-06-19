
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{

    public static void SavePlayer (PlayerMove player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.carpincho";
        FileStream stream = new FileStream(path, FileMode.Create);

        //set de la clase 
        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.carpincho";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            //pasa la infor desencriptada directamente a la clase de PlayerData;
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;

        }
        else
        {
            Debug.LogError("Save not found in" + path);
            return null;
        }
    }
    
}
