using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Animator anim;
    public PlayerMovementAdvanced pm;

    public KeyCode atk = KeyCode.R;
   
    public bool isAttacking = false;

    public bool canAttack;
    public float attackCooldown;
    


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
       
        pm = GameObject.FindObjectOfType<PlayerMovementAdvanced>();
   
        attackCooldown = .5f;
        canAttack = true;
        
    }

    void Movement()
    {
    if(pm.state == PlayerMovementAdvanced.MovementState.walking && pm.horizontalInput !=0 || pm.verticalInput != 0) 
        {
            anim.SetBool("isMoving", true);
            anim.SetBool("isSprinting", false);
        }
    if (pm.horizontalInput == 0 && pm.verticalInput == 0) 
    {
        anim.SetBool("isMoving", false);
    }
    if (pm.state == PlayerMovementAdvanced.MovementState.sprinting) 
    {
        anim.SetBool("isSprinting", true);
    }



    }
    void Air()
    {
        if (!pm.grounded) 
        {
            anim.SetBool("inAir", true);
        } 
        else 
        anim.SetBool("inAir", false);

 
        


    }

    void Attack() 
    {
        if(Input.GetKeyDown(atk) && canAttack == true) 
        {
            canAttack = false;
            isAttacking = true;
            // anim.SetBool("isAttacking", true);
            Invoke(nameof(AttackReset), attackCooldown);
            anim.SetTrigger("Attack");
            
        }
        
    }
    void AttackReset()
    {
        canAttack = true;
        isAttacking = false;

    }

    

    // Update is called once per frame
    void Update()
    {

Air();
Movement();
Attack();
 

    }
}

