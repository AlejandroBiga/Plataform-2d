using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject continueButton;
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        CheckForSaveData();
    }



    public void Play()
    {
        DeleteSaveData();
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void DeleteSaveData()
    {
        string path = Application.persistentDataPath + "/player.carpincho";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Save data deleted.");
        }
        else
        {
            Debug.Log("No save data to delete.");
        }
    }

    private void CheckForSaveData()
    {
        string path = Application.persistentDataPath + "/player.carpincho";
        if (File.Exists(path))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

}