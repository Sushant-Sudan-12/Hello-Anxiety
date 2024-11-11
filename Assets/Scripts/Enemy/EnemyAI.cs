using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public float patrolSpeed = 2f;
    public float detectionRadius = 5f;
    public float stopDistance = 1.5f;  // New parameter to stop at a certain distance from the player
    public Transform player;
    public TestMusicManager musicManager;
    private AnxietySystem anxietySystem;
    public Animator anim;

    private Vector3 targetPoint;
    public GameObject spotlight;
    private bool isChasing = false;

    private void Start()
    {
        transform.position = start.position;
        targetPoint = end.position;
        anxietySystem = FindObjectOfType<AnxietySystem>();
    }

    private void Update()
    {
        if (PlayerDetected())
        {
            FollowPlayer();
        }
        else if (isChasing)
        {
            StopChasing();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, patrolSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            targetPoint = targetPoint == end.position ? start.position : end.position;

            if (targetPoint == start.position)
            {
                anim.Play("ELeft");
            }
            else
            {
                anim.Play("ERight");
            }
        }
    }

    bool PlayerDetected()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= detectionRadius;
    }

    void FollowPlayer()
    {
        anxietySystem.SetChased(true);
        isChasing = true;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Only move towards the player if the enemy is further than the stopDistance
        if (distanceToPlayer > stopDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Vector3 targetPosition = player.position - directionToPlayer * stopDistance; // Maintain the stopDistance
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);
        }

        // Trigger chase music and spotlight while chasing
        musicManager.SetMusicState(TestMusicManager.MusicState.Chase);
        spotlight.SetActive(true);
        anim.SetBool("isFollow", true);
    }

    void StopChasing()
    {
        anxietySystem.SetChased(false);
        isChasing = false;
        musicManager.SetMusicState(TestMusicManager.MusicState.FreeRoam);
        spotlight.SetActive(false);
        anim.SetBool("isFollow", false);
    }

    private void OnDisable()
    {
        if (isChasing)
        {
            StopChasing(); // Stop chasing when this enemy is disabled
        }
    }
}
