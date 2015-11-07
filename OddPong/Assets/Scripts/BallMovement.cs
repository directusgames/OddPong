using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    public float startSpeed;
    public float speedUpPerSecond = 0.0f;
    public float rotationSpeed;
    public Text txtBallVel;

    public GameObject racquetLeft, racquetRight;

    private AudioSource _hitSound;
    private Rigidbody2D _rigid;
    private bool _raiseSpeed;

    void Start()
    {
        var velocityTextObj = GameObject.Find("Ball Velocity");
        if (velocityTextObj)
        {
            txtBallVel = velocityTextObj.GetComponent<Text>();
        }
        if (speedUpPerSecond <= 0.0f)
        {
            speedUpPerSecond = startSpeed;
        }
        _hitSound = GetComponent<AudioSource>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (txtBallVel)
        {
            txtBallVel.text = "X Velocity: " + _rigid.velocity.x;
        }
        
		transform.GetChild (1).transform.Rotate(0, Time.deltaTime*rotationSpeed, 0, Space.World);
		
    }

    //Check which section of racquet the ball hits
    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        //1 = top of paddle
        //2 = middle of paddle
        //3 = bottom of paddle
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    public void GetUpToSpeed()
    {
        _raiseSpeed = true;
    }

    void FixedUpdate()
    {
        if (_raiseSpeed)
        {
            float speed = _rigid.velocity.magnitude;
            if (speed < startSpeed)
            {
                float increase = speed + Time.fixedDeltaTime * speedUpPerSecond;
                // Manually normalise, because Vector2D.Normalize() doesn't do anything???
                if (speed != 0.0f)
                {
                    _rigid.velocity /= speed;
                }
                _rigid.velocity *= increase;
            }
            else
            {
                _raiseSpeed = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 dir;

        if (this.isActiveAndEnabled)
        {
            // Randomly alter the pitch so we don't go crazy hearing the same sound.
            _hitSound.pitch = Random.Range(0.75f, 1.25f);
            _hitSound.Play();
        }
        if (col.gameObject.name == racquetLeft.name)
        {

            float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);

            if (_rigid.velocity.x > 0)
            {
                dir = new Vector2(1, y).normalized;
            }
            else
            {
                dir = new Vector2(-1, y).normalized;
            }
            _rigid.velocity = dir * startSpeed;
        }
        else if (col.gameObject.name == racquetRight.name)
        {

            float y = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            if (_rigid.velocity.x < 0)
            {
                dir = new Vector2(-1, y).normalized;
            }
            else
            {
                dir = new Vector2(1, y).normalized;
            }

            _rigid.velocity = dir * startSpeed;
        }
        
//		Quaternion newrotation = Random.rotation;
//		transform.rotation = newrotation;
    }
}
