using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float minYPosition = -4.5f;
    public Rigidbody2D rb;

    private Vector3 targetPosition;
    private bool isMoving = false;
    public Transform childTransform;
    public GameObject lights;
    private float timer = 0f;
    private float flickerInterval = 0.45f;
    private int i = 0;
    private bool lighton;
    public Animator anim;

    private void Update()
    {
        HandleMouseClick();

        if (isMoving)
        {
            MoveToTarget();
            UpdateAnimation();
        }
        else
        {
            anim.SetBool("isMove", false);
        }

        HandleLightFlicker();
    }

    void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            if (CheckMouseClickPosition(mousePosition))
            {
                targetPosition = mousePosition;
                isMoving = true;
                anim.SetBool("isMove", true);
            }
        }
    }

    bool CheckMouseClickPosition(Vector3 mousePosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        return hit.collider != null;
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(
            Mathf.MoveTowards(transform.position.x, targetPosition.x, moveSpeed * Time.deltaTime),
            Mathf.Max(transform.position.y, minYPosition),
            0
        );

        float scaleFactor = 1 - (transform.position.y + 4) / 13;
        scaleFactor = Mathf.Clamp(scaleFactor, 0.1f, 1f);

        float scaleX = 0.55f * scaleFactor;
        float scaleY = 0.55f * scaleFactor;

        childTransform.localScale = new Vector3(scaleX, scaleY, transform.localScale.z);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            anim.SetBool("isMove", false);
        }
    }

    void HandleLightFlicker()
    {
        timer += Time.deltaTime;
        if (transform.position.y > -3)
        {
            lighton = true;
        }
        if (lighton)
        {
            if (timer >= flickerInterval)
            {
                lights.SetActive(i % 2 == 0);
                timer = 0f;

                if (i == 6)
                {
                    flickerInterval = 0.6f;
                }
                if (i == 10)
                {
                    flickerInterval = 1000f;
                }
                i++;
            }
        }
    }

    void UpdateAnimation()
    {
        Vector2 movementDirection = (targetPosition - transform.position).normalized;

        if (isMoving)
        {
            anim.SetFloat("moveX", movementDirection.x);
            anim.SetFloat("moveY", movementDirection.y);
            anim.SetBool("isMove", true);
        }
    }
}
