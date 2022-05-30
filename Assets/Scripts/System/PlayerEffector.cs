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

    [SerializeField]
    [Range(1f, 7f)]
    private float startDelay = 3;

    #endregion


    #region Private

    private Transform bodyTarget;
    private Rigidbody rigidBody;
    private WalkerController characterControl;
    private Timer timer;
    private bool isEffecting = false;

    private Vector3 newV;
    private float offBalanceForce;
    int sign;

    #endregion
    private void Awake()
    {
        timer = TimersPool.Pool.Get();
        timer.Duration = startDelay;
        timer.AddTimerFinishedEventListener(() => { isEffecting = true; TimersPool.Pool.Release(timer); });
        EventsPool.GameStartedEvent.AddListener(() => timer.Run());

        EventsPool.PlayerFallenEvent.AddListener((bool w) => Destroy(rigidBody));
        bodyTarget = GameObject.FindGameObjectWithTag("BodyTarget").transform;
        rigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        characterControl = rigidBody.GetComponentInChildren<WalkerController>();
    }

    private void Update()
    {
        if (rigidBody == null)
            return;
        if (!GameManager.GameStarted)
        {
            rigidBody.velocity = Vector3.zero;
            return;
        }
        if(GameManager.Instance.NoFall)
            isEffecting = false;
        if (isEffecting)
        {
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
        else
            newV = new Vector3(0, 0, normalWalkingSpeed);
        rigidBody.velocity = newV;
    }
}
