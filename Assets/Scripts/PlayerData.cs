using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public float[] position;
    public string sceneName;
    public Dictionary<string, bool> cinematicsSeen;

    public PlayerData(PlayerMove player, Dictionary<string, bool> cinematicsSeen)
    {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        this.cinematicsSeen = new Dictionary<string, bool>(cinematicsSeen);
    }
}
