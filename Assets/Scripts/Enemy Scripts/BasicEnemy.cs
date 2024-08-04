using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public int health;
    public AudioManagerScript audioManagerScript;

    void Start() 
    {
        audioManagerScript = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        audioManagerScript.PlaySFX(audioManagerScript.explosion);
        if(health <= 0) 
        {
            
            Destroy(gameObject);
        }
    }
}
