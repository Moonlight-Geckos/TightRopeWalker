using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioClip balloonPopClip;

    private AudioSource audioSource;

    private void Awake()
    {
        TryGetComponent(out audioSource);
        if(audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        EventsPool.BalloonPopped.AddListener(BalloonPop);
    }

    private void BalloonPop()
    {
        audioSource.PlayOneShot(balloonPopClip);
    }
}
