using UnityEngine;

public class IsometricController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private Vector3 lastPosition;
    public LayerMask obstacleLayer;
    public TestMusicManager musicManager;
    public float stopDistance = 0.5f; // Distance from colliders to stop

    private void Awake()
    {
        musicManager.SetMusicState(TestMusicManager.MusicState.FreeRoam);
    }

    private void Update()
    {
        HandleMouseClick();

        if (isMoving)
        {
            MoveCharacter();
            UpdateAnimation();
        }
        else
        {
            animator.SetBool("isMove", false);
        }
    }

    void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; // Ensure 2D movement
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (mousePos - transform.position).normalized, Vector3.Distance(transform.position, mousePos), obstacleLayer);

            if (hit.collider != null)
            {
                // Calculate target position with buffer distance from the collider
                Vector3 hitPoint = hit.point - hit.normal * stopDistance;
                if (Vector3.Distance(transform.position, hitPoint) > stopDistance)
                {
                    targetPosition = hitPoint; // Move if the point is far enough
                    isMoving = true;
                }
            }
            else
            {
                targetPosition = mousePos; // Move freely if there's no obstacle
                isMoving = true;
            }
        }
    }

    void MoveCharacter()
{
    // Calculate the direction to move and the distance to the target
    Vector3 directionToMove = (targetPosition - transform.position).normalized;
    float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

    // Check for obstacles in the direction of movement
    RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToMove, distanceToTarget, obstacleLayer);

    if (hit.collider != null)
    {
        // If there's an obstacle, calculate the adjusted stopping point
        float distanceToObstacle = hit.distance;

        // Stop movement if within the stop distance from the obstacle
        if (distanceToObstacle <= stopDistance)
        {
            isMoving = false;
            return;
        }

        // Adjust the target position to stop at the buffer distance from the obstacle
        Vector3 hitPoint = (Vector3)hit.point - (Vector3)hit.normal * stopDistance;
        targetPosition = hitPoint;
    }

    // Stop movement if close enough to the target position
    if (distanceToTarget < 0.1f)
    {
        isMoving = false;
        return;
    }

    // Move the player towards the adjusted target position
    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
}


    void UpdateAnimation()
    {
        Vector3 movement = transform.position - lastPosition;

        if (movement != Vector3.zero)
        {
            Vector2 movementDirection = new Vector2(movement.x, movement.y).normalized;

            // Update animation based on movement direction
            animator.SetFloat("moveX", movementDirection.x);
            animator.SetFloat("moveY", movementDirection.y);
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }

        lastPosition = transform.position;
    }
}
