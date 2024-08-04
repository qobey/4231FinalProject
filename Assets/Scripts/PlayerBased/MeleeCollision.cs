using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeCollision : MonoBehaviour





{
    public AnimationScript anim;

void OnTriggerEnter(Collider other)
{
    if (other.tag == "Enemy" && anim.isAttacking) 
    {
        BasicEnemy enemy = other.gameObject.GetComponent<BasicEnemy>();
        enemy.TakeDamage(10);
    }
}
}
