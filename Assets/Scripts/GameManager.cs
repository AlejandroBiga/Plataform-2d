using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Para que el GameManager persista entre escenas
        }
        else
        {
            Destroy(gameObject); // Si ya hay una instancia, destruye esta para mantener solo una
            return;
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }



    public void Play()
    {
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


    public void LoadGame()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SaveSystem.LoadSceneAndPlayer();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(LoadPlayerAfterSceneLoad());
    }

    private IEnumerator LoadPlayerAfterSceneLoad()
    {
        yield return null; // Esperar un frame para asegurarse de que todo se haya cargado

        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Debug.Log("Player object found, setting position.");
                Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
                player.transform.position = position;
                Debug.Log("Player position set to: " + position);
            }
            else
            {
                Debug.LogError("Player object not found.");
            }
        }
        else
        {
            Debug.LogError("No save data found.");
        }
    }
}