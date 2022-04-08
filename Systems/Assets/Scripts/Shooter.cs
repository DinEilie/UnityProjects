using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Shooter settings
    [SerializeField] private Transform shootingPosition;
    [SerializeField] private GameObject hitObject;
    [SerializeField] private const float recoilSpeed = 0.1f; 
    private float recoilTimer = recoilSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the recoil speed
        if(recoilTimer > 0f)
            recoilTimer -= Time.deltaTime;
        else
            recoilTimer = 0f;
        
        // Shoot a bullet
        if(Input.GetButton("Fire1") && recoilTimer == 0f){
            RaycastHit hit;
            if(Physics.Raycast(shootingPosition.position, shootingPosition.TransformDirection(Vector3.forward), out hit, 1000f)){
                Debug.DrawRay(shootingPosition.position, shootingPosition.TransformDirection(Vector3.forward) * hit.distance, Color.red, 1, false);
                GameObject bullet = GameObject.Instantiate(hitObject, hit.point, shootingPosition.rotation);
                Debug.Log("Hit!");
                recoilTimer = recoilSpeed;
                GameObject.Destroy(bullet, 0.5f);
            }
        }
    }
}
