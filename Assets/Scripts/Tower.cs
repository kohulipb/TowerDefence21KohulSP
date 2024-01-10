using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour
{
    [SerializeField] float range = 1.5f;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform firingPoint; 

    [SerializeField] LayerMask enemyLayers;
    [SerializeField] Collider[] colliders;
    [SerializeField] List<Enemy> enemiesInRange;
    [SerializeField] Enemy targetedEnemy;
         
    private void Update()
    {
        //-----SCANNING------

        //find the surrounding colliders, only detect objects on enemy layers
        colliders = Physics.OverlapSphere(transform.position, range, enemyLayers);

        //clear the list first
        enemiesInRange.Clear();

        //Go over all colliders
        foreach (Collider collider in colliders)
        {
            enemiesInRange.Add(collider.GetComponent<Enemy>());
        }


        //If there are enemies in range pick one to target
        if (enemiesInRange.Count > 0)
        {
            targetedEnemy = enemiesInRange[0];
        }


        //-----FIRING------

        //make sure theres something to shoot at
        if (targetedEnemy != null)
        {

            //Get enemy direction relative
            Vector3 enemyDirection = (targetedEnemy.transform.position - firingPoint.position).normalized;
            //create a projectile
            Instantiate(projectile, firingPoint.position, Quaternion.identity).Setup(enemyDirection);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }









}