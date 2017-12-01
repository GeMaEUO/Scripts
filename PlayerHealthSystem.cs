using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealthSystem : MonoBehaviour {

	public float startingHealth = 100, currentHealth, flashingCounter = 0, flashingLength = .67f;
	GameObject PlayerMovement; 
	bool damaged, isDead = false, invuln = false, flashingActive = false;

	private SpriteRenderer playerSprite;


	// Use this for initialization
	void Start () {

		playerSprite = GetComponent<SpriteRenderer>();
		currentHealth = startingHealth;

	}
	
	// Update is called once per frame
	void Update () {

		if (flashingActive) {

			if (flashingCounter > flashingLength * .66f) {

				playerSprite.color = new Color (playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);

			} else if (flashingCounter > flashingLength * .33f) {
				playerSprite.color = new Color (playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
			}	else if (flashingCounter > 0) {
				playerSprite.color = new Color (playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
			} else {

				playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
				flashingActive = false;

			}

			flashingCounter -= .09f;

		}
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{

		
		if (other.gameObject.CompareTag ("enemy1")) {
			TakeDamage(10);
	
		}
		if (other.gameObject.CompareTag ("enemy3")) {
			TakeDamage(10);
		
		}
		if (other.gameObject.CompareTag("enemy4")){
			TakeDamage(10);
		
		}
		if (other.gameObject.CompareTag("boss")){
			TakeDamage(10);
		
		}
		if (other.gameObject.CompareTag("enemyBullet")){
			TakeDamage(10);

		}
	}

	public void TakeDamage (float amount)
	{
		if (!invuln && !isDead)
		{
			soundManager.PlaySound ("playerHit");
			currentHealth -= amount;
			flashingActive = true;
			flashingCounter = flashingLength;
			invuln = Timer(220);
		}
			

		if (currentHealth <= 0 && !isDead)
		{
			Death();
		}

	}

	private bool Timer (int maxTimer)
	{
		var timer = maxTimer;
		var active = false;
		while (timer >= 0)
		{
			timer--;

		}
		if (timer > 0)
		{
			active = true;

		}
		else
		{
			active = false;
		
		}
		return active;
	}

	void Death ()
	{
		soundManager.PlaySound ("playerHit");
		isDead = true;
		Destroy(gameObject);
		Application.LoadLevel (0);
	}
}
