
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem 
{

    public static void SavePlayer(PlayerMove player, Dictionary<string, bool> cinematicsSeen)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.carpincho";
        Debug.Log("Saving player data to: " + path);
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player, cinematicsSeen);
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

    public static void LoadSceneAndPlayer(Dictionary<string, bool> cinematicsSeen)
    {
        PlayerData data = LoadPlayer();
        if (data != null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(data.sceneName);
            cinematicsSeen.Clear();
            foreach (var cinematic in data.cinematicsSeen)
            {
                cinematicsSeen.Add(cinematic.Key, cinematic.Value);
            }
        }
        else
        {
            Debug.LogError("No save data found.");
        }
    }

}
