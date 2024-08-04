using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Script : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform spawnPoint;
    public GameObject goal;


    public void CheckForEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) 
        {
            //Instantiate goal object at specific spawn point
            goal.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
       CheckForEnemies();
    }
}
