using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip walkSound;
    public AudioClip jumpSound;
    public AudioClip breakSound;
    public AudioClip portalSound;

    public bool isWalkSoundPlayed = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWalkSound()
    {
        audioSource.clip = walkSound;

        audioSource.loop = true;
        isWalkSoundPlayed = true;
        audioSource.Play();
    }

    public void StopWalkSound()
    {
        isWalkSoundPlayed = false;
        audioSource.Stop();
    }

    public void PlayJumpSound()
    {
        isWalkSoundPlayed = false;
        audioSource.PlayOneShot(jumpSound);
    }
    
    public void PlayBreakSound()
    {
        isWalkSoundPlayed = false;
        audioSource.PlayOneShot(breakSound);
    }

    public void PlayPortalSound()
    {
        isWalkSoundPlayed = false;
        audioSource.PlayOneShot(portalSound);
    }
}
