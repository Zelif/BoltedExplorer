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
    public GameObject DeathPrefab;
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if(health <= 0 && DeathEvent != null)
            {
                DeathEvent(transform.position);
                var dedWraith = Instantiate(DeathPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
                Destroy(dedWraith, 1.5f);
            }
        }
    }

    public static event DeathDelegate DeathEvent;
    public delegate void DeathDelegate(Vector3 pos);

    public float xOffset = 2.5f;
    public float moveForce = 365f;
    public float maxSpeed = 10f;
    // public AudioClip[] jumpClips;
    public float jumpForce = 300f;
    public GameObject activeItem;


    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Private Members

    private Animator anim;
    private GameObject target;
    private AudioSource audio;
    private bool execute = false;

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Awake Function

    void Awake()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Start Function

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Update Function

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Fixed Update Function

    void FixedUpdate()
    {
        if( execute )
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
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region OnTrigger Functions

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

    public void Initialise( GameObject newTarget )
    {
        target = newTarget;
        if( anim == null )
        {
            Debug.Log("Could not find animator");
            anim = GetComponent<Animator>();
        }
    }

    public void Run()
    {
        execute = true;
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */
}
