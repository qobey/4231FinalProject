using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    public int damageDealt;
    Rigidbody rb;
    private bool hitPlayer;
    public float maxLife;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(hitPlayer)
        return;
        else hitPlayer = true;   
    }

    // Update is called once per frame
    private void Update()
    {
        maxLife -= Time.deltaTime;
        if (maxLife <= 0) 
        Destroy(gameObject);
        
    }
//     private void OnTriggerEnter(Collider collision)
// {
//     if(collision.tag == "Player") 
//     {
//         var healthComponent = collision.GetComponent<PlayerHealth>();
//         if (healthComponent != null) 
//         {
//             healthComponent.TakeDamage(damageDealt);
//         }
//         Destroy(gameObject);
//     }
// }
    private void OnCollisionEnter (Collision collision) 
    {
 //check if the target has the basic enemy script on it
    if(collision.gameObject.GetComponent<PlayerHealth>() != null)
    {
        //if it does it will call on the take damage function inside the basicenemy script, then destroy itself
        PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
        health.TakeDamage(damageDealt);
        Destroy(gameObject);
    }
    }
   
}
