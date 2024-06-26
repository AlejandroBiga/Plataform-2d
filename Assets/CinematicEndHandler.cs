using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicEndHandler : MonoBehaviour
{
    private PlayableDirector director;
    private CinematicManager cinematicManager;

    void Start()
    {
        director = GetComponent<PlayableDirector>();
        cinematicManager = GetComponent<CinematicManager>();

        if (director != null)
        {
            director.stopped += OnPlayableDirectorStopped;
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector && cinematicManager != null)
        {
            cinematicManager.MarkCinematicAsSeen();
        }
    }
}
