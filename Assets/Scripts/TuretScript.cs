using UnityEngine;
public class TuretScript: MonoBehaviour 
{
    [SerializeField]
    private float _rotationSpeed = 0.5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                Debug.Log("ïonaë");
                LookAtTargetOnly(hit.point);
            }
            else
            {
                Debug.Log("He nonaë");
            }
        }

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                Debug.Log("ïonaë");
                Vector3 lookDirection = new Vector3(hit.point.x, 0f, hit.point.z);
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                Quaternion smoothRotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                transform.rotation = smoothRotation;
            }
            else
            {
                Debug.Log("He nonan");
            }
        }
    }

    void LookAtTargetOnly(Vector3 targetPosition)
    {
        Vector3 lookAtPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        transform.LookAt(lookAtPosition);
    }
}