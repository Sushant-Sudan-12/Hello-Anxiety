using UnityEngine;

public class IsometricController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private Vector3 lastPosition;

    void Update()
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
            mousePos.z = 0;

            targetPosition = mousePos;
            isMoving = true;
        }
    }

    void MoveCharacter()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }
    }

    void UpdateAnimation()
    {
        Vector3 movement = transform.position - lastPosition;

        if (movement != Vector3.zero)
        {
            Vector2 movementDirection = new Vector2(movement.x, movement.y).normalized;

            animator.SetFloat("moveX", movementDirection.x);
            animator.SetFloat("moveY", movementDirection.y);
            animator.SetBool("isMove", true);

            Debug.Log($"Movement: X={movementDirection.x}, Y={movementDirection.y}");
        }
        else
        {
            animator.SetBool("isMove", false);
        }

        lastPosition = transform.position;
    }
}

