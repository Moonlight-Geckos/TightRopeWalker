using System.Collections;
using UnityEngine;
public class WalkerController : MonoBehaviour
{
    #region Serialized

    [SerializeField]
    private float fallSpeed = 3f;

    [SerializeField]
    private float fallDistance = 30f;

    #endregion

    private Animator animator;
    private StickController stickControl;
    private Transform bodyTarget;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        stickControl = FindObjectOfType<StickController>();
        bodyTarget = GameObject.FindGameObjectWithTag("BodyTarget").transform;
        animator.speed = 0;
        EventsPool.PlayerFallenEvent.AddListener(Fall);
        EventsPool.PlayerWonEvent.AddListener(Dance);
        EventsPool.GameStartedEvent.AddListener(() => animator.speed = 1);
    }
    private void Fall(bool right)
    {
        animator.SetTrigger("Fall");
        IEnumerator animate()
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>(); ;
            yield return new WaitForEndOfFrame();
            animator.speed = 1;
            stickControl.Fall();
            rb.useGravity = true;
            Vector3 newForce = new Vector3(4 * (right ? 1 : -1), -18, - 2f);
            rb.AddForce(newForce, ForceMode.Impulse);
        }
        StartCoroutine(animate());
    }
    private void Dance()
    {
        IEnumerator animate()
        {
            yield return new WaitForEndOfFrame();
            bodyTarget.transform.localPosition = Vector3.zero;
            animator.SetTrigger("Win");
            animator.speed = 1;
            stickControl.Win();
        }
        StartCoroutine(animate());
    }
    public void SetAnimatorSpeed(float speed)
    {
        animator.speed = speed;
    }
}
