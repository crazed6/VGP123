using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Playermovement : MonoBehaviour
{
    private Coroutine jumpForceChange;
    private Coroutine speedChange;

    public void PowerupValueChange(pickups.PickupType type)
    {
        if (type == pickups.PickupType.PowerupSpeed)
            StartPowerupCoroutine(ref speedChange, ref speed, type);

        if (type == pickups.PickupType.PowerupJump)
            StartPowerupCoroutine(ref jumpForceChange, ref jumpForce, type);
    }

    public void StartPowerupCoroutine(ref Coroutine InCoroutine, ref float inVar, pickups.PickupType type)
    {
        if (InCoroutine != null)
        {
            StopCoroutine(InCoroutine);
            InCoroutine = null;
            inVar /= 2;
        }

        InCoroutine = StartCoroutine(PowerupChange(type));
    }

    IEnumerator PowerupChange(pickups.PickupType type)
    {
        //this code runs before the wait
        if (type == pickups.PickupType.PowerupSpeed)
            speed *= 2;

        if (type == pickups.PickupType.PowerupJump)
            jumpForce *= 2;

        Debug.Log($"Jump force value is {jumpForce}, Speed value is {speed}");

        yield return new WaitForSeconds(5.0f);

        if (type == pickups.PickupType.PowerupSpeed)
        {
            speed /= 2;
            speedChange = null;
        }
        if (type == pickups.PickupType.PowerupJump)
        {
            jumpForce /= 2;
            jumpForceChange = null;
        }

        Debug.Log($"Jump force value is {jumpForce}, Speed value is {speed}");
    }

    [SerializeField, Range(1, 20)] private float speed = 5;
    [SerializeField, Range(1, 20)] private float jumpForce = 10;
    [SerializeField, Range(0.01f, 1)] private float groundCheckRadius = 0.02f;
    [SerializeField] private LayerMask isGroundLayer;


    private Transform groundCheck;
    private bool isGrounded = false;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    AudioSource aud;

    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip stompClip;



    // Start is called before the first frame update
    void Start()
    {
        // Component References Filled
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (speed <= 0 )
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
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        
        //grab horizontal axis - Check project settings > Input manager to see the inputs defined
        float hInput = Input.GetAxis("Horizontal");

        IsGrounded();

        if (curPlayingClips.Length > 0)
        {
            if (curPlayingClips[0].clip.name == "Attack")
            {
                if (isGrounded)
                    rb.velocity = Vector2.zero;
            }
            else
            {
                rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
            }
        
        }



        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire2") && !curPlayingClips[0].clip.name.Contains("Attack"))
        {
            anim.SetTrigger("Fire");
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
        if (!isGrounded)
        {
            if (rb.velocity.y <= 0)
            {
                isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
            }
        }
        else
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
    }

    void IncreaseGravity()
    {

        rb.gravityScale = 10;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.lives--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Squish"))
        {
            collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(9999);
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

}
