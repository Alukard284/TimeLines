using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActivationTimeLine : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;

    public void ActivationAngarDoor()
    {
        playableDirector.Play();
    }
}
