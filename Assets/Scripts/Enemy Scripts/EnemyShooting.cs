using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShooting : MonoBehaviour
{
    //bullet
    public GameObject EnemyProjectile;
    public NavMeshAgent nma;
    public LayerMask groundID, playerID;
    public Transform player;

    //patrolling variables
    public Vector3 point;
    public bool pointSet;

    public float walkRange;
    //attacking variables
    public Transform shotPoint;
    bool attackedPlayer;
    public float attackInterval;
    //states
    public float sight, attackRange;
    public bool inSight, inRange;
    public float shotForce;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        nma = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        inSight = Physics.CheckSphere(transform.position, sight, playerID);
        inRange = Physics.CheckSphere(transform.position, attackRange, playerID);

        if(!inSight && !inRange) 
        {
            Patrol();
        }
        if (inSight && !inRange) 
        {
            Pursue();
        }
        if (inSight && inRange)
        {
            ShootPlayer();
        }
        if (player == null) 
        {
            Patrol(); 
        }

    }
    private void Patrol()
    {
        if (!pointSet) 
        {
            WalkPointSearch();
        }
        if(pointSet) 
        {
            nma.SetDestination(point);
        }
        Vector3 distanceToPoint = transform.position - point;
        if (distanceToPoint.magnitude < 1f) 
        {
            pointSet = false;
        }
    }
    private void WalkPointSearch()
    {
        float randomXPoint = Random.Range(-walkRange, walkRange);
        float randomZPoint = Random.Range(-walkRange, walkRange);

        point = new Vector3(transform.position.x + randomXPoint, transform.position.y, transform.position.z + randomZPoint);
        //checking to make sure the enemy is still on the ground
        if(Physics.Raycast(point, -transform.up, 2f, groundID))
        pointSet = true;
    }
    private void Pursue()
    {
        nma.SetDestination(player.position);
    }
    private void ResetAttack() 
    {
        attackedPlayer = false;
    }
    private void ShootPlayer()
    {
        nma.SetDestination(transform.position);
        transform.LookAt(player);

        if(!attackedPlayer) 
        { 
        //code for shooting
        Rigidbody rb = Instantiate(EnemyProjectile, shotPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * shotForce, ForceMode.Impulse);
        
    
            attackedPlayer = true;
            Invoke(nameof(ResetAttack), attackInterval);
        }
    }
}