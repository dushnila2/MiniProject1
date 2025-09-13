using System;
using UnityEngine;

public class RoadSegment : MonoBehaviour
{
    public event Action FinishedSegment;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            FinishedSegment?.Invoke();
        }
    }
}
