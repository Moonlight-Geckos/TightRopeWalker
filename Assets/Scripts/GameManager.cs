using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    #region Serialized

    [SerializeField]
    [Range(3f, 7f)]
    private float playerBounds = 5;

    [SerializeField]
    [Range(1f, 7f)]
    private float startDelay = 3;

    #endregion

    #region Private

    private static GameManager _instance;
    private static bool gameStarted = false;
    private static bool gameFinished = false;

    private Transform playerTransform;

    private static UnityEvent<bool> playerFallenEvent = new UnityEvent<bool>();
    private static UnityEvent playerWonEvent = new UnityEvent();
    private static UnityEvent clearPoolsEvent = new UnityEvent();

    private Timer timer;

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
    public Transform PlayerTransform
    {
        get { return playerTransform; }
    }
    public static bool GameStarted
    {
        get { return gameStarted; }
    }
    public static bool GameFinished
    {
        get { return gameFinished; }
    }
    public static UnityEvent<bool> PlayerFallenEvent
    {
        get { return playerFallenEvent; }
    }
    public static UnityEvent PlayerWonEvent
    {
        get { return playerWonEvent; }
    }
    public static UnityEvent ClearPoolsEvent
    {
        get { return clearPoolsEvent; }
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
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            PlayerFallenEvent.AddListener((bool d) =>
            {
                gameStarted = false;
                gameFinished = true;
            });
            PlayerWonEvent.AddListener(() =>
            {
                gameStarted = false;
                gameFinished = true;
            });
            timer = TimersPool.Pool.Get();
            timer.Duration = startDelay;
            timer.AddTimerFinishedEventListener(StartGame);
            timer.Run();
        }
    }

    private void Update()
    {
        TimersPool.UpdateTimers(Time.deltaTime);
    }

    private void StartGame()
    {
        gameStarted = true;
        TimersPool.Pool.Release(timer);
        timer = null;
    }

    #endregion
}
