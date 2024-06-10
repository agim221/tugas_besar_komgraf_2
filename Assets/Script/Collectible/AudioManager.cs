using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip pickUp;
    public AudioClip openDoor;
    public AudioClip walking;
    public AudioClip sucessRitual;

    private void Start() {
        PlayMusic(background, true);
    }

    public void PlayMusic(AudioClip clip, bool loop) {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip) {
        SFXSource.PlayOneShot(clip);
    }
}