using UnityEngine;

public class CoinsParticles : MonoBehaviour
{
    ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        EventsPool.BalloonPopped.AddListener(() => ps.Play());
    }
}
