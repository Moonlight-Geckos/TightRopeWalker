using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    private void Awake()
    {
        SetupRopePositions();
    }

    private void SetupRopePositions()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        GameObject ropeStart = GameObject.FindGameObjectWithTag("RopeStart");
        GameObject ropeEnd = GameObject.FindGameObjectWithTag("RopeEnd");
        lineRenderer.SetPosition(0, ropeStart.transform.position);
        lineRenderer.SetPosition(1, ropeEnd.transform.position);
    }

    private void OnDrawGizmos()
    {
        SetupRopePositions();
    }
}
