using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

	public float speed = 10, jumpVelocity = 10;
	public LayerMask playerMask;
	Transform myTrans, tagGround, tagLeft, tagRight;
	BoxCollider2D myBody;
	CircleCollider2D seePlayer;

	public GameObject bulletToRight, bulletToLeft;
	Vector2 bulletPos;
	public float fireRate = 0.5f;
	float nextFire = 0.0f;
	private Vector3 _target;
	private Animator playerAnimator;
	public bool active;
	public bool canSee = false;

	bool isGrounded = false, isLeftSide = false, isRightSide = false, jumping = false, dashIsCalled = false;
	public bool facingRight = false, facingLeft = false;

	// Use this for initialization
	void Start()
	{
		myBody = GetComponent<BoxCollider2D>();
		seePlayer = GetComponent<CircleCollider2D>();
		playerAnimator = GetComponent<Animator>();
		myTrans = this.transform;
	}

	void Update()
	{
		active = FlipFlop (120);
		canSee = lineofsight();

		if (active == true && canSee == true) {
			Fire();
		}
	}

	// Update is called once per frame

	void Fire()
	{
		var shoot = 100;

		while (shoot > 0) {
			shoot--;
		}
		bulletPos = transform.position;

	}



	void OnCollisionEnter2D(Collision2D other)
	{
		
	}

	bool lineofsight(){
		bool canSee = seePlayer.CompareTag("player");

		if (canSee == true) {
			return true;
		} else
			return false;
		
	}

	bool FlipFlop(int maxTimer){
		var timer = maxTimer;
		var active = false;

		while (timer > 0) {
			timer--;
		}
		if (timer <= 0) {
			if (active == true) {
				active = false;
				timer = maxTimer;
			} else if (active == false) {
				active = true;
				timer = maxTimer;
			}
		} else
			timer--;
		return active;
	}
}