using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float speed, jumpForce;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] bool isGrounded, isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            Movement();
        }
        else anim.SetBool("IsRunning", false);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime;
        }

        if (Input.GetMouseButton(0))
        {
            Attack();
        }
        else anim.SetBool("IsAttacking", false);
    }

    private void LateUpdate()
    {
        
    }

    void Movement()
    {
        if (rb.velocity.y == 0)
            anim.SetBool("IsRunning", true);

        if (Input.GetAxis("Horizontal") > 0 && spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = false;
        }

        if (Input.GetAxis("Horizontal") < 0 && spriteRenderer.flipX == false)
        {
            spriteRenderer.flipX = true;
        }

        transform.Translate(Vector3.right * speed * Input.GetAxis("Horizontal") * Time.deltaTime);
    }

    void Jump()
    {
        anim.SetBool("IsJumping", true);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void EndJumping()
    {
        anim.SetBool("IsJumping", false);
    }

    public void EndAttacking()
    {
        isAttacking = false;
    }

    void Attack()
    {
        anim.SetBool("IsAttacking", true);
        isAttacking = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            transform.position = new Vector3(-7.4f, -3.1f, 0);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
