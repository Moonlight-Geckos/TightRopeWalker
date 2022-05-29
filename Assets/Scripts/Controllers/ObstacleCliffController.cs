using UnityEngine;

public class ObstacleCliffController : MonoBehaviour, IInteractable
{

    private void OnTriggerEnter(Collider other)
    {
        Interact();
    }
    public void Interact()
    {
        EventsPool.PlayerFallenEvent.Invoke(transform.position.x > 0);
    }
}
