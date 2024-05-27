using UnityEngine;
[RequireComponent (typeof(Rigidbody))]
public class AddForce : MonoBehaviour
{
    private Rigidbody cubeBody;
    [SerializeField] float force;

    void Start()
    {
        cubeBody = GetComponent<Rigidbody> ();
    }

    public void AddFoece()
    {
        cubeBody.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
