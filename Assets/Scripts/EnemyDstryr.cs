using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDstryr : MonoBehaviour
{
    //collider gets triggered

    [SerializeField] LayerMask enemyLayers;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"I got touched by {other.name}");

       if(other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);      
        }

      
    }   
}
