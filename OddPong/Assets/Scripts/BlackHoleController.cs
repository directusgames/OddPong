using UnityEngine;
using System.Collections;

public class BlackHoleController : MonoBehaviour
{
    /// <summary>
    /// Time in seconds to take when coming in/out.
    /// </summary>
    public float timeToScale = 2.0f;

    private FadingAudioSource _loopingSound;
    private ScalingMode _scalingMode = ScalingMode.ScaleUp;

    private enum ScalingMode
    {
        ScaleUp,
        ScaleDown,
        None
    }

    // Use this for initialization
    void Start()
    {
        _loopingSound = GetComponentInChildren<FadingAudioSource>();
        _loopingSound.FadeTime = timeToScale;
        _loopingSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (ScalingMode.ScaleUp == _scalingMode)
        {
            if (transform.localScale.x < 1.0f)
            {
                float scaleAmount = 1.0f / timeToScale * Time.deltaTime;
                transform.localScale = new Vector3(Mathf.Min(1.0f, transform.localScale.x + scaleAmount),
                                                   Mathf.Min(1.0f, transform.localScale.y + scaleAmount),
                                                   1.0f);
            }
            else
            {
                _scalingMode = ScalingMode.None;
            }
        }
        else if (ScalingMode.ScaleDown == _scalingMode)
        {
            if (transform.localScale.x > 0.0f)
            {
                float scaleAmount = 1.0f / timeToScale * Time.deltaTime;
                transform.localScale = new Vector3(Mathf.Max(0.0f, transform.localScale.x - scaleAmount),
                                                   Mathf.Max(0.0f, transform.localScale.y - scaleAmount),
                                                   1.0f);
            }
            else
            {
                // We're done, can destroy ourselves.
                _scalingMode = ScalingMode.None;
                Destroy(gameObject);
            }
        }
    }

    public void ScaleDown()
    {
        _scalingMode = ScalingMode.ScaleDown;
        _loopingSound.FadeOut();
    }
}
