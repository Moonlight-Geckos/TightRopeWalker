using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Serialized

    [SerializeField]
    [Range(3f, 7f)]
    private float playerBounds = 5;

    #endregion

    #region Private

    private static GameManager _instance;
    private UnityEvent playerTappedEvent = new UnityEvent();

    #endregion

    #region Public

    public static GameManager Instance
    {
        get { return _instance; }
    }

    public float PlayerBounds
    {
        get { return playerBounds; }
    }

    public UnityEvent PlayerTappedEvent
    {
        get { return playerTappedEvent; }
    }

    #endregion

    #region Methods

    private void Awake()
    {
        if(_instance != null && _instance == this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion
}
