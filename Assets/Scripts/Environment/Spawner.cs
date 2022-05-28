using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Serialized
    [Header("Birdos")]
    [SerializeField]
    private GameObjectPool birdPool;

    [SerializeField]
    private float birdSpawnCooldown = 5f;

    #endregion

    private Timer birdSpawnTimer;

    private void Awake()
    {
        birdSpawnTimer = TimersPool.Pool.Get();
        birdSpawnTimer.Duration = birdSpawnCooldown;
        birdSpawnTimer.AddTimerFinishedEventListener(SpawnBird);
        EventsPool.GameStartedEvent.AddListener(SpawnBird);
    }
    private void SpawnBird()
    {
        GameObject bird = birdPool.Pool.Get();
        bird.GetComponent<BirdController>().Initialize(transform.position);

        if (GameManager.Instance.NoBirds)
        {
            TimersPool.Pool.Release(birdSpawnTimer);
            birdSpawnTimer = null;
            return;
        }
        birdSpawnTimer.Run();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector3.one * 3);
    }

}
