using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileController : MonoBehaviour
{
    Rigidbody2D missileRb;
    Vector2 moveDirection;
    GameObject[] enemies;
    bool bestFound = false;

    public bool leftSide = true;
    float initialForce = 100f;
    float moveForce = 150f;
    int maxCooltime = 30;
    int initialCooltime = 0;

    AudioSource soundManagersAudioSource;
    public AudioClip explodeSFX;
    public ParticleSystem explodeParticlePrefab;

    void Start()
    {
        missileRb = GetComponent<Rigidbody2D>();
        moveDirection = new Vector2(0, 1);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        bestFound = false;

        if (leftSide) missileRb.AddForce(new Vector2(-initialForce, 0));
        else missileRb.AddForce(new Vector2(initialForce, 0));
        initialCooltime = 0;

        soundManagersAudioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (initialCooltime < maxCooltime)
        {
            initialCooltime++;
            return;
        }

        if (!bestFound && enemies.Length > 0)
        {
            GameObject nearest = null;
            Vector2 bestDirection = new Vector2(0, 1);
            float bestDistance = 1000f;
            foreach (var enemy in enemies)
            {
                if (enemy == null) continue;
                Vector2 direction = (enemy.transform.position - transform.position);
                float distance = direction.sqrMagnitude;
                if (distance < bestDistance)
                {
                    bestFound = true;
                    nearest = enemy;
                    bestDirection = direction.normalized;
                    bestDistance = distance;
                }
            }

            if (nearest != null)
            {
                moveDirection = bestDirection;
                float rotationAngle = Mathf.Atan2(bestDirection.y, bestDirection.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0f, 0f, rotationAngle - 90f);
                transform.rotation = rotation;
            }
        }

        missileRb.AddForce(moveDirection * moveForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall") Destroy(gameObject);

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
