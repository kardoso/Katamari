using UnityEngine;

public class StickyBall : MonoBehaviour
{
	float facingAngle = 0;
	float x = 0;
	float z = 0;
	public Vector2 facingDirectionVector;

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
}