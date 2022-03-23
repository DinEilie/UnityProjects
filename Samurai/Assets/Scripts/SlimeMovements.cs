using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovements : MonoBehaviour
{
	public Transform pos1, pos2;
	public float speed;
	Vector3 nextPos;
    // Start is called before the first frame update
    void Start()
    {
        nextPos = pos2.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == pos1.position){
			nextPos = pos2.position;
			Vector3 Scaler = transform.localScale;
			Scaler.x *= -1;
			transform.localScale = Scaler;
		}
		if(transform.position == pos2.position){
			nextPos = pos1.position;
			Vector3 Scaler = transform.localScale;
			Scaler.x *= -1;
			transform.localScale = Scaler;
		}
		transform.position = Vector3.MoveTowards(transform.position,nextPos,speed*Time.deltaTime);
    }
}
