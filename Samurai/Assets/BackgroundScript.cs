using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
	public Sprite normal;
	public Sprite lava;
	private SpriteRenderer spriteRenderer;
	
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void shiftToLava(){
		spriteRenderer.sprite = lava;
	}
	
	public void shiftToNormal(){
		spriteRenderer.sprite = normal;
	}
}
