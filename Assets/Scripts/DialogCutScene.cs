using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DialogCutScene : MonoBehaviour
{
    [SerializeField] PlayableDirector dialogPlayebleDirector;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Animator droidAnimator;
    [SerializeField] Image topBoardUi;
    [SerializeField] Image downBoardUi;
    [SerializeField] float fade_Speed = 1.0f;
    private Color fade_Color;
    [SerializeField] GameObject virtualCameras;
    private int textChanger;
    private Coroutine startCoroutine;
    private Coroutine exitCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        dialogPlayebleDirector.enabled = false;
        text.text = "";
        textChanger = 0;
        fade_Color = topBoardUi.color;
        fade_Color = downBoardUi.color;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogPlayebleDirector.enabled = true;  
            }
        }
    }
    public void TextUdate()
    {
        startCoroutine = StartCoroutine(DialogEnter());
        switch (textChanger)
        {
            case 0:
                text.text = "Доложить номер,статус и задачу";
                textChanger++;
                break;
            case 1:
                text.text = "Номер 284, статус вершитель, задача полет на орбитальтарную станцию";
                textChanger++;
                break;
            case 2:
                text.text = "Не возможно, недостаточно энергии, неоходимо перезапустить основной реактор вручную";
                textChanger++;
                break;
            case 3:
                text.text = "Как туда попасть?";
                textChanger++;
                break;
            case 4:
                text.text = "На север в четыре километра до башни климатконтроля, от неё 7 на восток до скалистой греды.";
                textChanger++;
                break;
            case 5:
                exitCoroutine = StartCoroutine(DialogExit());
                text.text = "";
                droidAnimator.SetBool("Active",true);
                virtualCameras.SetActive(false);
                break;
        }
    }
    IEnumerator DialogEnter()
    {
        while (fade_Color.a < 1f)
        {
            fade_Color.a += fade_Speed * Time.fixedDeltaTime;
            topBoardUi.color = fade_Color;
            downBoardUi.color = fade_Color;
            yield return null;
        }
    }
    IEnumerator DialogExit()
    {
        while (fade_Color.a >= 0f)
        {
            fade_Color.a -= fade_Speed * Time.fixedDeltaTime;
            topBoardUi.color = fade_Color;
            downBoardUi.color = fade_Color;
            yield return null;
        }
    }
}
