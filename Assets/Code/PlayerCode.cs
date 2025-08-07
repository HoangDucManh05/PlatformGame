using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class PlayerCode : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator animator;
    public float movespeed;
    private float trai_phai;
    private bool isfasingRight = true;
    private bool isGrounded = true;
    public float doCao;
    private float moveVelocity;
    public int apple = 0;
    public int kiwi = 0;
    public int Health = 3;
    public TextMeshProUGUI appletext;
    public TextMeshProUGUI headingtext;
    public TextMeshProUGUI kiwitext;
    public GameObject GameOutButton1;
    public GameObject GameOutButton2;
    public GameObject GameOutButton3;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        headingtext.SetText(Health.ToString());
        appletext.SetText(apple.ToString());
        kiwitext.SetText(kiwi.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        #region Di chuyển
        moveVelocity = trai_phai * movespeed;
        trai_phai = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
        flip();
        
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            animator.SetBool("Jumping", true);
            rb.velocity = new Vector2(rb.velocity.x, doCao);


        }
        if ((Mathf.Abs(moveVelocity) >0 && isGrounded))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        #endregion

    }


    void flip()
    {
        if(isfasingRight && trai_phai < 0 || !isfasingRight && trai_phai > 0)
        {
            isfasingRight = !isfasingRight;
            Vector3 kick_thuoc = transform.localScale;
            kick_thuoc.x = kick_thuoc.x * -1;
            transform.localScale = kick_thuoc;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Groud"))
        {
            isGrounded = true;
            animator.SetBool("Jumping",false);
        }
        #region Win
        if (apple == 10)
        {
            if (collision.gameObject.CompareTag("win"))
            {
                SceneManager.LoadScene(4);
            }
        }
        if(kiwi == 10)
        {
            if (collision.gameObject.CompareTag("win"))
            {
                SceneManager.LoadScene(2);
            }
        }
        #endregion
        if (Health == 0)
        {
            Time.timeScale = 0;
            GameOutButton1.SetActive(true);
            GameOutButton2.SetActive(true);
            GameOutButton3.SetActive(true);
           
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        #region Die
        if (other.gameObject.CompareTag("Trap"))
        {
            Health--;
            headingtext.SetText(Health.ToString());
            Vector2 firsosition = new Vector2(x: -10, y: 1);
            transform.position = (Vector3)firsosition;
            
        }
        #endregion

        #region Apple
        if (other.gameObject.CompareTag("Apple"))
        {
            apple++;
            appletext.SetText(apple.ToString());
        }
        #endregion

        #region Kiwi
        if (other.gameObject.CompareTag("Kiwi"))
        {
            kiwi++;
            kiwitext.SetText(kiwi.ToString());
        }
        #endregion
        
    }
    

}
