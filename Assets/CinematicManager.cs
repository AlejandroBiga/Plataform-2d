using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicManager : MonoBehaviour
{
    public string cinematicName;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager.cinematicsSeen.ContainsKey(cinematicName) && gameManager.cinematicsSeen[cinematicName])
        {
           
            gameObject.SetActive(false);
        }
    }

    public void MarkCinematicAsSeen()
    {
        if (gameManager != null)
        {
            gameManager.cinematicsSeen[cinematicName] = true;
            SaveSystem.SavePlayer(FindObjectOfType<PlayerMove>(), gameManager.cinematicsSeen);
        }
    }
}
