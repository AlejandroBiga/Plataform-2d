using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testSave : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMove player = collision.GetComponent<PlayerMove>();
            if (player != null)
            {
                if (GameManager.instance != null)
                {
                    SaveSystem.SavePlayer(player, GameManager.instance.cinematicsSeen);
                    GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    }


}
