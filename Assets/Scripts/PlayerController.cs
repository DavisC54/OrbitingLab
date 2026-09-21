using UnityEngine;

/// <summary>
/// Left and right movement only, clamped to the camera's edges.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;

    // To keep the player in the camera, without this part of the sprite would go off the screen
    [SerializeField] private float edgePadding = 0.5f;

    private float lockedY;
    private float minX;
    private float maxX;

    private void Awake()
    {
        lockedY = transform.position.y;
    }

    private void Start()
    {
        Camera cam = Camera.main;

        // ortho size is half the height of the camera view
        float halfWidth = cam.orthographicSize * cam.aspect;
        float centerX = cam.transform.position.x;
        
        // in start becuase the cameera does not move
        minX = centerX - halfWidth + edgePadding;
        maxX = centerX + halfWidth - edgePadding;
    }

    private void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");

        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x + input * moveSpeed * Time.deltaTime, minX, maxX);
        position.y = lockedY; // stays locked even if the player is pushed out of their y

        transform.position = position;
    }
}