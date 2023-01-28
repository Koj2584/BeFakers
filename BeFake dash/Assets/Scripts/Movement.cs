using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 9f;
    private Rigidbody2D rb;

    private bool isGrounded;
    private bool repJump = true;
    private bool isColl;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public Transform collCheckUp;
    public Transform collCheckDown;
    public float checkRadius;
    public LayerMask whatIsGround;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckLeft.position, checkRadius, whatIsGround) ||
            Physics2D.OverlapCircle(groundCheckRight.position, checkRadius, whatIsGround);
        isColl = Physics2D.OverlapCircle(collCheckUp.position, checkRadius, whatIsGround) ||
            Physics2D.OverlapCircle(collCheckDown.position, checkRadius, whatIsGround);
        if (isColl)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (Input.GetKey(KeyCode.Space) && isGrounded && repJump)
        {
            rb.velocity = Vector2.up * jumpForce;
            repJump = false;
        }

        if (!isGrounded)
            repJump = true;
    }
}
