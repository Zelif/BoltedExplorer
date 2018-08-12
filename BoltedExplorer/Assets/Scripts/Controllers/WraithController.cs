using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithController : MonoBehaviour {

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Public Members

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public float health = 100f;

    public float xOffset = 2.5f;
    public float moveForce = 365f;
    public float maxSpeed = 10f;
    // public AudioClip[] jumpClips;
    public float jumpForce = 300f;
    public GameObject activeItem;
    public GameObject target;


    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Private Members

    private bool grounded = false;
    private bool inWater = false;
    private Animator anim;
    private new Rigidbody2D rigidbody;

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Awake Function

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();

        Debug.Log(anim);
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Start Function

    void Start()
    {
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Update Function

    void Update()
    {
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Fixed Update Function

    void FixedUpdate()
    {
        if( transform.position.x - target.transform.position.x < -xOffset || transform.position.x - target.transform.position.x > xOffset )
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, maxSpeed * Time.deltaTime);
            anim.Play("Idle");
        }
        else
        {
            anim.Play("Attack");
        }

        if( transform.position.x - target.transform.position.x < 0 && !facingRight )
        {
            Flip();
        }
        else if( transform.position.x - target.transform.position.x > 0 && facingRight )
        {
            Flip();
        }
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region OnTrigger Functions

    void OnTriggerStay2D( Collider2D col )
    {
        if( col.gameObject.CompareTag("water") )
        {
            inWater = true;
        }
    }

    void OnTriggerExit2D( Collider2D col )
    {
        if( col.gameObject.CompareTag("water") )
        {
            inWater = false;
        }
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Functions

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */
}
