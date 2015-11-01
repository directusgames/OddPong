using UnityEngine;

public class Attractor : MonoBehaviour
{
    public float massInfluence;
    // Factor of 2.5 was found to be a reasonable compromise between
    // orbiting and completely collapsing/having no impact.
    public float attractionPower;
    public string matchThisTag;

    private float _range;

    void Start()
    {
        _range = GetComponent<CircleCollider2D>().radius * Mathf.Max(transform.localScale.x, transform.localScale.y);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (matchThisTag == other.gameObject.tag)
        {
            AttractToSelf(other.gameObject);
        }
    }

    private void AttractToSelf(GameObject other)
    {
        Vector3 diff = other.transform.position - transform.position;
        // Only affect those inside our range (double check).
        if (diff.sqrMagnitude <= _range * _range)
        {
            float influencePercent = (_range - diff.magnitude) / _range;
            Vector3 force = -diff.normalized * Mathf.Pow(influencePercent, attractionPower) * massInfluence;
            other.GetComponent<Rigidbody2D>().AddForce(force);
        }
    }
}
