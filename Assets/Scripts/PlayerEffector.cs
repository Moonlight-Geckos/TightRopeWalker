using UnityEngine;

public class PlayerEffector : MonoBehaviour
{
    #region Serialized

    [SerializeField]
    [Range(0f, 2f)]
    private float gravityModifier = 0.3f;

    [SerializeField]
    [Range(0f, 2f)]
    private float gravityModifierAdditionPerSecond = 0.05f;

    [SerializeField]
    [Range(2f, 20f)]
    private float normalWalkingSpeed = 5f;

    [SerializeField]
    [Range(1f, 15f)]
    private float unbalanceWalkModifier = 2f;

    #endregion


    #region Private

    private Transform bodyTarget;
    private Rigidbody rigidBody;
    private WalkerController characterControl;

    #endregion
    private void Awake()
    {
        bodyTarget = GameObject.FindGameObjectWithTag("BodyTarget").transform;
        rigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        characterControl = rigidBody.GetComponentInChildren<WalkerController>();
    }

    private void Update()
    {
        Vector3 newV = Vector3.zero;
        if (GameManager.GameStarted)
        {
            float offBalanceForce;
            int sign;
            if (bodyTarget.localPosition.x == 0)
                sign = Random.Range(-10, 10) % 2 == 0 ? 1 : -1;
            else
                sign = bodyTarget.localPosition.x > 0 ? 1 : -1;

            offBalanceForce = (sign * (GameManager.Instance.PlayerBounds + 2) - bodyTarget.localPosition.x) * Time.deltaTime * gravityModifier;
            bodyTarget.localPosition = new Vector3(bodyTarget.localPosition.x + offBalanceForce, 0, 0);

            if (bodyTarget.localPosition.x <= -GameManager.Instance.PlayerBounds || bodyTarget.localPosition.x >= GameManager.Instance.PlayerBounds)
            {
                EventsPool.PlayerFallenEvent.Invoke(bodyTarget.localPosition.x > 0);
            }
            newV = new Vector3(0, 0, normalWalkingSpeed - (Mathf.Abs(bodyTarget.localPosition.x) / GameManager.Instance.PlayerBounds * unbalanceWalkModifier));
            gravityModifier += gravityModifierAdditionPerSecond * Time.deltaTime;
            characterControl.SetAnimatorSpeed(Mathf.Max(Mathf.Abs(newV.z / normalWalkingSpeed), 0.2f));
        }
        else if (!GameManager.GameFinished)
            newV = new Vector3(0, 0, normalWalkingSpeed);

        rigidBody.velocity = newV;
    }
}
