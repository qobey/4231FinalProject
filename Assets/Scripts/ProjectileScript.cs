using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public int damage;
    private Rigidbody rb;
    private bool hitTarget;

    public float maxLife;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(hitTarget)
        return;
        else hitTarget = true;
    }
    private void Update()
    {
        //counts down the amount of time the projectile will be active on the screen
        maxLife -= Time.deltaTime;
        //once it reaches zero it will destroy itself
        if (maxLife <=0) 
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
private void OnCollisionEnter(Collision collision)
{
    
    //check if the target has the basic enemy script on it
    if(collision.gameObject.GetComponent<BasicEnemy>() != null)
    {
        //if it does it will call on the take damage function inside the basicenemy script, then destroy itself
        BasicEnemy enemy = collision.gameObject.GetComponent<BasicEnemy>();
        enemy.TakeDamage(damage);
        Destroy(gameObject);
    }


}
}
