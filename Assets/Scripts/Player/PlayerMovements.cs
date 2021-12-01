using UnityEngine;

public class PlayerMovements : MonoBehaviour {
	private Rigidbody2D body;
	Animator anim;
	private bool onGround = false;
	[SerializeField] private float speed;

	public float knockbackCount;
	public float knockbackLength;

	private void Awake() {
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

			// flip character left right
			if (horizontalInput < -0.01f)
			{
				// flip left
				transform.localScale = new Vector3(-charScaleX, charScaleY, charScaleZ);
			}
			else if (horizontalInput > 0.01f)
			{
				// flip right
				transform.localScale = new Vector3(charScaleX, charScaleY, charScaleZ);
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
			onGround = true;
    }

	private void Jump() {
		body.velocity = new Vector2(body.velocity.x, speed * 1.5f);
		onGround = false;
	}
}
