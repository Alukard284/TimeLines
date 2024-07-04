using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    private bool active;

    private void Start()
    {
        playerController.enabled = false;
    }
    public void PlayerControllerEnable()
    {
        active = !active;
        playerController.enabled = active;
    }
}
