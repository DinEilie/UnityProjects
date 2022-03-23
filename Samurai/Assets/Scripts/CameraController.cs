using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{ 
	public GameObject WhoToFollow;
    public GameObject WhoNotToFollow;
	private Vector3 offset;
    private Vector3 NewCamPos;
    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
        offset = transform.position - WhoToFollow.transform.position;
    }
	
	void LateUpdate()
	{
        if(WhoNotToFollow.transform.position.y < WhoToFollow.transform.position.y + offset.y)
		    transform.position = WhoToFollow.transform.position + offset;
        else
            transform.position = new Vector3(WhoToFollow.transform.position.x + offset.x, transform.position.y, transform.position.z); 
	}
}
