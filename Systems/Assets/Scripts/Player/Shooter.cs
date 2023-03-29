﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Shooter settings
    
    [SerializeField] private Transform shootingPosition;
    [SerializeField] private GameObject hitObject;
    [SerializeField] private float recoilSpeed = 0.5f; 
    [SerializeField] private GameObject cursorShoot;
    [SerializeField] [Tooltip("Refrence to the animator controller.")] private Animator animator;
    [SerializeField] [Tooltip("Refrence to the animator controller.")] private Animator animatorTransparent;
    [SerializeField] [Tooltip("Refrence to the animator controller.")] private Animator animatorFirstPerson;
    [SerializeField] private float recoilTimer = 0.5f;
    private bool isDrawn = false;
    private bool isAiming = false;
    private bool isFiring = false;

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
            

        if (Input.GetButton("Fire2") && !gameObject.GetComponent<Movement>().IsClimbing()){
            cursorShoot.SetActive(true);
            isAiming = true;
            if(!isDrawn){
                animator.SetBool("isDrawn", true);
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
        }

        if(Input.GetKeyDown(KeyCode.Q) && !gameObject.GetComponent<Movement>().IsClimbing()){
            
            if(isDrawn){
                animator.SetBool("isDrawn", false);
                animatorFirstPerson.SetBool("isDrawn", false);
                isDrawn = false;
            } 
            else{
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