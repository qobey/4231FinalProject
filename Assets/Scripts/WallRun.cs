using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    //potential key button for wall running
    KeyCode wallRun = KeyCode.LeftAlt;
    KeyCode jumpKey = KeyCode.Space;
    //wall running & detection variables
    [Header("Wallrunning Detection Variables")]
    public LayerMask wallLayerID;
    public LayerMask groundLayerID;
    public float wallRunForce;
    public float maxTime;
    private float wallRunTimer;
    public float wallJumpUpForce;
    public float wallJumpSideForce;
    //
    public float wallDistance;
    public float minHeight;
    private RaycastHit leftWallHit; 
    private RaycastHit rightWallHit; 
    public bool leftWall;
    public bool rightWall;
    //exiting wall variables
    private bool exitingWall;
    public float exitWallTime;
   
    private float exitWallTimer;

    //References to external objects
    [Header("External References")]
    public Transform orientation;
    private PlayerMovementAdvanced playerMove;
    private Rigidbody rb;
    // Input Objects
    private float hInput;
    private float vInput;
    

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMove = GetComponent<PlayerMovementAdvanced>();
    }
    private void Update()
    {
        WallCheck();
        StateHandler();

    }
    void FixedUpdate()
    {
        if (playerMove.wallrunning) {
            WallRun();
        }
    }


    //this function uses raycasting to detect if there is a wall to the left or right of the player
    private void WallCheck()
    {
        rightWall = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance, wallLayerID);
        leftWall = Physics.Raycast(transform.position, -orientation.right, out rightWallHit, wallDistance, wallLayerID);

    }
    //a boolean function that will return true if there is no ground below the player.  Used for wall running
    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minHeight, groundLayerID);
    }
    private void StateHandler()
    {
        //get inputs
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        //Wall running state
        //if the player is touching a wall to their left or right, is holding forward, and is above the ground, they will run on the wall
        if((leftWall || rightWall) && vInput > 0 && AboveGround() && !exitingWall)
        {
            if(!playerMove.wallrunning) {
                StartWallRun();
            }
            //wall run timer
            if(wallRunTimer > 0)
            wallRunTimer -= Time.deltaTime;
            if(wallRunTimer <= 0 && playerMove.wallrunning) 
            {
                exitingWall = true;
            }
            // call wall jump
            if(Input.GetKeyDown(jumpKey)) 
            WallJump();
        }
        else if (exitingWall)
        {
            if(playerMove.wallrunning) 
            EndWallRun();
            if(exitWallTimer > 0) 
            exitWallTimer -= Time.deltaTime;
            if(exitWallTimer <= 0)
            exitingWall = false;
        }
        //None
        else {
            if(playerMove.wallrunning) 
            EndWallRun();
        }
    }
    //both start and end are just functions for controlling the wallrunning boolean
    private void StartWallRun()
    {
        playerMove.wallrunning = true;
        wallRunTimer = maxTime;
    }
    
    private void WallRun()
    {
       
        //temporarily disable gravity
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //tracking the variable for forward no matter the wall's orientation
        Vector3 wallNormal = rightWall ? rightWallHit.normal : leftWallHit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        //reverse wallforward vector
        if((orientation.forward - wallForward).magnitude > (orientation.forward - - wallForward).magnitude)
        wallForward = -wallForward;

        //apply forward force when on the wall
        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);
        //alternative for if the wall is curved on the outside

    }
    private void EndWallRun()
    {
        playerMove.wallrunning = false;
    }
    private void WallJump() 
    {
        exitingWall = true;
        exitWallTimer = exitWallTime;
    Vector3 wallNormal = rightWall ? rightWallHit.normal : leftWallHit.normal;
    Vector3 jumpForce = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;
    //reset y velocity, then add the force needed for the jump
    rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    rb.AddForce(jumpForce, ForceMode.Impulse);
    }
}
