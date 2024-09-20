using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float minYPosition = -4.5f;
    public Rigidbody2D rb;

    private float targetYPosition;
    private float targetXPosition;
    private bool isMoving = false;
    public Transform childTransform;
    public GameObject lights;
    private float timer =0f;
    private float flickerInterval = 0.45f;
    private int i=0;
    private bool lighton;
    public Animator anim;
    public static PlayerMovement instance;
    public string areaTransitionName;

    private void Awake() {
        if(instance == null) {
            instance = this;
           
        }

    }
    void Update()
    {
        if(targetXPosition-transform.position.x<0)
            anim.SetFloat("moveX",1);
        else if(targetXPosition-transform.position.x>0)
            anim.SetFloat("moveX",-1);

        if(targetYPosition-transform.position.y<0)
            anim.SetFloat("moveY",1);
        else if(targetYPosition-transform.position.y>0)
            anim.SetFloat("moveY",-1);

        if(Input.GetAxisRaw("Horizontal")==1||Input.GetAxisRaw("Vertical")==1||Input.GetAxisRaw("Horizontal")==-1||Input.GetAxisRaw("Vertical")==-1){
            anim.SetFloat("lastMoveX",Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("lastMoveY",Input.GetAxisRaw("Vertical"));
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; 
            

            if (CheckMouseClickPosition(mousePosition))
            {
                targetYPosition = mousePosition.y;
                targetXPosition = mousePosition.x;
                isMoving = true;
                anim.SetBool("isMove", true);
            }
            }

            if (isMoving)
            {
                MoveToTarget();
            } 

            timer += Time.deltaTime;
            if(transform.position.y > -3){
                lighton = true;
            }
            if(lighton){
                timer += Time.deltaTime;
                if (timer >= flickerInterval){
                    if(i%2==0){
                        lights.SetActive(true);
                    }
                    else{
                        lights.SetActive(false);
                    }
                    timer = 0f;
                    if(i == 6){
                        flickerInterval=0.6f;
                    }
                    if(i ==10){
                        flickerInterval = 1000f;
                    }
                    i++;
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
        Vector3 currentPosition = transform.position;
        

        currentPosition.y = Mathf.MoveTowards(currentPosition.y, targetYPosition, moveSpeed * Time.deltaTime);
        currentPosition.y = Mathf.Max(currentPosition.y, minYPosition);

        currentPosition.x = Mathf.MoveTowards(currentPosition.x, targetXPosition, moveSpeed * Time.deltaTime);

        transform.position = currentPosition;

        float scaleFactor = 1 - (currentPosition.y + 4) / 10;
        scaleFactor = Mathf.Clamp(scaleFactor, 0.1f, 1f);

        float scaleX = 0.3f * scaleFactor;
        float scaleY = 0.3f * scaleFactor;

        childTransform.localScale = new Vector3(scaleX, scaleY, transform.localScale.z);

        if (Mathf.Abs(currentPosition.y - targetYPosition) < 0.1f &&
            Mathf.Abs(currentPosition.x - targetXPosition) < 0.1f)
        {
            isMoving = false;
            anim.SetBool("isMove", false);
        }
    }
}

