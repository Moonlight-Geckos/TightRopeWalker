using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Serialized

    [SerializeField]
    [Range(3f, 7f)]
    private float playerBounds = 5;

    [SerializeField]
    private float noBirdsDistance = 5f;

    [SerializeField]
    private float noFallDistance = 15f;

    #endregion

    #region Private

    private static GameManager _instance;
    private static bool gameStarted = false;
    private static bool gameFinished = false;

    private Transform playerTransform;
    private Transform ropeEnd;
    private float startDistance;
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
    public bool NoBirds
    {
        get { return Vector3.Distance(playerTransform.position, ropeEnd.position) <= noBirdsDistance; }
    }
    public bool NoFall
    {
        get { return Vector3.Distance(playerTransform.position, ropeEnd.position) <= noFallDistance; }
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
            startDistance = Vector3.Distance(playerTransform.position, ropeEnd.position);
            EventsPool.ClearPoolsEvent.Invoke();
            gameStarted = false;
            gameFinished = false;
            EventsPool.GameStartedEvent.AddListener(() =>
            {
                gameStarted = true;
                gameFinished = false;
                TinySauce.OnGameStarted(SceneManager.GetActiveScene().buildIndex.ToString());
            });
            EventsPool.PlayerFallenEvent.AddListener((bool d) =>
            {
                gameStarted = false;
                gameFinished = true;
                TinySauce.OnGameFinished(true,
                    1 - (Vector3.Distance(playerTransform.position, ropeEnd.position) / startDistance),
                    SceneManager.GetActiveScene().buildIndex.ToString());
            });
            EventsPool.PlayerWonEvent.AddListener(() =>
            {
                gameStarted = false;
                gameFinished = true;
                TinySauce.OnGameFinished(true, 1, SceneManager.GetActiveScene().buildIndex.ToString());
                
            });
            EventsPool.BalloonPopped.AddListener(() =>
            {
                PlayerStorage.CoinsCollected = PlayerStorage.CoinsCollected + 1;
                EventsPool.UpdateUI.Invoke();
            });
        }
    }
    private void Start()
    {
        EventsPool.UpdateUI.Invoke();
    }

    private void Update()
    {
        TimersPool.UpdateTimers(Time.deltaTime);
    }
#endregion
}