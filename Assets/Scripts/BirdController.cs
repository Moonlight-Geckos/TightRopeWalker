using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class BirdController : MonoBehaviour, IInteractable
{
    #region Serialized

    [SerializeField]
    private float moveSpeed = 4;

    private Rigidbody rb;

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        Interact();
        GetComponent<IDisposable>()?.Dispose();
    }

    public void Interact()
    {
        EventsPool.PlayerFallenEvent.Invoke(transform.position.x > 0);
    }

    public void Initialize(Vector3 position)
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        Vector3 originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
        IEnumerator scaleUp()
        {
            while(Vector3.Distance(transform.localScale, originalScale) > 0.1f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
        StartCoroutine(scaleUp());
        rb.useGravity = false;
        transform.position = position;
        transform.LookAt(new Vector3(position.x, position.y, -1));
        rb.velocity = new Vector3(0, 0, -moveSpeed);
    }

    private void OnBecameInvisible()
    {
        GetComponent<IDisposable>()?.Dispose();
    }
}
