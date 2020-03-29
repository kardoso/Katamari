using UnityEngine;

public class CameraController : MonoBehaviour
{
    private StickyBall ball;
    private Vector3 lookAtOffset;
    private float distanceFromBall = 5;
    private float size = 1;

    void Start()
    {
        lookAtOffset = new Vector3(0, 1.5f, 0);
        ball = FindObjectOfType<StickyBall>();
    }

    void Update()
    {
        var ballPos = ball.transform.position;
        transform.LookAt(ballPos + lookAtOffset);
    }
    void FixedUpdate()
    {
        transform.position = new Vector3(
            -ball.facingDirectionVector.x * distanceFromBall,
            distanceFromBall,
            -ball.facingDirectionVector.y * distanceFromBall
        ) + ball.transform.position;
    }

    public void AddDistanceFromBall(float distance)
    {
        distanceFromBall += distance;
    }
}
