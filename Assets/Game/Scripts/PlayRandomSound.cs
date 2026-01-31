using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private AudioSource audioSource;

    public void Play()
    {
        audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
    }
}
