using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPoint : MonoBehaviour
{
    public ParticleSystem clearParticle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            Vector3 tempPos = transform.position;
            tempPos.y += 1f;
            Instantiate(clearParticle, tempPos, transform.rotation);
            GameManager.instance.SetCleared(true);
        }
    }
}
