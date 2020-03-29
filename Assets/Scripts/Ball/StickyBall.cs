using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StickyBall : MonoBehaviour
{
	float facingAngle = 0;
	float x = 0;
	float z = 0;
	public Vector2 facingDirectionVector;
	private Rigidbody body;
	float maxSpeed = 8;
	float size = 1;

	CameraController mainCam;
	
    public GameObject category1;
    bool category1Unlocked = false;
    public GameObject category2;
    bool category2Unlocked = false;
    public GameObject category3;
    bool category3Unlocked = false;

	public TMP_Text sizeUI;

	public AudioClip pickupSound;

	void Start() {
		mainCam = FindObjectOfType<CameraController>();
		body = GetComponent<Rigidbody>();
	}

	void Update ()
	{
		UpdateInput ();
	}

	void FixedUpdate () {
		Move();
		UnlockPickupCategory();
	}

	void Move ()
	{
		body.AddForce (
			new Vector3 (
				facingDirectionVector.x,
				0,
				facingDirectionVector.y
			) * z * 2
		);
		body.velocity = Vector3.ClampMagnitude(body.velocity, maxSpeed);
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

	void UnlockPickupCategory()
	{
		if(!category1Unlocked)
		{
			if(size >= 1)
			{
				category1Unlocked = true;
				for(int i = 0; i < category1.transform.childCount; i++)
				{
					category1.transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
				}
			}
		}
		if(!category2Unlocked)
		{
			if(size >= 1.5f)
			{
				category2Unlocked = true;
				for(int i = 0; i < category2.transform.childCount; i++)
				{
					category2.transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
				}
			}
		}
		if(!category3Unlocked)
		{
			if(size >= 2)
			{
				category3Unlocked = true;
				for(int i = 0; i < category3.transform.childCount; i++)
				{
					category3.transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
				}
			}
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Sticky"))
        {
            //Grow the ball
            transform.localScale += new Vector3(.01f, .01f, .01f);
            size += .01f;
			maxSpeed += 0.1f;
			mainCam.AddDistanceFromBall(.08f);
			other.enabled = false;

			other.transform.SetParent(this.transform);

			sizeUI.GetComponent<TMP_Text>().text = "Mass: " + Math.Round(size, 2);

			FindObjectOfType<AudioSource>().PlayOneShot(pickupSound);
        }
    }
}