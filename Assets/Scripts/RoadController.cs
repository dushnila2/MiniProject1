using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    [SerializeField]
    List<RoadSegment> segments;
    int lastSegmentIndex = 0;
    private void Start()
    {
        for (int i = 0; i < segments.Count; i++)
        {
            segments[i].FinishedSegment += OnSegmentFinished;
        }
    }
    private void OnSegmentFinished()
    {
        segments[lastSegmentIndex].transform.Translate(Vector3.forward * 60);
        lastSegmentIndex++;
        if (lastSegmentIndex >= segments.Count)
        {
            lastSegmentIndex = 0;
        }
    }
}


