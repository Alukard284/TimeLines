using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorTigger : MonoBehaviour
{
    [SerializeField] Animator LeftDoorAnimator;
    [SerializeField] Animator RightDoorAnimator;
    [SerializeField] TextMeshProUGUI actionText;
    [SerializeField] Image topBoardUi;
    [SerializeField] float fade_Speed = 1.0f;
    private Color fade_Color;
    private Coroutine startCoroutine;
    private Coroutine exitCoroutine;
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        actionText.text = null;
        fade_Color = topBoardUi.color;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startCoroutine = StartCoroutine(DoorDialogEnter());
        }
    }
    
    IEnumerator DoorDialogEnter()
    {
        while (fade_Color.a < 1f && !active)
        {
            fade_Color.a += fade_Speed * Time.deltaTime;
            topBoardUi.color = fade_Color;
            if (topBoardUi.color.a > 0.95f)
            {
                actionText.text = "Для активации двери нажмите Е";
            }
            yield return null;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            exitCoroutine = StartCoroutine(DoorDialogExit());
        }
    }

    IEnumerator DoorDialogExit()
    {
        while (fade_Color.a >= 0f)
        {
            fade_Color.a -= fade_Speed * Time.deltaTime;
            topBoardUi.color = fade_Color;
            if (topBoardUi.color.a < 0.95f)
            {
                actionText.text = "";
            }
            yield return null;

        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                LeftDoorAnimator.SetBool("Open", true);
                RightDoorAnimator.SetBool("Open", true);
                exitCoroutine = StartCoroutine(DoorDialogExit());
                active = true;
                
            }
        }
    }

}
