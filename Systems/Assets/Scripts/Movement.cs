using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Movement triggers
    [Header ("Movement triggers")]
    public bool allowMovement = true;
    public bool allowSprint = true;
    public bool allowJump = true;
    public bool allowFloatability = true;
    public bool allowGravity = true;
    public bool allowCrouch = true;
    public bool allowClimb = true;

    // Movement properties
    [Header ("Movement values")]
    [SerializeField] [Range(1,25)] [Tooltip("Player movement speed.")] private float speed = 4.5f;
    [SerializeField] [Range(1,10)] [Tooltip("Player movement speed is multiply by this value while sprinting.")] private float sprintSpeed = 2f;
    [SerializeField] [Range(1,30)] private float jumpHeight = 2.5f;
    private const float gravity = -9.8f;
    private float tempGravity = gravity;
    private float velocity;
    [SerializeField] private CharacterController playerController;
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private Transform eyeMarker;
    [SerializeField] private Transform crouchMarker;
    [SerializeField] private Transform floorMarker;
    [SerializeField] private float floorMarkerRadius = 0.2f;
    [SerializeField] private LayerMask floorMask;
    private float countStep = 0f;
    
    // Movement status
    [Header ("Movement status")]
    [SerializeField] private bool grounded;
    [SerializeField] private bool walking;
    [SerializeField] private bool sprinting;
    [SerializeField] private bool crouching;
    [SerializeField] private bool climbing;
    [SerializeField] private float steps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update if the object is grounded
        grounded = Physics.CheckSphere(floorMarker.position, floorMarkerRadius, floorMask);
        if(allowClimb)
            Climb();
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
        if (allowGravity)
            Gravitate();
    }

    private void Climb(){
        if(climbing){
            allowGravity = false;
            allowSprint = false;
            allowCrouch = false;
            allowJump = false;
            allowFloatability = false;
        }
    }

    private void Move(){
        float moveVertically;
        float moveHorizontally;
        if(!climbing){
            if(grounded){
            //Player movement values
                moveVertically = Input.GetAxis("Vertical") * (speed * sprintSpeed) * Time.deltaTime;
                moveHorizontally = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            } else {
                //Player movement values while in the air
                moveVertically = Input.GetAxis("Vertical") * ((speed * sprintSpeed) * 0.7f) * Time.deltaTime;
                moveHorizontally = Input.GetAxis("Horizontal") * (speed * 0.9f) * Time.deltaTime;
            }
             //Move the object
            Vector3 motion = (transform.right * moveHorizontally) + (transform.forward * moveVertically);
            if (moveVertically != 0)
                walking = true;
            else 
                walking = false;
            playerController.Move(motion);
        } else{
            moveVertically = Input.GetAxis("Vertical") * (speed * 0.3f) * Time.deltaTime;
            moveHorizontally = Input.GetAxis("Horizontal") * (speed * 0.3f) * Time.deltaTime;
            //Move the object
            Vector3 motion = (transform.right * moveHorizontally) + (transform.up * moveVertically);
            playerController.Move(motion);
        }
    }

    private void Crouch(){
        if(grounded && !sprinting){
            if(Input.GetKey(KeyCode.LeftControl)){
                crouching = true;
                gameObject.GetComponent<CharacterController>().center = Vector3.down * 0.5f;
                gameObject.GetComponent<CharacterController>().height = 1f;
                cameraObj.transform.position = crouchMarker.position;
                sprintSpeed = 0.7f;
            } else {
                crouching = false;
                gameObject.GetComponent<CharacterController>().center = Vector3.zero;
                gameObject.GetComponent<CharacterController>().height = 2f;
                cameraObj.transform.position = eyeMarker.position;
                sprintSpeed = 1f;
            }
        }
    }

    private void Jump(){
        if(Input.GetButtonDown("Jump")){
            if (grounded && sprinting)
                velocity = Mathf.Sqrt((jumpHeight * sprintSpeed) * tempGravity * -2f);
            else if (grounded)
                velocity = Mathf.Sqrt(jumpHeight * tempGravity * -2f);
        }
    }

    private void Floatate(){
        if (Input.GetKey(KeyCode.F) && !grounded)
            tempGravity = -3f;
        else   
            tempGravity = gravity;
    }

    private void Gravitate(){
        velocity += tempGravity * Time.deltaTime * 1.2f;
        if (grounded && velocity < 0)
            velocity = -2f;
        Vector3 motion = transform.up * velocity;
        playerController.Move(motion * Time.deltaTime);
    }

    private void Sprint(){
        if (walking &&  Input.GetKey(KeyCode.LeftShift) && grounded && !crouching){
            sprintSpeed = 2f;
            sprinting = true;
        }
        else if (walking && sprinting && !grounded) {
            sprintSpeed = 2f;
        } else if (!crouching) {
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

    public void TriggerClimbing(){
        if(!climbing){
            Debug.Log("Now entering climbing mode");
            climbing = true;
        } else{
            Debug.Log("Now leaving climbing mode");
            climbing = false; 
            allowGravity = true;
            allowSprint = true;
            allowCrouch = true;
            allowJump = true;
            allowFloatability = true;
        }
    }
}
