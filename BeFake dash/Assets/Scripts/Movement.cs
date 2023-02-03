using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 9f;
    private Rigidbody2D rb;     // ovl�d�n� fyziky postavy

    private bool isGrounded;    //zji�t�n� jestli, je postava na zemi.
    private bool repJump = true; // omezen� po�tu skok� postavy, dokud nen� znovu na zemi.
    private bool isColl; //zda postava narazila do n�jak�ho objektu
    public Transform groundCheckLeft; //k ur�en� bodu pro kontrolu, zda postava stoj� na zemi.
    public Transform groundCheckRight;//k ur�en� 2.bodu pro kontrolu, zda postava stoj� na zemi.
    public Transform collCheckUp;  // kontrola n�razu nahoru
    public Transform collCheckDown; // kontrola n�razu dolu
    public float checkRadius;
    public LayerMask whatIsGround;
    public Animator animator;



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

        if(isGrounded&& rb.velocity.y < 0.7f&& rb.velocity.y > -0.7f)
        {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
        }
        else
        {
            repJump = true;
        }

        if (isColl)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (Input.GetKey(KeyCode.Space) && isGrounded && repJump)
        {
            rb.velocity = Vector2.up * jumpForce;
            repJump = false;
        }

        if (rb.velocity.y > 0.7f)
        {
            animator.SetBool("Up", true);
            animator.SetBool("Down", false);
        }
        if (rb.velocity.y < -0.7f)
        {
            animator.SetBool("Up", false);
            animator.SetBool("Down", true);
        }
    }
}

