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

    [SerializeField]
    private float safeDistance = 5f;

    #endregion

    #region Private

    private static GameManager _instance;
    private static bool gameStarted = false;
    private static bool gameFinished = false;

    private Transform playerTransform;
    private Transform ropeEnd;

    private Timer timer;

    #endregion

    #region Public

    public float PlayerBounds
    {
        get { return playerBounds; }
    }
    public Transform PlayerTransform
    {
        get { return playerTransform; }
    }
    public bool PlayerCloseToWin
    {
        get { return Vector3.Distance(playerTransform.position, ropeEnd.position) <= safeDistance; }
    }
    public static GameManager Instance
    {
        get { return _instance; }
    }
    public static bool GameStarted
    {
        get { return gameStarted; }
    }
    public static bool GameFinished
    {
        get { return gameFinished; }
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
            playerTransform = GameObject.FindGameObjectWithTag("Walker").transform;
            ropeEnd = GameObject.FindGameObjectWithTag("RopeEnd").transform;
            EventsPool.PlayerFallenEvent.AddListener((bool d) =>
            {
                gameStarted = false;
                gameFinished = true;
            });
            EventsPool.PlayerWonEvent.AddListener(() =>
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
