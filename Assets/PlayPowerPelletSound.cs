
using UnityEngine;

public class PlayPowerPelletSound : MonoBehaviour
{
    public AudioSource  powerPelletSource;
    public AudioClip    powerPelletSound;
    public PowerPellet powerPellet;
    
    private void Start()
    {
        powerPelletSource = this.gameObject.GetComponent<AudioSource>();
        powerPellet = FindObjectOfType<PowerPellet>();
    }
    
    public void PlayPowerPelletSoundMethod()
    {
        powerPelletSource.Play();
        CancelInvoke();
        Invoke(nameof(Stop), powerPellet.duration);
        
    }

    public void Stop()
    {
        powerPelletSource.Stop();
    }



}
