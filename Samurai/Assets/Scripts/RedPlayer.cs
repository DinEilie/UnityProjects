using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedPlayer : MonoBehaviour
{
    private Rigidbody2D rb2d;
	public float Speed;
	public float jumpForce;
	private bool facingRight = true;
	private bool isGrounded;
	public Transform groundCheck;
	public float checkRadius;
	public LayerMask whatIsGround;
	public LayerMask whatIsLava;
	private int extraJumps;
	public int extraJumpsValue;
	public bool isDead;
	private bool isStageComplete;
	public Animator animator;
	public GameObject Menu;
	public GameObject winMenu;
	private int countToDeathMenu;
	public float knockback;
	public float knockbackLength;
	public float knockbackCount;
	public bool knockFromRight;
	private bool knockedOnce = false;
	public GameObject[] backGrounds;
	
    // Start is called before the first frame update
    void Start()
    {
		countToDeathMenu = 3;
		extraJumps = extraJumpsValue;
        rb2d = GetComponent<Rigidbody2D>();
		gameObject.GetComponent<AudioMgr>().Start();
		//Access another script in the same game object -> gameObject.GetComponent<UIbar>().currentHealth = 3;
		//Access another script in another game object -> GameObject.Find("NameOfTheOtherObject").GetComponent<UIbar>().currentHealth = 3;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		//Check for ground and death
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
		if(isDead != true){
			isDead = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsLava);
		}
		
		//Fix jumping animations
		if (isGrounded == true && isDead != true){
			animationsBreak();
		}
		else if(isGrounded == false && isDead != true){
			if(knockbackCount <= 0){
				if(animator.GetBool("ISJUMP")!= true){
					ABjump();
					animator.SetBool("ISJUMP", true);
				}
				else {
					if (extraJumps == 0 && animator.GetBool("doDoubleJump")!= true){
						animator.SetBool("doDoubleJump", true);
					}
				}
			} else if(isDead != true){
				if(animator.GetBool("ISKNOCKED") != true){
					ABknock();
					animator.SetBool("ISKNOCKED", true);
				}
			}
		}
		
		//Move Horizontally
		float moveHorizontal = Input.GetAxis ("Horizontal");
		if(knockbackCount <= 0){
			knockedOnce = false;
			if (moveHorizontal != 0 && isGrounded == true && isDead != true){
				rb2d.velocity = new Vector2(moveHorizontal * Speed, rb2d.velocity.y);
				ABwalk();
				animator.SetBool("ISWALK", true);
				if (facingRight == false && moveHorizontal > 0)
					FlipSprite();
				else if (facingRight == true && moveHorizontal < 0)
					FlipSprite();
			}
			else if(moveHorizontal != 0 && isGrounded == false && isDead != true){
				rb2d.velocity = new Vector2(moveHorizontal * Speed, rb2d.velocity.y);
				ABdoubleJump();
				if (facingRight == false && moveHorizontal > 0)
					FlipSprite();
				else if (facingRight == true && moveHorizontal < 0)
					FlipSprite();
			} else if (moveHorizontal == 0 && isDead != true){
				rb2d.velocity = new Vector2(moveHorizontal * Speed, rb2d.velocity.y);
				ABdoubleJump();
				if (facingRight == false && moveHorizontal > 0)
					FlipSprite();
				else if (facingRight == true && moveHorizontal < 0)
					FlipSprite();
			}
		} else {
			if(knockFromRight && isDead != true){
				rb2d.velocity = new Vector2(knockback, knockback);
				if(animator.GetBool("ISKNOCKED") != true){
					ABknock();
					animator.SetBool("ISKNOCKED", true);
				}
			}
			if(!knockFromRight && isDead != true){
				rb2d.velocity = new Vector2(-knockback, knockback);
				if(animator.GetBool("ISKNOCKED") != true){
					ABknock();
					animator.SetBool("ISKNOCKED", true);
				}
			}
			knockbackCount -= Time.deltaTime;
		}
			
		//Death
		if(isDead == true){
			rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
			ABdead();
			animator.SetBool("ISDEAD", true);
			if(Time.time / (int)Time.time == 1 &&  countToDeathMenu > 0){
				Time.timeScale = 0.78f;
				countToDeathMenu--;
				gameObject.GetComponent<AudioMgr>().playSound(1);
				if(countToDeathMenu == 0){
					Time.timeScale = 0;
					Menu.SetActive(true);
				}
			}
		}
    }
	
	void Update(){
		//Check for death
		if(isDead != true){
			isDead = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsLava);
		}
		
		//Fix jumping animations
		if (isGrounded == true && isDead != true){
			extraJumps = extraJumpsValue;
			animationsBreak();
		}
		else if (isGrounded == false && isDead != true){
			if(knockbackCount <= 0){
				if(animator.GetBool("ISJUMP")!= true){
					ABjump();
					animator.SetBool("ISJUMP", true);
				}
				else {
					if (extraJumps == 0 && animator.GetBool("doDoubleJump")!= true){
						animator.SetBool("doDoubleJump", true);
					}
				}
			} else if(isDead != true){
				ABknock();
			}
		}
		
		if(knockbackCount <= 0){
			//Activate jump or double jump
			if(Input.GetKeyDown("space") && extraJumps > 0 && isDead != true){
				rb2d.velocity = Vector2.up * jumpForce;
				extraJumps--;
			} else if(Input.GetKeyDown("space") && extraJumps == 0 && isGrounded == true && isDead != true){
				rb2d.velocity = Vector2.up * jumpForce;
			}
		}
		
		//Death
		if(isDead == true){
			rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
			ABdead();
			animator.SetBool("ISDEAD", true);
			if(Time.time / (int)Time.time == 1 &&  countToDeathMenu > 0){
				Time.timeScale = 0.78f;
				countToDeathMenu--;
				gameObject.GetComponent<AudioMgr>().playSound(1);
				if(countToDeathMenu == 0){
					Time.timeScale = 0;
					Menu.SetActive(true);
				}
			}
		}
	}
	
	//Flip the object to the other side
	void FlipSprite(){
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
	}
	
	//Interactive objects
	void OnTriggerEnter2D(Collider2D other){
		//Coin
		if(other.gameObject.CompareTag ("coin")){
			other.gameObject.SetActive(false);
			if (gameObject.GetComponent<UIbar>().coinsCounter < 999){
				gameObject.GetComponent<UIbar>().coinsCounter++;
			}
		}
		
		//heart
		if(other.gameObject.CompareTag ("heart")){
			if (gameObject.GetComponent<UIbar>().currentHealth<= 2){
				gameObject.GetComponent<UIbar>().currentHealth++;
				gameObject.GetComponent<AudioMgr>().playSound(2);				
				other.gameObject.SetActive(false);				
			} else if(gameObject.GetComponent<UIbar>().currentHealth <3) {
				gameObject.GetComponent<UIbar>().currentHealth=3;
				gameObject.GetComponent<AudioMgr>().playSound(2);
				other.gameObject.SetActive(false);
			}
		}
		
		//CoinGold
		if(other.gameObject.CompareTag ("coinGold")){
			other.gameObject.SetActive(false);
			if (gameObject.GetComponent<UIbar>().coinsCounter + 5 <= 999){
				gameObject.GetComponent<UIbar>().coinsCounter += 5;
			} else {
				gameObject.GetComponent<UIbar>().coinsCounter = 999;
			}
		}
		
		//Shift to Lava Background
		if(other.gameObject.CompareTag ("shiftBackground")){
			backGrounds[0].gameObject.SetActive(false);
			backGrounds[1].gameObject.SetActive(true);
			gameObject.GetComponent<AudioMgr>().switchBackgroundMusic();
		}
		
		//Blue Key
		if(other.gameObject.CompareTag ("blueKey")){
			gameObject.GetComponent<UIbar>().blueKey = true;
			gameObject.GetComponent<AudioMgr>().playSound(7);
			other.gameObject.SetActive(false);

		}

		//Red Key
		if (other.gameObject.CompareTag ("redKey")){
			gameObject.GetComponent<UIbar>().redKey = true;
			gameObject.GetComponent<AudioMgr>().playSound(7);
			other.gameObject.SetActive(false);

		}

		//Green Key
		if (other.gameObject.CompareTag ("greenKey")){
			gameObject.GetComponent<UIbar>().greenKey = true;
			gameObject.GetComponent<AudioMgr>().playSound(7);
			other.gameObject.SetActive(false);

		}

		//Yellow Key
		if (other.gameObject.CompareTag ("yellowKey")){
			gameObject.GetComponent<UIbar>().yellowKey = true;
			gameObject.GetComponent<AudioMgr>().playSound(7);
			other.gameObject.SetActive(false);

		}

		//Weak Foe
		if (other.gameObject.CompareTag ("foeW")){
			if(isDead != true){
				if(animator.GetBool("ISKNOCKED") != true){
					animator.SetBool("ISKNOCKED", true);
				}
				if (knockedOnce != true){
					gameObject.GetComponent<UIbar>().currentHealth -= 0.5f;
					gameObject.GetComponent<AudioMgr>().playSound(5);
					knockedOnce = true;
				}
			}
			knockbackCount = knockbackLength;
			if(other.gameObject.transform.position.x < transform.position.x)
				knockFromRight = true;
			else
				knockFromRight = false;
		}
		
		//Adept Foe
		if(other.gameObject.CompareTag ("foeM")){
			if(isDead != true){
				if(animator.GetBool("ISKNOCKED") != true){
					animator.SetBool("ISKNOCKED", true);
				}
				if (knockedOnce != true){
					gameObject.GetComponent<UIbar>().currentHealth -= 1f;
					gameObject.GetComponent<AudioMgr>().playSound(5);
					knockedOnce = true;
				}
			}
			knockbackCount = knockbackLength;
			if(other.gameObject.transform.position.x < transform.position.x)
				knockFromRight = true;
			else
				knockFromRight = false;
		}
		
		//Hard Foe
		if(other.gameObject.CompareTag ("foeH")){
			if(isDead != true){
				if(animator.GetBool("ISKNOCKED") != true){
					animator.SetBool("ISKNOCKED", true);
				}
				if (knockedOnce != true){
					gameObject.GetComponent<UIbar>().currentHealth -= 1.5f;
					gameObject.GetComponent<AudioMgr>().playSound(5);
					knockedOnce = true;
				}
			}
			knockbackCount = knockbackLength;
			if(other.gameObject.transform.position.x < transform.position.x)
				knockFromRight = true;
			else
				knockFromRight = false;
		}
	}
	
	//Interactive Doors & Enemies
	void OnCollisionEnter2D(Collision2D other)
    {
        //Blue Door
		if(other.gameObject.CompareTag ("blueDoor")){
			if(gameObject.GetComponent<UIbar>().blueKey == true){
				other.gameObject.SetActive(false);
			}
		}
		
		//Red Door
		if(other.gameObject.CompareTag ("redDoor")){
			if(gameObject.GetComponent<UIbar>().redKey == true){
				other.gameObject.SetActive(false);
			}
		}
		
		//Green Door
		if(other.gameObject.CompareTag ("greenDoor")){
			if(gameObject.GetComponent<UIbar>().greenKey == true){
				other.gameObject.SetActive(false);
			}
		}
		
		//Yellow Door
		if(other.gameObject.CompareTag ("yellowDoor")){
			if(gameObject.GetComponent<UIbar>().yellowKey == true){
				other.gameObject.SetActive(false);
				Time.timeScale = 0;
				winMenu.SetActive(true);
			}
		}
    }
	
	void animationsBreak(){
		animator.SetBool("ISKNOCKED", false);
		animator.SetBool("doDoubleJump", false);
		animator.SetBool("ISJUMP", false);
		animator.SetBool("ISWALK", false);
		animator.SetBool("ISDEAD", false);
		
	}
	
	void ABknock(){
		animator.SetBool("doDoubleJump", false);
		animator.SetBool("ISJUMP", false);
		animator.SetBool("ISWALK", false);
		animator.SetBool("ISDEAD", false);
		
	}
	
	void ABdoubleJump(){
		animator.SetBool("ISKNOCKED", false);
		animator.SetBool("ISWALK", false);
		animator.SetBool("ISDEAD", false);
		
	}
	
	void ABwalk(){
		animator.SetBool("ISKNOCKED", false);
		animator.SetBool("doDoubleJump", false);
		animator.SetBool("ISJUMP", false);
		animator.SetBool("ISDEAD", false);
		
	}
	
	void ABjump(){
		animator.SetBool("ISKNOCKED", false);
		animator.SetBool("doDoubleJump", false);
		animator.SetBool("ISWALK", false);
		animator.SetBool("ISDEAD", false);
		
	}
	
	void ABdead(){
		animator.SetBool("ISKNOCKED", false);
		animator.SetBool("doDoubleJump", false);
		animator.SetBool("ISJUMP", false);
		animator.SetBool("ISWALK", false);
		
	}
}
