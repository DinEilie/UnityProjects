using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Shooter settings
    
    [SerializeField] private Transform shootingPosition;
    [SerializeField] private GameObject hitObject;
    [SerializeField] private float recoilSpeed = 0.5f; 
    [SerializeField] private GameObject cursorShoot;
    [SerializeField] private Camera camera;
    [SerializeField] private Camera cameraFirstPerson;
    [SerializeField] private GameObject flashlight;
    [SerializeField] [Tooltip("Refrence to the animator controller.")] private Animator animator;
    [SerializeField] [Tooltip("Refrence to the animator controller.")] private Animator animatorTransparent;
    [SerializeField] [Tooltip("Refrence to the animator controller.")] private Animator animatorFirstPerson;
    [SerializeField] private float recoilTimer = 0.5f;
    [SerializeField] private bool isDrawn = true;
    [SerializeField] private bool isAiming = false;
    [SerializeField] private bool isFiring = false;
    [SerializeField] private bool isLight = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cursorShoot.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the recoil speed
        if(recoilTimer > 0f)
            recoilTimer -= Time.deltaTime;
        else {
            recoilTimer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.T)){
            if (isLight){
                flashlight.SetActive(false);
                isLight = false;
            }
            else
            {
                flashlight.SetActive(true);
                isLight = true;
            }     
        }
            

        if (Input.GetButton("Fire2") && !gameObject.GetComponent<Movement>().IsClimbing()){
            cursorShoot.SetActive(true);
            isAiming = true;
            if (!isDrawn)
            {
                animator.SetBool("isDrawn", true);
                animatorFirstPerson.SetBool("isDrawn", true);
                animatorTransparent.SetBool("isDrawn", true);
                isDrawn = true;
            }

            if (camera.fieldOfView - 60f * Time.deltaTime <= 70f)
            {
                camera.fieldOfView = 70f;
                cameraFirstPerson.fieldOfView = 70f;
            }
            else
            {
                camera.fieldOfView -= 60f * Time.deltaTime;
                cameraFirstPerson.fieldOfView -= 60f * Time.deltaTime;
            }
                

            // Shoot a bullet
            if(Input.GetButton("Fire1") && recoilTimer == 0f){
                RaycastHit hit;
                isFiring = true;
                animator.SetBool("isFiring", true);
                animatorFirstPerson.SetBool("isFiring", true);
                animatorTransparent.SetBool("isFiring", true);
                recoilTimer = recoilSpeed;
                if(Physics.Raycast(shootingPosition.position, shootingPosition.TransformDirection(Vector3.forward), out hit, 1000f)){
                    Debug.DrawRay(shootingPosition.position, shootingPosition.TransformDirection(Vector3.forward) * hit.distance, Color.red, 1, false);
                    GameObject bullet = GameObject.Instantiate(hitObject, hit.point, shootingPosition.rotation);
                    Debug.Log("Hit!");
                    GameObject.Destroy(bullet, 0.5f);
                }
            }
        } else {
            cursorShoot.SetActive(false);
            isAiming = false;
            if (camera.fieldOfView + 60f * Time.deltaTime >= 85f)
            {
                camera.fieldOfView = 85f;
                cameraFirstPerson.fieldOfView = 85f;
            }
            else
            {
                camera.fieldOfView += 60f * Time.deltaTime;
                cameraFirstPerson.fieldOfView += 60f * Time.deltaTime;
            }  
        }

        if(Input.GetKeyDown(KeyCode.Q) && !gameObject.GetComponent<Movement>().IsClimbing())
        {    
            if(isDrawn)
            {
                animator.SetBool("isDrawn", false);
                animatorFirstPerson.SetBool("isDrawn", false);
                animatorTransparent.SetBool("isDrawn", false);
                isDrawn = false;
            } 
            else
            {
                animator.SetBool("isDrawn", true);
                animatorFirstPerson.SetBool("isDrawn", true);
                animatorTransparent.SetBool("isDrawn", true);
                isDrawn = true;
            }
                
        }
    }

    public bool IsAiming(){
        if (isAiming)
            return true;
        return false;
    }

    public bool IsFiring(){
        if (isFiring)
            return true;
        return false;
    }

    public void SetRecoilSpeed(float var){
        recoilSpeed = var;
    }

    public void SetIsFiring(bool var){
        isFiring = var;
    }
}
