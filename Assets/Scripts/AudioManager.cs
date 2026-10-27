using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource ambientSound;     // plays looping ambient sound
    public AudioSource pickupSound;         // plays one-shot VFX sounds
    public AudioSource gameOverSound;

    void Start()
    {
        // Start playing ambient sound    
        ambientSound.loop = true;
        ambientSound.Play();
    }

    public void PlayItemPickup()
    {
        pickupSound.Play();
    }

    public void PlayGameOver()
    {
        // Stop ambient
        ambientSound.Stop();

        // Play game over SFX
        gameOverSound.Play();
    }
}