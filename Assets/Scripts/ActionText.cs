using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI actionText;
    [SerializeField] string actionString;
    [SerializeField] Image topBoardUi;
    [SerializeField] float fade_Speed = 1.0f;
    private Color fade_Color;
    private Coroutine startCoroutine;
    private Coroutine exitCoroutine;
    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        actionText.text = "";
        fade_Color = topBoardUi.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startCoroutine = StartCoroutine(DialogEnter());
        }
    }
    IEnumerator DialogEnter()
    {
        while (fade_Color.a < 1f && !active)
        {
            fade_Color.a += fade_Speed * Time.deltaTime;
            topBoardUi.color = fade_Color;
            if (topBoardUi.color.a > 0.95f)
            {
                actionText.text = actionString;
            }
            yield return null;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            exitCoroutine = StartCoroutine(DialogExit());
        }
    }
    IEnumerator DialogExit()
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
                exitCoroutine = StartCoroutine(DialogExit());
                active = true;
            }
        }
    }
}
