using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 10f;

    private Rigidbody rb;
    private Enemy targetedEnemy;
    [SerializeField] float damage = 5f;
    private Vector3 lastDirection;
    [SerializeField] private ParticleSystem ParticleFX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Prevent the Rigidbody from affecting the projectile initially
    }
    //Setup the projectile as soon as it is created
    public void Setup(Vector3 enemyDirection, Enemy incomingTargetedEnemy)
    {
        targetedEnemy = incomingTargetedEnemy; // who to chase?
        lastDirection = (targetedEnemy.transform.position - transform.position).normalized;
        //Vector3 force = enemyDirection * 5.0f;
        //rb.AddForce(force, ForceMode.Impulse);
    }
    private void Update()
    {
        if (targetedEnemy) // if the targeted enemy is still alive
        {
            lastDirection = (targetedEnemy.transform.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(
                        transform.position,
                        targetedEnemy.transform.position,
                        speed * Time.deltaTime);
        }
        else if (rb.isKinematic) // Target is gone, and Rigidbody is not yet active
        {
            ActivateRigidbody();
        }
    }
    private void ActivateRigidbody()
    {
        rb.isKinematic = false; // Allow the Rigidbody to be affected by physics
        rb.velocity = lastDirection * speed; // Continue in the last known direction
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetedEnemy.gameObject)
        {
            targetedEnemy.InflictDamage(damage);
            Destroy(this.gameObject);
        }

        //Spawn fx
        Instantiate(ParticleFX, transform.position, Quaternion.identity);
    }
}

