using UnityEngine;

public class BalloonController : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ParticlePool popParticlePool;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NoBirds")
        {
            return;
        }
        Interact();
        popParticlePool?.createItem(transform);
        Destroy(gameObject);
    }

    public void Interact()
    {
        EventsPool.BalloonPopped.Invoke();
    }
}