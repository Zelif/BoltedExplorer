using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;

    public float moveForce = 365f;
    public float maxSpeed = 5f;
   // public AudioClip[] jumpClips;
    public float jumpForce = 300f;
   // public AudioClip[] taunts;
    public float tauntProbability = 50f;
    public float tauntDelay = 1f;

    private int tauntIndex;
    private Transform groundCheck;
    private bool grounded = false;
   // private Animator anim;
    private new Rigidbody2D rigidbody;

    void Awake()
    {
        groundCheck = transform.Find("groundCheck");
  //      anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if( Input.GetButtonDown("Jump") && grounded )
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

     //   anim.SetFloat("Speed", Mathf.Abs(h));

        if( h * rigidbody.velocity.x < maxSpeed )
        {
            rigidbody.AddForce(Vector2.right * h * moveForce);
        }

        if( Mathf.Abs(rigidbody.velocity.x) > maxSpeed )
        {
            rigidbody.velocity = new Vector2(Mathf.Sign(rigidbody.velocity.x) * maxSpeed, rigidbody.velocity.y);
        }

        if( h > 0 && !facingRight )
        {
            Flip();
        }
        else if( h < 0 && facingRight )
        {
            Flip();
        }

        if( jump )
        {
      //      anim.SetTrigger("Jump");

      //      int i = Random.Range(0, jumpClips.Length);
      //      AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

            rigidbody.AddForce(new Vector2(0f, jumpForce));

            jump = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
