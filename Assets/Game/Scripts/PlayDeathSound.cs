using UnityEngine;

public class PlayDeathSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deathSound;
    
    public void PlayTheDeathSound()
    {
        audioSource.PlayOneShot(deathSound);
    }
}
