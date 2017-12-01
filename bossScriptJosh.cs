using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bossScriptJosh : MonoBehaviour {

	public LayerMask enemyMask;
	Rigidbody2D myBody;
	Transform myTrans;
	Vector2 bulletPos;
	private Animator enemy3Animator;
	float myWidth, myHeight, speed = 13, startingHealth = 300, currentHealth, flashingCounter = 0, flashingLength = .5f;
	public float stateTimer = 0f, jumpVelocity = 10;
	public int statePicker = 0;
	public GameObject EnemyBulletLeft;
	public GameObject EnemyBulletRight;
	bool isBlocked, isGrounded;
	Vector2 lineCastPost;
	bool damaged, isDead = false, invuln = false, flashingActive = false;
	SpriteRenderer bossSprite;

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		enemy3Animator = GetComponent<Animator>();
		stateTimer = 100f;
		myTrans = this.transform;
		myBody = GetComponent<Rigidbody2D>();
		SpriteRenderer enemySprite = this.GetComponent<SpriteRenderer>();
		myWidth = enemySprite.bounds.extents.x;
		myHeight = enemySprite.bounds.extents.y / 2;
		lineCastPost = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight / 2;
		//Check to see if enemy is touching ground
		isGrounded = Physics2D.Linecast(lineCastPost, lineCastPost + Vector2.down * 3f, enemyMask);
		//Check to see if enemy is blocked by wall
		isBlocked = Physics2D.Linecast(lineCastPost, lineCastPost - myTrans.right.toVector2() * .3f, enemyMask);
		bossSprite = GetComponent<SpriteRenderer>();
	
	}

	void Update(){

		if (flashingActive) {

			if (flashingCounter > flashingLength * .66f) {

				bossSprite.color = new Color (bossSprite.color.r, bossSprite.color.g, bossSprite.color.b, 0f);

			} else if (flashingCounter > flashingLength * .33f) {
				bossSprite.color = new Color (bossSprite.color.r, bossSprite.color.g, bossSprite.color.b, 1f);
			}	else if (flashingCounter > 0) {
				bossSprite.color = new Color (bossSprite.color.r, bossSprite.color.g, bossSprite.color.b, 0f);
			} else {

				bossSprite.color = new Color(bossSprite.color.r, bossSprite.color.g, bossSprite.color.b, 1f);
				flashingActive = false;

			}

			flashingCounter -= .09f;

		}

		stateTimer -= 1;

		enemy3Animator.SetBool ("isJumping", false);
		enemy3Animator.SetBool ("isDashing", false);
		enemy3Animator.SetBool ("isShooting", false);
		if (stateTimer <= 0) {

			statePicker = Random.Range (0, 4);

			switch (statePicker) {

			case 1: //dash
				enemy3Animator.SetBool ("isShooting", false);
				enemy3Animator.SetBool ("isJumping", false);
				enemy3Animator.SetBool ("isDashing", true);
					Move ();
					stateTimer = 100f;
					break;
			case 2: //shoot
				enemy3Animator.SetBool ("isShooting", true);
				Fire ();
				Fire ();
				Fire ();
				Fire ();
				stateTimer = 100f;

				break;

			case 3: //Jump
				
				enemy3Animator.SetBool ("isDashing", false);
				enemy3Animator.SetBool ("isShooting", false);
				enemy3Animator.SetBool ("isJumping", true);

				Jump();
				stateTimer = 100f;
				break;

			default: //back to idle
				stateTimer = 100f;
				break;


			}

		}

		if (statePicker == 2) {
			enemy3Animator.SetBool ("isShooting", true);
			//if not touching ground turn around
			if (!isGrounded || isBlocked) {
				//eulerAngles is the x,y,z for the object
				Vector3 currRot = myTrans.eulerAngles;
				//must use y so that the linecast moves to the correct side of the sprite
				currRot.y += 180;
				//reassign the object so it always faces the correct direction
				myTrans.eulerAngles = currRot;

			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Tracking position
		lineCastPost = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight / 2;

		//Check to see if enemy is touching ground
		isGrounded = Physics2D.Linecast(lineCastPost, lineCastPost + Vector2.down * 3f, enemyMask);
		//Check to see if enemy is blocked by wall
		isBlocked = Physics2D.Linecast(lineCastPost, lineCastPost - myTrans.right.toVector2() * .3f, enemyMask);





	}




	void Fire()
	{

		bulletPos = transform.position;
		bulletPos -= new Vector2(4, Random.Range (0, 5));
			Instantiate(EnemyBulletLeft, bulletPos, Quaternion.identity);

		bulletPos += new Vector2(7, Random.Range (0, 5));
			Instantiate(EnemyBulletRight, bulletPos, Quaternion.identity);
			
		enemy3Animator.SetBool ("isShooting", false);

	}

	public void Jump()
	{
		

		var direction = Random.Range (0, 2);

		if (direction == 0) {
			Vector2 moveVel = myBody.velocity;

			myBody.velocity += jumpVelocity * Vector2.up;
			myBody.velocity += jumpVelocity * Vector2.left;


		}
		else {
			Vector2 moveVel = myBody.velocity;

			myBody.velocity += jumpVelocity * Vector2.up;
			myBody.velocity += jumpVelocity * Vector2.right;


		}




	}

	public void Move()
	{
		

		//if not touching ground turn around
		if (!isGrounded || isBlocked) {
			//eulerAngles is the x,y,z for the object
			Vector3 currRot = myTrans.eulerAngles;
			//must use y so that the linecast moves to the correct side of the sprite
			currRot.y += 180;
			//reassign the object so it always faces the correct direction
			myTrans.eulerAngles = currRot;
		}

		//Always move forward
		Vector2 myVel = myBody.velocity;
		//- is move left and + is move right
		myVel.x = -myTrans.right.x * speed;
		myBody.velocity = myVel;

			
	}
		

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("bullet")) {
			TakeDamage(10);

		}
			}
			public void TakeDamage (float amount)
			{
				if (!invuln && !isDead)
				{
					soundManager.PlaySound ("enemyHit");		
					currentHealth -= amount;
					flashingActive = true;
					flashingCounter = flashingLength;
					invuln = Timer(120);
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
			soundManager.PlaySound ("enemyDie");
			isDead = true;
				Destroy(gameObject);
		Application.LoadLevel (0);
			}


}



