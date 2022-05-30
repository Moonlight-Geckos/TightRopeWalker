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
        if (other.gameObject.tag == "NoBirds")
        {
            FlyOutsideScreen(other.transform.position);
        }
        else
        {
            Interact();
            GetComponent<IDisposable>()?.Dispose();
        }
    }

    public void Interact()
    {
        if(GameManager.GameStarted)
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
        StopAllCoroutines();
        GetComponent<IDisposable>()?.Dispose();
    }

    private void FlyOutsideScreen(Vector3 pos)
    {
        IEnumerator flyoutside()
        {
            float duration = 3;
            Vector3 originalPos = transform.position;
            Vector3 newPos;
            newPos = originalPos;
            newPos += transform.forward * 30f;
            int sign = (transform.position.y - pos.y > 0 ? 1 : -1);
            newPos += transform.up * sign * 30;
            rb.velocity = Vector3.zero;
            while (duration > 0)
            {
                transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
                transform.localEulerAngles = transform.localEulerAngles + new Vector3(2 * sign * -1, 0, 0);
                yield return new WaitForEndOfFrame();
                duration -= Time.deltaTime;
            }
            GetComponent<IDisposable>()?.Dispose();
        }
        StartCoroutine(flyoutside());
    }
}
