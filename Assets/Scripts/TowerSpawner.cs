using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerSpawner : MonoBehaviour
{
    private Tower towerToSpawn;
    [SerializeField] LayerMask  groundLayers;

    [Header("Towers user can buy")]
    //Types of towers
    [SerializeField] Tower defaultTower;
    //more to come...

    [Header("Grid")]
    [SerializeField] Tilemap tilemap;
    private Vector3Int cellPosition;

    private bool spawnerIsActive;

    private void Awake()
    {
        spawnerIsActive = false;
        towerToSpawn = null;
    }
    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.T) && !spawnerIsActive) 
        {
            towerToSpawn = Instantiate(defaultTower, GetMousePosition(), Quaternion.identity);
            spawnerIsActive =true;
        }
        

        if (spawnerIsActive) 
        {
            towerToSpawn.transform.position = GetMousePosition();

            //Drop the tower
            if (Input.GetMouseButton(0)) 
            {
                towerToSpawn.activateTower();
                towerToSpawn = null;
                spawnerIsActive=false;
            }
            //Cancel placemet
            else if(Input.GetMouseButton(1))
            {
                Destroy(towerToSpawn.gameObject);
                spawnerIsActive = false;
            }
        }
    }
    private Vector3 GetMousePosition() 
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        //Fire the laser
        if (Physics.Raycast(ray, out hit, 200f, groundLayers)) 
        {
            //We hit something
            Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);


            //convert hit point to a position on the grid
            cellPosition = tilemap.LocalToCell(hit.point);

            return new Vector3(
                 cellPosition.x + tilemap.cellSize.x/2f,
                 0,
                 cellPosition.y + tilemap.cellSize.y / 2f
                );
        }

        return Vector3.zero;

      
    }
}
