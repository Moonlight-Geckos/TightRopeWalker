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
    private StickControl stickControl;
    private Transform bodyTarget;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        stickControl = FindObjectOfType<StickControl>();
        bodyTarget = GameObject.FindGameObjectWithTag("BodyTarget").transform;
        GameManager.PlayerFallenEvent.AddListener(Fall);
        GameManager.PlayerWonEvent.AddListener(Dance);
    }

    private void Fall(bool right)
    {
        animator.SetTrigger("Fall");
        IEnumerator animate()
        {
            yield return new WaitForEndOfFrame();
            animator.speed = 1;
            stickControl.Fall();
            Vector3 position = new Vector3(fallDistance/2 * (right ? 1 : -1), transform.position.y - fallDistance, transform.position.z);
            while (transform.position.y > position.y)
            {
                transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * fallSpeed);
                yield return new WaitForEndOfFrame();
            }
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
