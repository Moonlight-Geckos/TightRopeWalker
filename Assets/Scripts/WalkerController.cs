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


    private void Awake()
    {
        animator = GetComponent<Animator>();
        GameManager.PlayerFallenEvent.AddListener(Fall);
        stickControl = FindObjectOfType<StickControl>();
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

    public void SetAnimatorSpeed(float speed)
    {
        animator.speed = speed;
    }
}
