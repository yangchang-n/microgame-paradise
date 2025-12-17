using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    AudioSource soundManagersAudioSource;
    public AudioClip explodeSFX;
    public ParticleSystem explodeParticlePrefab;

    void Start()
    {
        soundManagersAudioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Translate(0, 0.5f, 0);
        if (transform.position.y > 10) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.instance.AddScore();
            soundManagersAudioSource.PlayOneShot(explodeSFX);
            Instantiate(explodeParticlePrefab, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
