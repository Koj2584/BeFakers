using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 9f;
    private Rigidbody2D rb;     // ovládání fyziky postavy

    private bool isGrounded;    //zjištìní jestli, je postava na zemi.
    private bool repJump = true; // omezení poètu skokù postavy, dokud není znovu na zemi.
    private bool isColl; //zda postava narazila do nìjakého objektu
    public Transform groundCheckLeft; //k urèení bodu pro kontrolu, zda postava stojí na zemi.
    public Transform groundCheckRight;//k urèení 2.bodu pro kontrolu, zda postava stojí na zemi.
    public Transform collCheckUp;  // kontrola nárazu nahoru
    public Transform collCheckDown; // kontrola nárazu dolu
    public float checkRadius;
    public LayerMask whatIsGround;
    public Animator animator;

    public float minulaPozice;

    int i = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(i == 0)
            minulaPozice = this.gameObject.transform.position.x;
        if (i == 2)
        {
            isColl = Mathf.Abs(this.gameObject.transform.position.x - minulaPozice) < 0.05;

            if (isColl)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (i == 4)
            i = 0;
        else
            i++;

        rb.velocity = new Vector2(speed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckLeft.position, checkRadius, whatIsGround) ||
            Physics2D.OverlapCircle(groundCheckRight.position, checkRadius, whatIsGround);
        /*isColl = Physics2D.OverlapCircle(collCheckUp.position, checkRadius, whatIsGround) ||
            Physics2D.OverlapCircle(collCheckDown.position, checkRadius, whatIsGround);*/

        if (isGrounded&& rb.velocity.y < 0.7f&& rb.velocity.y > -0.7f)
        {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
        }
        else
        {
            repJump = true;
        }

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


        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

