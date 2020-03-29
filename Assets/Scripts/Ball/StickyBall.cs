using UnityEngine;

public class StickyBall : MonoBehaviour
{
	float facingAngle = 0;
	float x = 0;
	float z = 0;
	public Vector2 facingDirectionVector;
	float size = 1;

	CameraController camera;

	void Start() {
		camera = FindObjectOfType<CameraController>();
	}

	void Update ()
	{
		UpdateInput ();
	}

	void FixedUpdate () {
		Move();
	}

	void Move ()
	{
		transform.GetComponent<Rigidbody> ().AddForce (
			new Vector3 (
				facingDirectionVector.x,
				0,
				facingDirectionVector.y
			) * z * 2
		);
	}

	void UpdateInput ()
	{
		x = Input.GetAxis ("Horizontal") * Time.deltaTime * -100;
		z = Input.GetAxis ("Vertical") * Time.deltaTime * 500;

		facingAngle += x;
		facingDirectionVector = new Vector2 (
			Mathf.Cos (facingAngle * Mathf.Deg2Rad),
			Mathf.Sin (facingAngle * Mathf.Deg2Rad)
		);
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Sticky"))
        {
            //Grow the ball
            transform.localScale += new Vector3(.01f, .01f, .01f);
            size += .01f;
			camera.AddDistanceFromBall(.08f);
			other.enabled = false;

			other.transform.SetParent(this.transform);
        }
    }
}