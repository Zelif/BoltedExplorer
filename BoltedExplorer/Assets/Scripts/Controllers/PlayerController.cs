using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Public Members

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public float health = 100f;
    [HideInInspector]
    public float anxiety = 100f;


    public float moveForce = 365f;
    public float maxSpeed = 5f;
    // public AudioClip[] jumpClips;
    public float jumpForce = 300f;
    public float originalJumpForce;

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Private Members

    private Transform groundCheck;
    private GameObject spawnPoint;
    private bool grounded = false;
    // private Animator anim;
    private new Rigidbody2D rigidbody;
    private bool inWater = false;

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Awake Function

    void Awake()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("spawn");
        groundCheck = transform.Find("groundCheck");
        //      anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();

        var pos = spawnPoint.transform.position;
        var col = spawnPoint.GetComponent<BoxCollider2D>().offset;
        gameObject.transform.position = new Vector2(pos.x + col.x, pos.y + col.y);

        originalJumpForce = jumpForce;
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
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if( Input.GetButtonDown("Jump") && ( grounded || inWater ) )
        {
            jump = true;
        }
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Fixed Update Function

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        //   anim.SetFloat("Speed", Mathf.Abs(h));

        rigidbody.velocity = new Vector2(h * maxSpeed, rigidbody.velocity.y);


        /* Sliding code ---------------------*/

        //if( h * rigidbody.velocity.x < maxSpeed )
        //{
        //    rigidbody.AddForce(Vector2.right * h * moveForce);
        //}

        //if( Mathf.Abs(rigidbody.velocity.x) > maxSpeed )
        //{
        //    rigidbody.velocity = new Vector2(Mathf.Sign(rigidbody.velocity.x) * maxSpeed, rigidbody.velocity.y);
        //}

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

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region OnTrigger Functions

    void OnTriggerEnter2D( Collider2D col )
    {
        if( col.gameObject.CompareTag("goal") )
        {
            SceneManager.LoadScene("Scenes/MainMenu");
        }
    }

    void OnTriggerStay2D( Collider2D col )
    {
        if( col.gameObject.CompareTag("water"))
        {
            inWater = true;
            jumpForce = 150f;
        }
    }

    void OnTriggerExit2D( Collider2D col)
    {
        if( col.gameObject.CompareTag("water") )
        {
            inWater = false;
            jumpForce = originalJumpForce;
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
