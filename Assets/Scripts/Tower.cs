using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour
{
    [SerializeField] float range = 1.5f;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform firingPoint;

    private bool towerIsActive;
    //Timers
    [SerializeField] float firingTimer;
    [SerializeField] float firingDelay = 1.0f;

    float scanningTimer;
    float scanningDelay = 0.1f;

    //Enemy bookkeeping
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] Collider[] colliders;
    [SerializeField] List<Enemy> enemiesInRange;
    [SerializeField] Enemy targetedEnemy;


    private void Awake()
    {
        //Initialize setup
        towerIsActive = false;
    }
    private void Update()
    {
        if (towerIsActive)
        {
            //SCANNING PART

            scanningTimer += Time.deltaTime;
            if (scanningTimer >= scanningDelay)
            {
                scanningTimer = 0f;
                ScanForEnemies();
            }

            //FIRING PART

            if (targetedEnemy)
            {
                firingTimer += Time.deltaTime;
            }


            if (firingTimer >= firingDelay)
            {
                firingTimer = 0f;
                OverlapFire();
            }
        }
        
    }

    private void ScanForEnemies()
    {
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

    }
    private void OverlapFire()
    {

        //make sure theres something to shoot at
        if (targetedEnemy != null)
        {

            //Get enemy direction relative
            Vector3 enemyDirection = (targetedEnemy.transform.position - firingPoint.position).normalized;
            //create a projectile
            Instantiate(projectile, firingPoint.position, Quaternion.identity).Setup(enemyDirection, targetedEnemy);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void activateTower() 
    {
        towerIsActive = true;
    }









}