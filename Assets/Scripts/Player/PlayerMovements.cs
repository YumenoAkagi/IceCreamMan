using UnityEngine;

public class PlayerMovements : MonoBehaviour {
	private Rigidbody2D body;
	private bool onGround = false;
	[SerializeField] private float speed;

	private void Awake() {
		body = GetComponent<Rigidbody2D> ();
	}

	private void Update() {
		// get character scaling for movement adjustments
		float charScaleX = Mathf.Abs(transform.localScale.x);
		float charScaleY = transform.localScale.y;
		float charScaleZ = transform.localScale.z;

		// character left and right movement settings
		float horizontalInput = Input.GetAxis("Horizontal");
		body.velocity = new Vector2 (horizontalInput * speed, body.velocity.y);

        // flip character left right
        if (horizontalInput < -0.01f)
        {
			// flip left
			transform.localScale = new Vector3(-charScaleX, charScaleY, charScaleZ);
        } else if(horizontalInput > 0.01f)
        {
			// flip right
			transform.localScale = new Vector3(charScaleX, charScaleY, charScaleZ);
        }

        if (Input.GetKey (KeyCode.UpArrow) && onGround) {
			// character jump movement settings
			Jump();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "Platform")
			onGround = true;
    }

	private void Jump() {
		body.velocity = new Vector2(body.velocity.x, speed);
		onGround = false;
	}
}
