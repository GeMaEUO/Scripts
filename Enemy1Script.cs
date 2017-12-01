using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Script : MonoBehaviour {

	public LayerMask enemyMask;
	public float speed, startingHealth = 100, currentHealth, flashingCounter = 0, flashingLength = .5f;
	Rigidbody2D myBody;
	SpriteRenderer enemy1Sprite;
	Transform myTrans;
	float myWidth, myHeight;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f,0f,0f,.1f);
	bool damaged, isDead = false, invuln = false, flashingActive = false;



	void Start () {

		myTrans = this.transform;
		myBody = this.GetComponent<Rigidbody2D>();
		SpriteRenderer enemySprite = this.GetComponent<SpriteRenderer>();
		myWidth = enemySprite.bounds.extents.x;
		myHeight = enemySprite.bounds.extents.y / 2;
		currentHealth = startingHealth;
		enemy1Sprite = GetComponent<SpriteRenderer>();
	}

	void Update(){

		if (flashingActive) {

			if (flashingCounter > flashingLength * .66f) {

				enemy1Sprite.color = new Color (enemy1Sprite.color.r, enemy1Sprite.color.g, enemy1Sprite.color.b, 0f);

			} else if (flashingCounter > flashingLength * .33f) {
				enemy1Sprite.color = new Color (enemy1Sprite.color.r, enemy1Sprite.color.g, enemy1Sprite.color.b, 1f);
			}	else if (flashingCounter > 0) {
				enemy1Sprite.color = new Color (enemy1Sprite.color.r, enemy1Sprite.color.g, enemy1Sprite.color.b, 0f);
			} else {

				enemy1Sprite.color = new Color(enemy1Sprite.color.r, enemy1Sprite.color.g, enemy1Sprite.color.b, 1f);
				flashingActive = false;

			}

			flashingCounter -= .09f;

		}
	}

	

	void FixedUpdate () {


		//Tracking position
		Vector2 lineCastPost = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight / 8;

		//This will show that ^ line of code is in the right position
		//Debug.DrawLine (lineCastPost, lineCastPost + Vector2.down);

		//Check to see if enemy is touching ground
		bool isGrounded = Physics2D.Linecast(lineCastPost, lineCastPost + Vector2.down * 3f, enemyMask);
		//Check to see if enemy is blocked by wall
		bool isBlocked = Physics2D.Linecast(lineCastPost, lineCastPost - myTrans.right.toVector2() * .3f, enemyMask);

		//This will show the collision line for hitting walls 
		Debug.DrawLine (lineCastPost, lineCastPost - myTrans.right.toVector2() * .3f);

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


        invuln = Timer(0);
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            TakeDamage(25);
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
	}

    private bool FlipFlop(int maxTimer)
    {
        var timer = maxTimer;
        var active = false;

        while (timer > 0)
        {
            timer--;
        }
        if (timer <= 0)
        {
            if (active == true)
            {
                active = false;
                timer = maxTimer;
            }
            else if (active == false)
            {
                active = true;
                timer = maxTimer;
            }
        }
        else
            timer--;
        return active;
    }


}
