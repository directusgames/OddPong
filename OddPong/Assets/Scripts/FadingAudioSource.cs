using UnityEngine;

/// <summary>
///   Audio source that fades between clips instead of playing them immediately.
/// </summary>
/// Derived from http://wiki.unity3d.com/index.php/Fading_Audio_Source
[RequireComponent(typeof(AudioSource))]
public class FadingAudioSource : MonoBehaviour
{
    /// <summary>
    /// Time in seconds that the audio takes to fade in/out.
    /// </summary>
    public float FadeTime = 2.0f;

    /// <summary>
    /// Maximum volume of the audio source.
    /// </summary>
    public float MaxVolume = 1.0f;

    /// <summary>
    /// Actual audio source.
    /// </summary>
    private AudioSource audioSource;

    /// <summary>
    /// Whether the audio source is currently fading, in or out.
    /// </summary>
    private FadeState fadeState = FadeState.None;

    /// <summary>
    /// Volume that the fading out gives up at.
    /// </summary>
    private float fadeOutThreshold = 0.01f;

    public enum FadeState
    {
        None,
        FadingOut,
        FadingIn
    }

    /// <summary>
    ///   Continues fading in the current audio clip.
    /// </summary>
    public void Play()
    {
        this.fadeState = FadeState.FadingIn;
        this.audioSource.Play();
    }

    /// <summary>
    ///   Stop playing the current audio clip immediately.
    /// </summary>
    public void Stop()
    {
        this.audioSource.Stop();
        this.fadeState = FadeState.None;
    }

    public void FadeOut()
    {
        this.fadeState = FadeState.FadingOut;
    }

    private void Awake()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        this.audioSource.volume = 0f;
    }

    private void Update()
    {
        if (!this.audioSource.enabled)
        {
            return;
        }

        if (this.fadeState == FadeState.FadingOut)
        {
            if (this.audioSource.volume > this.fadeOutThreshold)
            {
                // Fade out current clip.
                this.audioSource.volume -= this.MaxVolume / this.FadeTime * Time.deltaTime;
            }
            else
            {
                // Stop playing.
                this.Stop();
            }
        }
        else if (this.fadeState == FadeState.FadingIn)
        {
            if (this.audioSource.volume < this.MaxVolume)
            {
                // Fade in current clip.
                this.audioSource.volume += this.MaxVolume / this.FadeTime * Time.deltaTime;
            }
            else
            {
                // Stop fading in.
                this.fadeState = FadeState.None;
            }
        }
    }
}