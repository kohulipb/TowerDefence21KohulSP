using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class Enemy : MonoBehaviour
{

    
    [SerializeField] float speed = 5.0f;
    //rotate health bar to cam
    [SerializeField] Vector3 barTarget = new Vector3(0, 3.77f, -7.46f);

    // get reference to the road
    [SerializeField] EnemyPath enemyPath;

    //Health
    [SerializeField] float maxHealth = 10.0f;
    private float currentHealth;
    [SerializeField] HealthBar healthBar;

    // remember where to go
    private int currentTargetWaypoint = 0;

    private bool hasReachedEnd;

    private void Awake()
    {
        //Set max health
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (!hasReachedEnd)
        {   

            // block comment:    ctrl + k, ctrl + c  
            // block uncomment:  ctrl + k, ctrl + u

            // look at the destination
            transform.LookAt(enemyPath.GetWayPoint(currentTargetWaypoint));

            // move toward the destination
            transform.position = Vector3.MoveTowards(
                transform.position,                                     // where from
                enemyPath.GetWayPoint(currentTargetWaypoint).position,  // where to
                speed * Time.deltaTime                                  // how fast
                );

            // are we close enough to the destination?
            if (Vector3.Distance(transform.position,
                enemyPath.GetWayPoint(currentTargetWaypoint).position) < 0.2f)
            {
                // increment the current target waypoint
                currentTargetWaypoint++;

                // have we surpassed the last waypoint?
                if (currentTargetWaypoint >= enemyPath.GetNumberOfWaypoints())
                {
                    hasReachedEnd = true; // we have reached the end of the road
                }
            }

        }
        healthBar.transform.LookAt(barTarget);
    }
    // let the enemy know which path to follow
    public void SetEnemyPath(EnemyPath incomingPath)
    {
        enemyPath = incomingPath;
    }

    public void InflictDamage(float incomingDamage)
    {
        currentHealth -= incomingDamage;

        //Update the health bar
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        

        switch (currentHealth)
        {
            case <= 0:
            //get money here, later
            Destroy(this.gameObject);
            break;
        }
    }
}
