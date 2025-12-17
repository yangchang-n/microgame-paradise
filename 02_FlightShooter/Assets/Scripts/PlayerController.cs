using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRb;
    Vector2 pushForce;
    int healthPoint = 5;

    int bulletMaxCooltime = 10;
    int missileMaxCooltime = 30;

    int bulletCooltime;
    int missileCooltime;

    public GameObject bulletPrefab;
    public GameObject missilePrefab;

    AudioSource audioSource;
    public ParticleSystem explodeParticlePrefab;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        pushForce = new Vector2(0, 0);
        healthPoint = 5;
        bulletCooltime = bulletMaxCooltime;
        missileCooltime = missileMaxCooltime;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.1f;
    }

    void Update()
    {
        pushForce = new Vector2(0, 0);
        if (bulletCooltime < bulletMaxCooltime) bulletCooltime++;
        if (missileCooltime < missileMaxCooltime) missileCooltime++;

        if (Input.GetKey(KeyCode.A)) pushForce.x = -1;
        else if (Input.GetKey(KeyCode.D)) pushForce.x = 1;

        if (Input.GetKey(KeyCode.W)) pushForce.y = 1;
        else if (Input.GetKey(KeyCode.S)) pushForce.y = -1;

        if (Input.GetKey(KeyCode.J) && bulletCooltime == bulletMaxCooltime)
        {
            Vector2 tempPosition = transform.position;
            tempPosition.y += 1f;
            Instantiate(bulletPrefab, tempPosition, Quaternion.identity);
            audioSource.Play();
            bulletCooltime = 0;
        }

        if (Input.GetKey(KeyCode.K) && missileCooltime == missileMaxCooltime)
        {
            Vector2 tempPosition = transform.position;
            GameObject m1 = Instantiate(missilePrefab, tempPosition, Quaternion.identity);
            GameObject m2 = Instantiate(missilePrefab, tempPosition, Quaternion.identity);
            m2.GetComponent<PlayerMissileController>().leftSide = false;
            audioSource.Play();
            missileCooltime = 0;
        }

        pushForce.Normalize();
        playerRb.AddForce(pushForce * 15);
    }

    public void DecreaseHp()
    {
        healthPoint--;
        GameObject.Find("HPText").GetComponent<StageUI>().UpdateHPUI();
        if (healthPoint <= 0)
        {
            healthPoint = 0;
            Die();
        }
    }

    void Die()
    {
        GameManager.instance.SetIsGameOver(true);
        Instantiate(explodeParticlePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void SetHP(int hp)
    {
        healthPoint = hp;
    }

    public int GetHP()
    {
        return healthPoint;
    }
}
