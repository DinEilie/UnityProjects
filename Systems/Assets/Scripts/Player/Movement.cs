using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    // Movement triggers
    [Header ("Movement triggers")]
    public bool allowMovement = true;
    public bool allowSprint = true;
    public bool allowJump = true;
    public bool allowFloatability = true;
    public bool allowCrouch = true;
    public bool allowClimb = true;

    // Movement properties
    [Header ("Movement values")]
    [SerializeField] [Range(1,25)] [Tooltip("Player movement speed.")] private float speed = 4.5f;
    [SerializeField] [Range(1,10)] [Tooltip("Player movement speed is multiply by this value while sprinting.")] private float sprintSpeed = 2f;
    [SerializeField] [Range(1,30)] [Tooltip("Player jump height.")] private float jumpHeight = 2.5f;
    [SerializeField] [Tooltip("Refrence to the ''CharacterController''.")] private CharacterController playerController;
    [SerializeField] [Tooltip("Refrence to the ''Camera'' object.")] private GameObject[] cameraObj;
    [SerializeField] [Tooltip("Refrence to the ''Camera'' default position.")] private Transform eyeMarker;
    [SerializeField] [Tooltip("Refrence to the ''Camera'' low position.")] private Transform crouchMarker;
    [SerializeField] [Tooltip("Refrence to the ''Player'' ground checker position.")] private Transform floorMarker;
    [SerializeField] [Tooltip("Ground checker radius.")] private float floorMarkerRadius = 0.2f;
    [SerializeField] [Tooltip("Refrence to the ''Layer'' ground.")] private LayerMask floorMask;
    [SerializeField] [Tooltip("Refrence to the animator controller.")] private Animator animator;
    [SerializeField] [Tooltip("Refrence to the animator controller.")] private Animator animatorFirstPerson;
    [SerializeField] [Tooltip("Refrence to the animator controller.")] private Animator animatorTransparent;
    private float countStep = 0f;
    private float waitMilisec = 0.1f;
    
    // Movement status
    [Header ("Movement status")]
    [SerializeField] private bool grounded;
    [SerializeField] private bool walking;
    [SerializeField] private bool sprinting;
    [SerializeField] private bool crouching;
    [SerializeField] private bool climbing;
    [SerializeField] private float steps;

    // Update is called once per frame
    void Update()
    {
        //Update if the object is grounded
        grounded = Physics.CheckSphere(floorMarker.position, floorMarkerRadius, floorMask);
        if (allowMovement)
            Move();
        if (allowCrouch)
            Crouch();
        if (allowSprint)
            Sprint();
        if (allowJump)
            Jump();
        if (allowFloatability)
            Floatate();
    }

    private void Move(){
        float moveVertically;
        float moveHorizontally;
        if(!climbing){
            if(grounded){
                // Player movement values
                moveVertically = Input.GetAxis("Vertical") * (speed * sprintSpeed) * Time.deltaTime;
                moveHorizontally = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
                animator.SetBool("isGrounded", true);
                animatorFirstPerson.SetBool("isGrounded", true);
                animatorTransparent.SetBool("isGrounded", true);

                // Player is moving vertically or horizontally
                if(moveVertically != 0 || moveHorizontally != 0){
                    if(moveVertically > 0){
                        animator.SetBool("isRunningBackward", false);
                        animatorTransparent.SetBool("isRunningBackward", false);
                    } else {
                        animator.SetBool("isRunningBackward", true);
                        animatorTransparent.SetBool("isRunningBackward", true);
                    }
                    
                    // Update animation and stats
                    if (waitMilisec - Time.deltaTime <= 0)
                        waitMilisec = 0;
                    else
                        waitMilisec -= Time.deltaTime;
                    if (waitMilisec == 0 && sprinting && !gameObject.GetComponent<Shooter>().IsFiring())
                        cameraObj[1].transform.localRotation = Quaternion.Euler(40f, 0f, 0f);
                    else
                        cameraObj[1].transform.localRotation = Quaternion.Euler(8.129001f, 0f, 0f);
                    walking = true;
                    animator.SetBool("isWalking", true);
                    animatorFirstPerson.SetBool("isWalking", true);
                    animatorTransparent.SetBool("isWalking", true);

                    gameObject.GetComponent<Stats>().AddPower(-0.8f * Time.deltaTime);
                } else {
                    // Update animation and stats
                    walking = false;
                    animator.SetBool("isWalking", false);
                    animatorFirstPerson.SetBool("isWalking", false);
                    animatorTransparent.SetBool("isWalking", false);
                    animator.SetBool("isRunningBackward", false);
                    animatorTransparent.SetBool("isRunningBackward", false);
                    cameraObj[1].transform.localRotation = Quaternion.Euler(8.129001f, 0f, 0f);
                    waitMilisec = 0.1f;
                }
            } else {
                // Player movement values while in the air
                moveVertically = Input.GetAxis("Vertical") * ((speed * sprintSpeed) * 0.7f) * Time.deltaTime;
                moveHorizontally = Input.GetAxis("Horizontal") * (speed * 0.9f) * Time.deltaTime;
                animator.SetBool("isGrounded", false);
                animatorFirstPerson.SetBool("isGrounded", false);
                animatorTransparent.SetBool("isGrounded", false);

                // Player is moving vertically or horizontally
                if(moveVertically != 0 || moveHorizontally != 0){
                    // Decrease players power
                    gameObject.GetComponent<Stats>().AddPower(-0.6f * Time.deltaTime);

                    // Update animation and stats
                    if (waitMilisec - Time.deltaTime <= 0)
                        waitMilisec = 0;
                    else
                        waitMilisec -= Time.deltaTime;
                    if (waitMilisec == 0 && sprinting && !gameObject.GetComponent<Shooter>().IsFiring())
                        cameraObj[1].transform.localRotation = Quaternion.Euler(40f, 0f, 0f);
                    else
                        cameraObj[1].transform.localRotation = Quaternion.Euler(8.129001f, 0f, 0f);
                }
            }

            // Move the object
            Vector3 motion = (transform.right * moveHorizontally) + (transform.forward * moveVertically);
            playerController.Move(motion);

            // Decrease players power
            gameObject.GetComponent<Stats>().AddPower(-0.08f * Time.deltaTime);
        } else{
            // Player movement values while climbing
            moveVertically = Input.GetAxis("Vertical") * (speed * 0.3f) * Time.deltaTime;
            moveHorizontally = Input.GetAxis("Horizontal") * (speed * 0.3f) * Time.deltaTime;
            if(moveVertically > 0){
                animator.SetBool("isClimbingUp", true);
                animatorFirstPerson.SetBool("isClimbingUp", true);
                animatorTransparent.SetBool("isClimbingUp", true);
                animator.SetBool("isClimbingDown", false);
                animatorFirstPerson.SetBool("isClimbingDown", false);
                animatorTransparent.SetBool("isClimbingDown", false);
            } else if (moveVertically == 0){
                animator.SetBool("isClimbingUp", false);
                animatorFirstPerson.SetBool("isClimbingUp", false);
                animatorTransparent.SetBool("isClimbingUp", false);
                animator.SetBool("isClimbingDown", false);
                animatorFirstPerson.SetBool("isClimbingDown", false);
                animatorTransparent.SetBool("isClimbingDown", false);
            } else {
                animator.SetBool("isClimbingUp", false);
                animatorFirstPerson.SetBool("isClimbingUp", false);
                animatorTransparent.SetBool("isClimbingUp", false);
                animator.SetBool("isClimbingDown", true);
                animatorFirstPerson.SetBool("isClimbingDown", true);
                animatorTransparent.SetBool("isClimbingDown", true);
            }

            // Player is moving vertically or horizontally
            if(moveVertically > 0 || moveHorizontally > 0){
                // Decrease players power
                gameObject.GetComponent<Stats>().AddPower(-1.6f * Time.deltaTime);
            }

            //Move the object
            Vector3 motion = (transform.right * moveHorizontally) + (transform.up * moveVertically);
            playerController.Move(motion);

            // Decrease players power
            gameObject.GetComponent<Stats>().AddPower(-0.16f * Time.deltaTime);
        }
    }

    private void Crouch(){
        if(grounded && !sprinting){
            if(Input.GetKey(KeyCode.C)){
                // Update animation and stats
                animator.SetBool("isCrouching", true);
                animatorTransparent.SetBool("isCrouching", true);
                gameObject.GetComponent<Stats>().AddPower(-0.4f * Time.deltaTime);
                crouching = true;
                gameObject.GetComponent<CharacterController>().center = Vector3.down * 0.5f;
                gameObject.GetComponent<CharacterController>().height = 1f;
                cameraObj[0].transform.position = crouchMarker.position;
                sprintSpeed = 0.7f;
            } else {
                // Update animation and stats
                animator.SetBool("isCrouching", false);
                animatorTransparent.SetBool("isCrouching", false);
                crouching = false;
                gameObject.GetComponent<CharacterController>().center = Vector3.zero;
                gameObject.GetComponent<CharacterController>().height = 2f;
                cameraObj[0].transform.position = eyeMarker.position;
                cameraObj[1].transform.position = eyeMarker.position;
                sprintSpeed = 1f;
            }
        }
    }

    private void Jump(){
        if(Input.GetButtonDown("Jump") && !IsCrouching()){
            float temp = gameObject.GetComponent<Gravity>().Get();
            if (grounded && sprinting)
                gameObject.GetComponent<Gravity>().SetVelocity(jumpHeight, sprintSpeed);
            else if (grounded)
                gameObject.GetComponent<Gravity>().SetVelocity(jumpHeight, 1f);
        }
    }

    private void Floatate(){
        if (Input.GetKey(KeyCode.F) && !grounded && !gameObject.GetComponent<Stats>().IsJetOverheat() && gameObject.GetComponent<Stats>().GetJet() > 0){
            // Deacrease fuel and increase chance for cooldown
            gameObject.GetComponent<Stats>().AddJet(-5f * Time.deltaTime);
            gameObject.GetComponent<Stats>().AddJetCooldown(1f * Time.deltaTime);

            if(gameObject.GetComponent<Gravity>().GetVelocity() < 0)
            {
                gameObject.GetComponent<Gravity>().AddVelocity();
                gameObject.GetComponent<Stats>().AddJetHeight(-4.5f * Time.deltaTime);
            }  
            else {
                gameObject.GetComponent<Gravity>().enable = false;
                gameObject.GetComponent<Stats>().AddJetHeight(4.5f * Time.deltaTime);
                if(gameObject.GetComponent<Stats>().GetJetHeight() < gameObject.GetComponent<Stats>().GetMaxJetHeight())
                    playerController.Move(transform.up * speed * Time.deltaTime);
            }
        } else {
            gameObject.GetComponent<Gravity>().enable = true;
            gameObject.GetComponent<Stats>().AddJetHeight(-4.5f * Time.deltaTime);
            gameObject.GetComponent<Stats>().AddJetCooldown(-1f * Time.deltaTime);
        }
            
    }

    private void Sprint(){
        if (walking &&  Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") >= 0 && grounded && !crouching){
            // Update animation and stats
            animator.SetBool("isSprinting", true);
            animatorFirstPerson.SetBool("isSprinting", true);
            animatorTransparent.SetBool("isSprinting", true);
            sprintSpeed = 2f;
            sprinting = true;
            gameObject.GetComponent<Stats>().AddPower(-0.8f * Time.deltaTime);
        }
        else if (walking && sprinting && Input.GetAxis("Vertical") >= 0 && !grounded) {
            sprintSpeed = 2f;
        } else if (!crouching) {
            // Update animation and stats
            animator.SetBool("isSprinting", false);
            animatorFirstPerson.SetBool("isSprinting", false);
            animatorTransparent.SetBool("isSprinting", false);
            sprintSpeed = 1f;
            sprinting = false;
        }
    }

    public bool IsGrounded(){
        if(grounded)
            return true;
        return false;
    }

    public bool IsMoving(){
        if(walking)
            return true;
        return false;
    }
    
    public bool IsSprinting(){
        if(sprinting)
            return true;
        return false;
    }

    public bool IsCrouching(){
        if(crouching)
            return true;
        return false;
    }

    public bool IsClimbing(){
        if(climbing)
            return true;
        return false;
    }

    public void EnableClimbing(){
            Debug.Log("Now entering climbing mode");
            gameObject.GetComponent<Gravity>().enable = false;
            animator.SetBool("isClimbingUp", false);
            animatorFirstPerson.SetBool("isClimbingUp", false);
            animatorTransparent.SetBool("isClimbingUp", false);
            animator.SetBool("isClimbingDown", false);
            animatorFirstPerson.SetBool("isClimbingDown", false);
            animatorTransparent.SetBool("isClimbingDown", false);
            animator.SetBool("isClimbing", true);
            animatorFirstPerson.SetBool("isClimbing", true);
            animatorTransparent.SetBool("isClimbing", true);
            allowSprint = false;
            allowCrouch = false;
            allowJump = false;
            allowFloatability = false;
            climbing = true;
    }

    public void DisableClimbing(){
            Debug.Log("Now leaving climbing mode");
            animator.SetBool("isClimbing", false);
            animatorFirstPerson.SetBool("isClimbing", false);
            animatorTransparent.SetBool("isClimbing", false);
            animator.SetBool("isClimbingUp", false);
            animatorFirstPerson.SetBool("isClimbingUp", false);
            animatorTransparent.SetBool("isClimbingUp", false);
            animator.SetBool("isClimbingDown", false);
            animatorFirstPerson.SetBool("isClimbingDown", false);
            animatorTransparent.SetBool("isClimbingDown", false);
            gameObject.GetComponent<Gravity>().enable = true;
            allowSprint = true;
            allowCrouch = true;
            allowJump = true;
            allowFloatability = true;
            climbing = false;
    }
}
