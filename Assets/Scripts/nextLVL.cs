using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class nextLVL : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DeleteSaveData();
        if (collision.gameObject.CompareTag("Player"))
        {
            // next escene in the build
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
}
