using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class nextLVL : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            DeleteSaveData();

            // Cambia de escena
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
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

    //evento
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            PlayerMove player = playerObject.GetComponent<PlayerMove>();
            if (player != null)
            {
                // Guarda los datos del jugador en la nueva escena
                SaveSystem.SavePlayer(player);
            }
        }

        // Desuscribir el método para evitar múltiples llamadas
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
