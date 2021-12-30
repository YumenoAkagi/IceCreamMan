using System;
using UnityEngine;

public class PlayerMovements : MonoBehaviour {
	private Rigidbody2D body;
	Animator anim;
	private bool onGround = false;
	[SerializeField] private float speed;

	public float knockbackCount;
	public float knockbackLength;

	private AudioSource walkSFX, jumpSFX;

	public static GameObject instance;

	bool facingRight = true;

	private void Awake() {
		DontDestroyOnLoad(this);

		if(instance == null)
        {
			instance = gameObject;
        } else
        {
			Destroy(gameObject);
			return;
        }

		body = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	private void Update() {
		// get character scaling for movement adjustments
		float charScaleX = Mathf.Abs(transform.localScale.x);
		float charScaleY = transform.localScale.y;
		float charScaleZ = transform.localScale.z;

		if(knockbackCount <= 0)
        {
			// character left and right movement settings
			float horizontalInput = Input.GetAxis("Horizontal");
			body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
			anim.SetFloat("velocity", Mathf.Abs(horizontalInput));

			if (horizontalInput < -0.01f || horizontalInput > 0.01f)
				WalkSFX(true);
			else
				WalkSFX(false);
			

			// flip character left right
			if (horizontalInput < -0.01f)
			{
				if(facingRight)
                {
					// flip left
					transform.Rotate(0f, 180f, 0f);
					facingRight = false;
				}
			}
			else if (horizontalInput > 0.01f)
			{
				if(!facingRight)
                {
					// flip right
					transform.Rotate(0f, 180f, 0f);
					facingRight = true;
				}
				
			}

			if (Input.GetKey(KeyCode.UpArrow) && onGround)
			{
				// character jump movement settings
				Jump();
			}
		} else
        {
			knockbackCount -= Time.deltaTime;
        }
		
	}

    private void WalkSFX(bool isWalking)
    {
		if(walkSFX == null)
        {
			if (FindObjectOfType<AudioManager>() == null)
				return;

			var audio = Array.Find(FindObjectOfType<AudioManager>().SFXAudios, x => x.name == "Player - Walk");
			if (audio == null)
				return;

			walkSFX = audio;
		}

		if (isWalking && !walkSFX.isPlaying)
			walkSFX.Play();
		else if(!isWalking && walkSFX.isPlaying)
			walkSFX.Stop();
    }

	private void JumpSFX()
	{
		if (jumpSFX == null)
		{
			if (FindObjectOfType<AudioManager>() == null)
				return;

			var audio = Array.Find(FindObjectOfType<AudioManager>().SFXAudios, x => x.name == "Player - Jump");
			if (audio == null)
				return;

			jumpSFX = audio;
		}

		if (!jumpSFX.isPlaying)
			jumpSFX.Play();
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Chest")
			onGround = true;
    }

	private void Jump() {
		body.velocity = new Vector2(body.velocity.x, speed * 1.5f);
		onGround = false;
		JumpSFX();
	}
}
