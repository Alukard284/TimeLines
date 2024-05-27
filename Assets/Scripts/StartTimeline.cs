using UnityEngine;
using UnityEngine.Playables;

public class StartTimeline : MonoBehaviour
{
    private PlayableDirector playableDirector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        InputPlayer();
    }
    private void InputPlayer()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
           playableDirector.Play();
        }
    }
}
