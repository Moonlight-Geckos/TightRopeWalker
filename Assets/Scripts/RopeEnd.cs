using UnityEngine;

public class RopeEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.PlayerWonEvent.Invoke();
        Destroy(this);
    }
}
