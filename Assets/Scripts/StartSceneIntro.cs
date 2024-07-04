using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneIntro : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Start");
            Debug.Log(other.name);
        }
        else
        {
            animator.SetTrigger("StandUp");
        }
    }
}
