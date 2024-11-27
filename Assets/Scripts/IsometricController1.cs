using UnityEngine;
using Pathfinding;

public class IsometricController1 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private Vector3 lastPosition;
    public float stopDistance = 0.5f; // Distance from the target to stop

    // Pathfinding components
    private Seeker seeker;
    private Path path;
    private int currentWaypoint = 0;

    // Layer mask for obstacles
    public LayerMask obstacleLayer;

    // Predefined point for auto movement
    public Transform predefinedTarget; // Assign in the Inspector

    private void Awake()
    {
        seeker = GetComponent<Seeker>();

        // Ensure the character is not affected by physics (keep it in the 2D plane)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }
    }

    private void Start()
    {
        // Start automatic movement after 1 second
        if (predefinedTarget != null)
        {
            Invoke(nameof(StartAutoMovement), 1.5f);
        }
    }

    private void Update()
    {
        HandleMouseClick();

        if (isMoving)
        {
            if (path != null && currentWaypoint < path.vectorPath.Count)
            {
                MoveCharacter();
                UpdateAnimation();
            }
            else
            {
                isMoving = false;
                animator.SetBool("isMove", false);
            }
        }
        else
        {
            animator.SetBool("isMove", false);
        }
    }

    void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) // Left click to set target
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; // Ensure 2D movement on the plane

            // Use pathfinding to calculate the path
            seeker.StartPath(transform.position, mousePos, OnPathComplete);
        }
    }

    void StartAutoMovement()
    {
        // Start pathfinding towards the predefined target
        seeker.StartPath(transform.position, predefinedTarget.position, OnPathComplete);
    }

    // Callback when the pathfinding has completed
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
            isMoving = true;
        }
    }

    void MoveCharacter()
    {
        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            isMoving = false;
            return;
        }

        // Move to the next waypoint
        Vector3 targetWaypoint = path.vectorPath[currentWaypoint];
        Vector3 direction = (targetWaypoint - transform.position).normalized;

        // Move towards the target waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, moveSpeed * Time.deltaTime);

        // Check if the character has reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint) < stopDistance)
        {
            currentWaypoint++;
        }
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

// using UnityEngine;
// using Pathfinding;

// public class IsometricController1 : MonoBehaviour
// {
//     public float moveSpeed = 5f;
//     public Animator animator;

//     private Vector3 targetPosition;
//     private bool isMoving = false;
//     private Vector3 lastPosition;
//     public float stopDistance = 0.5f; // Distance from the target to stop

//     // Pathfinding components
//     private Seeker seeker;
//     private Path path;
//     private int currentWaypoint = 0;

//     // Layer mask for obstacles
//     public LayerMask obstacleLayer;

//     // Maximum radius within which we use pathfinding
//     public float pathfindingRadius = 5f;

//     private void Awake()
//     {
//         seeker = GetComponent<Seeker>();
//         // Ensure the character is not affected by physics (keep it in the 2D plane)
//         Rigidbody2D rb = GetComponent<Rigidbody2D>();
//         if (rb)
//         {
//             rb.gravityScale = 0;
//             rb.freezeRotation = true;
//         }
//     }

//     private void Update()
//     {
//         HandleMouseClick();

//         if (isMoving)
//         {
//             if (path != null && currentWaypoint < path.vectorPath.Count)
//             {
//                 // If pathfinding is being used, follow the path
//                 MoveCharacter();
//                 UpdateAnimation();
//             }
//             else
//             {
//                 // If no path, stop movement
//                 isMoving = false;
//                 animator.SetBool("isMove", false);
//             }
//         }
//         else
//         {
//             animator.SetBool("isMove", false);
//         }
//     }

//     void HandleMouseClick()
//     {
//         if (Input.GetMouseButtonDown(0)) // Left click to set target
//         {
//             Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             mousePos.z = 0; // Ensure 2D movement on the plane

//             // Check if the mouse click is within the allowed radius
//             float distance = Vector3.Distance(transform.position, mousePos);
//             if (distance <= pathfindingRadius)
//             {
//                 // Pathfinding is needed, use A* pathfinding
//                 seeker.StartPath(transform.position, mousePos, OnPathComplete);
//             }
//             else
//             {
//                 // Move directly to the target if outside the radius
//                 targetPosition = mousePos;
//                 isMoving = true;
//             }
//         }
//     }

//     // Callback when the pathfinding has completed
//     void OnPathComplete(Path p)
//     {
//         if (!p.error)
//         {
//             path = p;
//             currentWaypoint = 0;
//             isMoving = true;
//         }
//     }

//     void MoveCharacter()
//     {
//         if (path == null) return;

//         if (currentWaypoint >= path.vectorPath.Count)
//         {
//             isMoving = false;
//             return;
//         }

//         // Move to the next waypoint
//         Vector3 targetWaypoint = path.vectorPath[currentWaypoint];
//         Vector3 direction = (targetWaypoint - transform.position).normalized;

//         // Move towards the target waypoint
//         transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, moveSpeed * Time.deltaTime);

