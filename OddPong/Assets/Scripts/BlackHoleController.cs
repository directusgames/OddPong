using UnityEngine;
using System.Collections;

public class BlackHoleController : MonoBehaviour
{
    /// <summary>
    /// Time in seconds to take when coming in/out.
    /// </summary>
    public float timeToScaleUp = 2.0f;
    public float timeToScaleDown = 0.5f;
    public float maxLifetime = 8.0f;

    private FadingAudioSource _loopingSound;
    public ScalingMode _scalingMode = ScalingMode.ScaleUp;
    private float _startTime;

    public enum ScalingMode
    {
        ScaleUp,
        ScaleDown,
        None
    }

    // Use this for initialization
    void Start()
    {
        _loopingSound = GameObject.Find ("Wormhole Loop Sound").GetComponent<FadingAudioSource>();
        _loopingSound.FadeTime = timeToScaleDown;
        _loopingSound.Play();
        _startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (ScalingMode.ScaleUp == _scalingMode)
        {
            if (transform.localScale.x < 1.0f)
            {
                float scaleAmount = 1.0f / timeToScaleUp * Time.deltaTime;
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
                float scaleAmount = 1.0f / timeToScaleDown * Time.deltaTime;
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
        else // should we timeout and start collapsing ourselves?
        {
            if (Time.time - _startTime > maxLifetime)
            {
                ScaleDown();
            }
        }
    }

    public void ScaleDown()
    {
		transform.GetChild(0).GetComponent<CircleCollider2D>().enabled = false;
        _scalingMode = ScalingMode.ScaleDown;
        _loopingSound.FadeOut();
    }
    
    public void ScaleUp()
    {
    }
    
    public void StopScaling()
    {
		_scalingMode = ScalingMode.None;
    }
}
