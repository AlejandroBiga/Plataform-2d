using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class activatescript : MonoBehaviour
{
    [SerializeField] private PlayableDirector PlayableDirector;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayableDirector.Play();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
