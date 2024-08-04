using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    AudioManagerScript audioManager;
    public Transform orientation;
    public Transform playerCamera;
    private Rigidbody rb;
    private PlayerMovementAdvanced pmove;

    //dashing variables
    public float dashForce;
    public float dashUpForce;

    public float dashLength;

    public float dashCooldown;
    private float cooldownTimer;

    public KeyCode dashButton = KeyCode.E;

    public AudioManagerScript amScript;

    



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pmove = GetComponent<PlayerMovementAdvanced>();
        amScript = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
    }

    private void Dash()
    {
        if ( cooldownTimer > 0) return;
        else cooldownTimer = dashCooldown;

        pmove.dashing = true;

        //Creates the force that will be added to the rigidbody
        
        Vector3 forceForDash = orientation.forward * dashForce + orientation.up * dashUpForce;

        //Calls on the AddForce function from the rigidbody in impulse mode so it only adds the force in that instance
        rb.AddForce(forceForDash, ForceMode.Impulse);
        //audio manager script plays the sound
        amScript.PlaySFX(amScript.dashSnd);
        
        //set gravity back to true after force has been applied
        rb.useGravity = true;
        //will reset the dash at a specific point in time
        Invoke(nameof(ResetDash), dashLength);

    }
    private void ResetDash()
    {
        pmove.dashing = false;
    }
    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(dashButton) && !pmove.grounded) 
        {
            Dash();
        }
        if (cooldownTimer > 0) 
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}
