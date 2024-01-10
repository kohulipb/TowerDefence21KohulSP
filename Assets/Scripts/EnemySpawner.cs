using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /* ENEMY SPAWNER
     * Spawns an enemy wave onto a chosen path
     */

    [SerializeField] EnemyPath enemyPathA;
    [SerializeField] EnemyPath enemyPathB;
    [SerializeField] EnemyPath enemyPathC;

    [SerializeField] Enemy enemy;
    [SerializeField] Enemy enemyFast;
    [SerializeField] Enemy enemyHeavy;

    private void SpawnEnemy(Enemy enemyToSpawn, EnemyPath chosenPath)
    {
        Instantiate(enemyToSpawn, transform.position, Quaternion.identity).SetEnemyPath(chosenPath);
    }

    private void Awake()
    {
        StartCoroutine(Wave01());   
    }

    IEnumerator Wave01()
    {
        yield return new WaitForSeconds(2); //wait for 2 seconds
        SpawnEnemy(enemy, enemyPathA);       // spawn
        yield return new WaitForSeconds(2); //wait for 2 seconds
        SpawnEnemy(enemy, enemyPathB);       // spawn
        yield return new WaitForSeconds(.5f); //wait for 1/2 seconds
        SpawnEnemy(enemyFast, enemyPathA);       // spawn
        yield return new WaitForSeconds(2); //wait for 2 seconds
        SpawnEnemy(enemyHeavy, enemyPathC);
        yield return new WaitForSeconds(4); //wait for 2 seconds
        SpawnEnemy(enemyHeavy, enemyPathA);       // spawn// spawn
        yield return new WaitForSeconds(.1f); //wait for 2 seconds
        SpawnEnemy(enemyHeavy, enemyPathB);       // spawn
    }
}
