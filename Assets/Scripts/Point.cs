using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Point : MonoBehaviour
{
    public float floatingSpeed = 10f;
    public float rotationSpeed = 20f;
    public float distanceThreshold = 0.5f;
    public Vector3 finalDestination = new Vector3(-11, 30, 0);
    
    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        FlyToDestination(finalDestination);
    }

    private void FlyToDestination(Vector3 destination)
    {
        Vector3 currentPosition = transform.position;
        Vector2 floatingDirection = destination - currentPosition;
        rigidbody2D.velocity = floatingDirection * floatingSpeed;

        transform.RotateAround(currentPosition, new Vector3(0, 0, 1), rotationSpeed);

        if (Vector3.Distance(destination, currentPosition) < distanceThreshold)
        {
            Destroy(this.gameObject);
        }
    }
}
