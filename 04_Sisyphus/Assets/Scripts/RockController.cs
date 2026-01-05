using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    Rigidbody rigid;
    float tanY_Z;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        tanY_Z = Mathf.Tan(20 * Mathf.Deg2Rad);
    }

    private void Update()
    {
        float rockY = transform.position.y;
        float rockZ = transform.position.z;

        if (rockY < -20)
        {
            transform.position = GameManager.instance.rockRespawnPoint;
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
        else if (rockY < rockZ * tanY_Z - 25)
        {
            Vector3 respawnPoint = new Vector3(0, rockY + 30, rockZ - 20);
            transform.position = respawnPoint;
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }
}
