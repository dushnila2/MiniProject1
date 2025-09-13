using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed*Time.deltaTime);
    }
}
