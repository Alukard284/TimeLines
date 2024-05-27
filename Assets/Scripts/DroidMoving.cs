using UnityEngine;

public class DroidMoving : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        MoveAround();
    }
    public void MoveAround()
    {
       transform.RotateAround(target.position, transform.up, speed * Time.deltaTime);
    }
}
