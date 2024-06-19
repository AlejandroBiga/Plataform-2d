
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{

    public static void SavePlayer(PlayerMove player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.carpincho";
        Debug.Log("Saving player data to: " + path);
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Player data saved: Position - " + data.position[0] + ", " + data.position[1] + ", " + data.position[2] + " in Scene - " + data.sceneName);
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.carpincho";
        Debug.Log("Loading player data from: " + path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            Debug.Log("Player data loaded: Position - " + data.position[0] + ", " + data.position[1] + ", " + data.position[2] + " in Scene - " + data.sceneName);
            return data;
        }
        else
        {
            Debug.LogError("Save not found in " + path);
            return null;
        }
    }

    public static void LoadSceneAndPlayer()
    {
        PlayerData data = LoadPlayer();
        if (data != null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(data.sceneName);
        }
        else
        {
            Debug.LogError("No save data found.");
        }
    }

}
