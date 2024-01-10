using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    //Setup the projectile as soon as it is created
    public void Setup(Vector3 enemyDirection)
    {
        Vector3 force = enemyDirection * 5.0f;
        rb.AddForce(force, ForceMode.Impulse);
    }
}
