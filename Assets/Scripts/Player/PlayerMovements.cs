using System;
using UnityEngine;

public class PlayerMovements : MonoBehaviour {
	private static string NEW_GAME = "NewGame";

	private Rigidbody2D body;
	Animator anim;
	private bool onGround = false;
	[SerializeField] private float speed;

	public float knockbackCount;
	public float knockbackLength;

	public AudioSource walkSFX, jumpSFX;

	public static GameObject instance;

	bool facingRight = true;

	private void Awake() {
		if (PlayerPrefs.GetInt(NEW_GAME) == 1)
			instance = null;

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
            {
				if(!walkSFX.isPlaying)
					walkSFX.Play();
			}
			else
            {
				if (walkSFX.isPlaying)
					walkSFX.Stop();
			}
				


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

	private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Chest")
        {
			onGround = true;
			anim.SetBool("isJump", false);
		}
    }

	private void Jump() {
		body.velocity = new Vector2(body.velocity.x, speed * 1.5f);
		onGround = false;
		anim.SetBool("isJump", true);
		jumpSFX.Play();
	}
}
