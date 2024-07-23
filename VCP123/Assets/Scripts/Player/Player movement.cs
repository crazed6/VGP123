using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class Playermovement : MonoBehaviour
{
    [SerializeField, Range(1, 20)] private float speed = 5;
    [SerializeField, Range(1, 20)] private float jumpForce = 10;
    [SerializeField, Range(0.01f, 1)] private float groundCheckRadius = 0.02f;
    [SerializeField] private LayerMask isGroundLayer;


    private Transform groundCheck;
    private bool isGrounded = false;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // Component References Filled
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if(speed <= 0 )
        {
            speed = 5;
            Debug.Log("Speed was set to a value less than 0");
        }

        if(jumpForce <= 0)
        {
            jumpForce = 10;
            Debug.Log("jumpForce was set to a value less than 0");
        }

        //Creating groundCheck object
        if(!groundCheck)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "groundCheck";
            groundCheck = obj.transform; 
        }
        Debug.Log(rb.name);
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
   
        //grab horizontal axis - Check project settings > Input manager to see the inputs defined
        float hInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire1") && isGrounded)
        {
            anim.SetTrigger("aInput");
        }

        if(Input.GetButtonDown("Fire1") &&  !isGrounded)
        {
            anim.SetTrigger("aInput");
        }
       
        // Sprite Flipping
        if (hInput != 0) sr.flipX = (hInput < 0);

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
    }
    void IsGrounded()
    {
        if (rb.velocity.y <= 0)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        }

        else
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
    }
}