//         // Check if the character has reached the waypoint
//         if (Vector3.Distance(transform.position, targetWaypoint) < stopDistance)
//         {
//             currentWaypoint++;
//         }
//     }

//     void UpdateAnimation()
//     {
//         Vector3 movement = transform.position - lastPosition;

//         if (movement != Vector3.zero)
//         {
//             Vector2 movementDirection = new Vector2(movement.x, movement.y).normalized;

//             // Update animation based on movement direction
//             animator.SetFloat("moveX", movementDirection.x);
//             animator.SetFloat("moveY", movementDirection.y);
//             animator.SetBool("isMove", true);
//         }
//         else
//         {
//             animator.SetBool("isMove", false);
//         }

//         lastPosition = transform.position;
//     }
// }





// using UnityEngine;
// using Pathfinding;

// public class IsometricController1 : MonoBehaviour
// {
//     public float moveSpeed = 5f; // Movement speed
//     public Animator animator; // Animator for controlling animations
//     private Vector3 targetPosition; // Target position to move towards
//     private bool isMoving = false; // Is the character moving?

//     // Pathfinding components
//     private Seeker seeker;
//     private Path path;
//     private int currentWaypoint = 0;

//     // Maximum radius within which we use pathfinding
//     public float pathfindingRadius = 5f;

//     // Maximum path length allowed for pathfinding
//     public float maxPathLength = 20f;

//     // Stop distance from the target waypoint
//     public float stopDistance = 0.5f;

//     private void Awake()
//     {
//         seeker = GetComponent<Seeker>();

//         // Ensure the character is not affected by physics (keep it in 2D plane)
//         Rigidbody2D rb = GetComponent<Rigidbody2D>();
//         if (rb)
//         {
//             rb.gravityScale = 0;
//             rb.freezeRotation = true;
//         }
//     }

//     private void Update()
//     {
//         HandleMouseClick();

//         if (isMoving)
//         {
//             if (path != null && currentWaypoint < path.vectorPath.Count)
//             {
//                 // If pathfinding is being used, follow the path
//                 MoveCharacter();
//             }
//             else
//             {
//                 // If no path, stop movement
//                 isMoving = false;
//                 animator.SetBool("isMove", false);
//             }
//         }
//         else
//         {
//             animator.SetBool("isMove", false); // Stop animation if not moving
//         }

//         // Update animation every frame, regardless of movement
//         UpdateAnimation();
//     }

//     void HandleMouseClick()
//     {
//         if (Input.GetMouseButtonDown(0)) // Left click to set target
//         {
//             Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             mousePos.z = 0; // Ensure 2D movement on the plane

//             // Check if the click is within the allowed radius
//             float distance = Vector3.Distance(transform.position, mousePos);

//             if (distance <= pathfindingRadius)
//             {
//                 // If the click is within the radius, start pathfinding
//                 seeker.StartPath(transform.position, mousePos, OnPathComplete);
//             }
//             else
//             {
//                 // Don't move if outside the radius
//                 isMoving = false;
//                 animator.SetBool("isMove", false);
//             }
//         }
//     }

//     // Callback when the pathfinding has completed
//     void OnPathComplete(Path p)
//     {
//         if (!p.error)
//         {
//             // If the path length exceeds the max allowed length, don't start movement
//             if (p.vectorPath.Count > maxPathLength)
//             {
//                 isMoving = false; // Stop movement if the path is too long
//                 animator.SetBool("isMove", false);
//             }
//             else
//             {
//                 path = p;
//                 currentWaypoint = 0;
//                 isMoving = true; // Start moving
//                 animator.SetBool("isMove", true);
//             }
//         }
//     }

//     void MoveCharacter()
//     {
//         if (path == null) return;

//         if (currentWaypoint >= path.vectorPath.Count)
//         {
//             isMoving = false;
//             return;
//         }

//         // Move to the next waypoint
//         Vector3 targetWaypoint = path.vectorPath[currentWaypoint];
//         Vector3 direction = (targetWaypoint - transform.position).normalized;

//         // Smooth the movement towards the target waypoint
//         float distanceToWaypoint = Vector3.Distance(transform.position, targetWaypoint);
//         if (distanceToWaypoint < stopDistance) // Reached waypoint
//         {
//             currentWaypoint++;
//         }

//         // Move the character
//         transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, moveSpeed * Time.deltaTime);
//     }

//     void UpdateAnimation()
//     {
//         // Calculate the movement direction
//         Vector3 movement = transform.position - new Vector3(transform.position.x, transform.position.y, 0);

//         // Update the animator parameters every frame
//         Vector2 movementDirection = new Vector2(movement.x, movement.y).normalized;

//         // Update animator with movement direction
//         animator.SetFloat("moveX", movementDirection.x); // Update moveX
//         animator.SetFloat("moveY", movementDirection.y); // Update moveY

//         // If moving, set isMove to true
//         if (movement.magnitude > 0.1f) // Adjust this value based on what feels right
//         {
//             animator.SetBool("isMove", true); // Start movement animation
//         }
//         else
//         {
//             animator.SetBool("isMove", false); // Stop animation if no movement
//         }
//     }
// }




