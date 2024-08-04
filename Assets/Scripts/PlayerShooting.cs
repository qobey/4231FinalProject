using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;

    [Header("Settings")]
   
    public float shotCooldown;

    [Header("Throwing")]
    public KeyCode shootKey = KeyCode.Mouse0;
    public float shotForce;
    public float verticalForce;

    bool readyToShoot;
    public AudioManagerScript amScript;



    private void Start()
    {
         amScript = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
        readyToShoot = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(shootKey) && readyToShoot)
        {
            Throw();
        }
    }

    private void Throw()
    {
        amScript.PlaySFX(amScript.playerShot);
        readyToShoot = false;

        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * shotForce + transform.up * verticalForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

      

        // implement shotCooldown
        Invoke(nameof(ResetThrow), shotCooldown);
    }

    private void ResetThrow()
    {
        readyToShoot = true;
    }
}