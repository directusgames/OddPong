using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour
{
    public int startSpeed;

    public GameObject racquetLeft, racquetRight;

    private float _frozenAngularVelocity;
    private Vector3 _frozenVelocity = new Vector3();
    private AudioSource hitSound;

    void Start()
    {
        hitSound = GetComponent<AudioSource>();
    }

    public void Freeze()
    {
        var rigid = GetComponent<Rigidbody2D>();
        _frozenAngularVelocity = rigid.angularVelocity;
        _frozenVelocity = rigid.velocity;
        rigid.angularVelocity = 0.0f;
        rigid.velocity = new Vector3();
        rigid.isKinematic = true;
        this.enabled = false;
    }

    public void Thaw()
    {
        var rigid = GetComponent<Rigidbody2D>();
        rigid.angularVelocity = _frozenAngularVelocity;
        rigid.velocity = _frozenVelocity;
        rigid.isKinematic = false;
        this.enabled = true;
    }

    //Check which section of racquet the ball hits
    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {

        //1 = top of paddle
        //2 = middle of paddle
        //3 = bottom of paddle
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (this.isActiveAndEnabled)
        {
            // Randomly alter the pitch so we don't go crazy hearing the same sound.
            hitSound.pitch = Random.Range(0.75f, 1.25f);
            hitSound.Play();
        }
        if (col.gameObject.name == racquetLeft.name)
        {

            float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);

            Vector2 dir = new Vector2(1, y).normalized;

            GetComponent<Rigidbody2D>().velocity = dir * startSpeed;
        }
        else if (col.gameObject.name == racquetRight.name)
        {

            float y = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            Vector2 dir = new Vector2(-1, y).normalized;

            GetComponent<Rigidbody2D>().velocity = dir * startSpeed;
        }
    }
}
