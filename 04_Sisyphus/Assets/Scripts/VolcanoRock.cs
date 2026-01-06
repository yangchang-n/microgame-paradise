using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoRock : MonoBehaviour
{
    public ParticleSystem destroyParticle;

    private void Update()
    {
        if (transform.position.y < 22)
        {
            Instantiate(destroyParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
