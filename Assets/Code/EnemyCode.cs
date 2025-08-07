using UnityEngine;

public class EnemyCode : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 5f;  // Phạm vi phát hiện nhân vật
    public Transform leftLimit;
    public Transform rightLimit;
    public Transform player;  // Tham chiếu đến nhân vật
    public LayerMask obstacleLayer;  // Lớp đối tượng cần kiểm tra va chạm

    private bool movingRight = true;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        else
        {
            // Nhân vật không ở gần, di chuyển qua lại giữa các giới hạn
            Patrol();
        }
    }

    void Patrol()
    {
        if (movingRight)
        {
            if (!CheckObstacle(Vector2.right))
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                animator.SetBool("isMovingRight", true);
                spriteRenderer.flipX = true;  // Không lật hình
            }
            else
            {
                movingRight = false;  // Đổi hướng khi gặp vật cản
            }

            if (transform.position.x >= rightLimit.position.x)
            {
                movingRight = false;
            }
        }
        else
        {
            if (!CheckObstacle(Vector2.left))
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                animator.SetBool("isMovingRight", true);
                spriteRenderer.flipX = false;  // Lật hình
            }
            else
            {
                movingRight = true;  // Đổi hướng khi gặp vật cản
            }

            if (transform.position.x <= leftLimit.position.x)
            {
                movingRight = true;
            }
        }
    }

    void MoveRight()
    {
        if (!CheckObstacle(Vector2.right))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            animator.SetBool("isMovingRight", true);
            spriteRenderer.flipX = true;  // Không lật hình
        }
    }

    void MoveLeft()
    {
        if (!CheckObstacle(Vector2.left))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            animator.SetBool("isMovingRight", true);
            spriteRenderer.flipX = false;  // Lật hình
        }
    }

    bool CheckObstacle(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.1f, obstacleLayer);
        return hit.collider != null;
    }

    void OnDrawGizmos()
    {
        if (leftLimit != null && rightLimit != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(leftLimit.position, rightLimit.position);
        }

        // Vẽ vòng tròn phạm vi phát hiện
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Vẽ raycast để kiểm tra vật cản
        Gizmos.color = Color.green;
        if (movingRight)
        {
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * 0.1f);
        }
        else
        {
            Gizmos.DrawLine(transform.position, transform.position + Vector3.left * 0.1f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            Destroy(gameObject);
        }
    }
}