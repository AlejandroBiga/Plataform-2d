using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadButtom : MonoBehaviour
{
    public void LoadGame()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        SaveSystem.LoadSceneAndPlayer(gameManager.cinematicsSeen);
    }
}
