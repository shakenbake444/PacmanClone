
using UnityEngine;

public class PlaySoundSingleton : MonoBehaviour
{
    public static PlaySoundSingleton Instance;
    public AudioSource  audioSource;
    public AudioClip    audioClip;
    public PowerPellet  powerPellet;

    private void Awake()
    {
        Instance = this;
        powerPellet = FindObjectOfType<PowerPellet>();
    }

    public void PlaySound()
    {
        audioSource.Play();
        CancelInvoke();
        Invoke(nameof(StopSound), powerPellet.duration);

    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}


