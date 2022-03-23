using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIbar : MonoBehaviour
{
	//Health parameters
	public float maxHealth;
	public float currentHealth;
	public Image[] hearts;
	public Sprite emptyHeart;
	public Sprite halfHeart;
	public Sprite fullHeart;
	
	//Keys parameters
	public Image[] keys;
	public Sprite blueKeySprite;
	public Sprite greenKeySprite;
	public Sprite redKeySprite;
	public Sprite yellowKeySprite;
	public Sprite blueKeyEmptySprite;
	public Sprite greenKeyEmptySprite;
	public Sprite redKeyEmptySprite;
	public Sprite yellowKeyEmptySprite;
	public bool blueKey;
	public bool greenKey;
	public bool redKey;
	public bool yellowKey;
	
	//Coins
	public float coinsCounter;
	public Image[] coins;
	
	//Text and Numbers
	public Image[] numbers;
	
	
    // Update is called once per frame
    void Update()
    {
		//Updating Heart UI
		if(currentHealth / (int)currentHealth == 1 && currentHealth > 0){
			for (int i = 0; i < (int)currentHealth; i++){
				hearts[i].sprite = fullHeart;
			}
			for (int i = (int)currentHealth; i < (int)maxHealth; i++){
				hearts[i].sprite = emptyHeart;
			}
		} else if(currentHealth / (int)currentHealth != 1 && currentHealth > 0){
			if(currentHealth < 1){
				hearts[0].sprite = halfHeart;
				for (int i = 1; i < (int)maxHealth; i++){
					hearts[i].sprite = emptyHeart;
				}
			} else{
				int lastHalf = (int)currentHealth;
				for (int i = 0; i < (int)currentHealth; i++){
					hearts[i].sprite = fullHeart;
				}
				hearts[lastHalf].sprite = halfHeart;
				for (int i = 1 + lastHalf; i < (int)maxHealth; i++){
					hearts[i].sprite = emptyHeart;
				}
			}
		} else if(currentHealth <= 0){
			//Call for death
			for (int i = 0; i < (int)maxHealth; i++){
				hearts[i].sprite = emptyHeart;
				gameObject.GetComponent<RedPlayer>().isDead = true;
			}
		}
		
		//Updating Keys UI
		if(blueKey == true){
			keys[0].sprite = blueKeySprite;
		} else{
			keys[0].sprite = blueKeyEmptySprite;
		}
        if(greenKey == true){
			keys[1].sprite = greenKeySprite;
		} else{
			keys[1].sprite = greenKeyEmptySprite;
		}
        if(redKey == true){
			keys[2].sprite = redKeySprite;
		} else{
			keys[2].sprite = redKeyEmptySprite;
		}
        if(yellowKey == true){
			keys[3].sprite = yellowKeySprite;
		} else{
			keys[3].sprite = yellowKeyEmptySprite;
		}
		
		//Updating Coins UI
		if(coinsCounter >= 100 && coinsCounter <= 999){
			int digit0 = (int)(coinsCounter/100);
			int digit1 = (int)((coinsCounter - (digit0 * 100))/10);
			int digit2 = ((int)coinsCounter - ((digit0 * 100) + (digit1 * 10)));
			coins[0].enabled = true;
			coins[1].enabled = true;
			for (int i = 1; i <= 9; i++){
				if(i == digit0)
					coins[0].sprite = numbers[i].sprite;
			}
			for (int i = 0; i <= 9; i++){
				if(i == digit1)
					coins[1].sprite = numbers[i].sprite;
			}
			for (int i = 0; i <= 9; i++){
				if(i == digit2)
					coins[2].sprite = numbers[i].sprite;
			}
		} else if(coinsCounter >= 10 && coinsCounter <= 99){
			int digit1 = (int)(coinsCounter/10);
			int digit2 = ((int)coinsCounter - (digit1 * 10));
			coins[0].enabled = false;
			coins[1].enabled = true;
			for (int i = 0; i <= 9; i++){
				if(i == digit1)
					coins[1].sprite = numbers[i].sprite;
			}
			for (int i = 0; i <= 9; i++){
				if(i == digit2)
					coins[2].sprite = numbers[i].sprite;
			}
		} else{
			coins[0].enabled = false;
			coins[1].enabled = false;
			for (int i = 0; i <= 9; i++){
				if(i == (int)coinsCounter)
					coins[2].sprite = numbers[i].sprite;
			}
		}
    }
}
