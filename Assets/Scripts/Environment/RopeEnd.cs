using UnityEngine;

public class RopeEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventsPool.PlayerWonEvent.Invoke();
        Destroy(this);
    }
}
