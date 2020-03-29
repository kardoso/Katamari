using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject ball;
    private Vector3 lookAtOffset;

    void Start()
    {
        lookAtOffset = new Vector3(0, 1.5f, 0);
    }

    void Update()
    {
        var ballPos = ball.transform.position;
        transform.LookAt(ballPos + lookAtOffset);
    }

    void FixedUpdate()
    {
        var ballPos = ball.transform.position;
        transform.position = new Vector3(ballPos.x, ballPos.y + 4, ballPos.z - 4);
    }
}
