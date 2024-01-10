using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    
    [SerializeField] float speed = 5.0f;

    // get reference to the road
    [SerializeField] EnemyPath enemyPath; 


    // remember where to go
    private int currentTargetWaypoint = 0;

    private bool hasReachedEnd;



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

    }
    // let the enemy know which path to follow
    public void SetEnemyPath(EnemyPath incomingPath)
    {
        enemyPath = incomingPath;
    }
}
