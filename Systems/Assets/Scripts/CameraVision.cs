using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVision : MonoBehaviour
{
    [Header ("Camera triggers")]
    public bool allowCameraVision = true;
    public bool allowYInvertion = false;
    public bool allowXInvertion = false;

    [Header ("Camera values")]
    [SerializeField] [Range(20f,600f)] private float cameraSensitivity = 50f;
    private float cameraLeftRight;
    private float cameraUpDown;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (allowCameraVision){
            //Camera rotation values
            cameraLeftRight = Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
            cameraUpDown -= Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
            cameraUpDown = Mathf.Clamp(cameraUpDown, -90f, 75f);
        
            //Rotate the object
            if (allowYInvertion){
                if (allowXInvertion){
                    transform.localRotation = Quaternion.Euler(-1f * cameraUpDown, 0f, 0f);
                    player.Rotate(Vector3.up * cameraLeftRight * -1f);
                } else {
                    transform.localRotation = Quaternion.Euler(-1f * cameraUpDown, 0f, 0f);
                    player.Rotate(Vector3.up * cameraLeftRight);
                }
                
            } else {
                if (allowXInvertion){
                    transform.localRotation = Quaternion.Euler(cameraUpDown, 0f, 0f);
                    player.Rotate(Vector3.up * cameraLeftRight * -1f);
                } else {
                    transform.localRotation = Quaternion.Euler(cameraUpDown, 0f, 0f);
                    player.Rotate(Vector3.up * cameraLeftRight);
                }
            }
        }
    }
}
