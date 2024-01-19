using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 10f;

    private Rigidbody rb;
    private Enemy targetedEnemy;
    [SerializeField] float damage = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    //Setup the projectile as soon as it is created
    public void Setup(Vector3 enemyDirection, Enemy incomingTargetedEnemy)
    {
        targetedEnemy = incomingTargetedEnemy;  
        //Vector3 force = enemyDirection * 5.0f;
        //rb.AddForce(force, ForceMode.Impulse);
    }
    private void Update()
    {
        if (targetedEnemy)
        {
            transform.position = Vector3.MoveTowards(
                        transform.position,
                        targetedEnemy.transform.position,
                        speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetedEnemy.gameObject)
        {
            targetedEnemy.InflictDamage(damage);
            Destroy(this.gameObject);
        }
    }   
}
