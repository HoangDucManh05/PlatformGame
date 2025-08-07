using UnityEngine;

public class Enemy1Code : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 5f;  // Phạm vi phát hiện nhân vật
    public Transform player;  // Tham chiếu đến nhân vật
    private bool movingRight = true;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Nhân vật ở gần, hướng về nhân vật
            if (player.position.x > transform.position.x)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }
      
    }

 
    void MoveRight()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        animator.SetBool("isMovingRight", true);
    }

    void MoveLeft()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        animator.SetBool("isMovingRight", false);
    }

    void OnDrawGizmos()
    {
        // Vẽ vòng tròn phạm vi phát hiện
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}