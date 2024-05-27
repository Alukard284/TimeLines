using UnityEngine;

public class ChangeScale : MonoBehaviour
{
    private Transform cube;
    private float scale = 3;
    private Vector3 lacalScale;

    private void Start()
    {
       cube = GetComponent<Transform>();
    }
    public void ChangeScaleGameObject()
    {
        lacalScale = new Vector3(scale +1, scale, scale+3);
        cube.localScale = lacalScale;
        
    }
}
