using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    //Health Variables
    public int startingHealth = 100;
    public int currentHealth;
    public PlayerMovementAdvanced player;
    // References
    public HealthBar hbar;
    AudioManagerScript ams;
    private void Start()
    {
        currentHealth = startingHealth;
        hbar.SetMaxHealth(startingHealth);
        ams = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
    }

    public void TakeDamage(int damage)
    {
        //Subtracts current health from the damage that has been taken
        currentHealth -= damage;
        //Updates the healthbar using the SetHealth method
        hbar.SetHealth(currentHealth);
        ams.PlaySFX(ams.explosion);

    if (currentHealth <= 0){
       
    //calls on the endgame function from the game manager script
    FindObjectOfType<GameManagerScript>().EndGame();
}
    
    }
    private void Update()
    {
        //testing for the takedamage function to work
        if (Input.GetKeyDown(KeyCode.L)) {
            TakeDamage(20);
        }
    }

 

}
