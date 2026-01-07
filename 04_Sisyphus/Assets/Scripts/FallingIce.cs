using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIce : MonoBehaviour
{
    public ParticleSystem destroyParticle;

    private void Update()
    {
        if (transform.position.y < 38)
        {
            Instantiate(destroyParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(destroyParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
