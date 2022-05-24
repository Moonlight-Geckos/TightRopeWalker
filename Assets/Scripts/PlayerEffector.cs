using UnityEngine;

public class PlayerEffector : MonoBehaviour
{
    #region Serialized

    [SerializeField]
    [Range(0f, 2f)]
    private float gravityModifier = 0.3f;

    #endregion


    #region Private

    private Transform bodyTarget;

    #endregion
    private void Awake()
    {
        bodyTarget = GameObject.FindGameObjectWithTag("BodyTarget").transform;
    }

    private void Start()
    {
        if (Random.Range(-10, 10) % 2 == 0)
        {
            bodyTarget.localPosition = new Vector3(-1, 0, 0);
        }
        else
        {
            bodyTarget.localPosition = new Vector3(1, 0, 0);
        }
    }


    private void Update()
    {
        float offBalanceForce;
        if (bodyTarget.localPosition.x < 0)
            offBalanceForce = (-GameManager.Instance.PlayerBounds - bodyTarget.localPosition.x) * Time.deltaTime * gravityModifier;
        else
            offBalanceForce = (GameManager.Instance.PlayerBounds - bodyTarget.localPosition.x) * Time.deltaTime * gravityModifier;
        bodyTarget.localPosition = new Vector3(bodyTarget.localPosition.x + offBalanceForce, 0, 0);
    }
}
