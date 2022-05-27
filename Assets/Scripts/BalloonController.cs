using UnityEngine;

public class BalloonController : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ParticlePool popParticlePool;

    private void OnTriggerEnter(Collider other)
    {
        Interact();
        popParticlePool?.createItem(transform);
        Destroy(gameObject);
    }

    public void Interact()
    {
        EventsPool.BalloonPopped.Invoke();
    }
}