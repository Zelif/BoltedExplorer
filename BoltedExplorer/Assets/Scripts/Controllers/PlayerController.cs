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
    [SerializeField]
    private float health = 100f;
    [SerializeField]
    private float anxiety = 100f;
    [SerializeField]
    private int ammo = 14;
    [SerializeField]
    private int loadedAmmo = 8;


    public event HealthDelegate HealthEvent;
    public event AnxietyDelegate AnxietyEvent;
    public event AmmoDelegate AmmoEvent;
    public event LoadedAmmoDelegate LoadedAmmoEvent;

    public delegate void HealthDelegate(float h);
    public delegate void AnxietyDelegate(float a);
    public delegate void AmmoDelegate(int a);
    public delegate void LoadedAmmoDelegate(int lda);

    public float Anxiety
    {
        get
        {
            return anxiety;
        }
        set
        {
            anxiety = value;
            if(AnxietyEvent != null)
            {
                AnxietyEvent(anxiety);
            }
        }
    }

    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if(HealthEvent != null)
            {
                HealthEvent(health);
            }
        }
    }

    public int Ammo
    {
        get
        {
            return ammo;
        }
        set
        {
            ammo = value;
            if(AmmoEvent != null)
            {
                AmmoEvent(ammo);
            }
        }
    }

    public int LoadedAmmo
    {
        get
        {
            return loadedAmmo;
        }
        set
        {
            loadedAmmo = value;
            if(LoadedAmmoEvent != null)
            {
                LoadedAmmoEvent(loadedAmmo);
            }
        }
    }

    private void OnValidate()
    {
        LoadedAmmo = loadedAmmo;
        Health = health;
        Anxiety = anxiety;
        Ammo = ammo;
    }


    public float moveForce = 365f;
    public float maxSpeed = 5f;
    // public AudioClip[] jumpClips;
    public float jumpForce = 300f;
    public GameObject activeItem;

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Private Members

    private Transform groundCheck;
    private GameObject spawnPoint;
    private float originalJumpForce;
    private bool grounded = false;
    private bool inWater = false;
    // private Animator anim;
    private new Rigidbody2D rigidbody;

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Awake Function

    void Awake()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("spawn");
        groundCheck = transform.Find("groundCheck");
        //      anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();

        if( spawnPoint != null)
        {
            var pos = spawnPoint.transform.position == null ? new Vector3(0, 0, 0) : spawnPoint.transform.position;
            var col = spawnPoint.GetComponent<BoxCollider2D>().offset;
            gameObject.transform.position = new Vector2(pos.x + col.x, pos.y + col.y);
        }

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

    void Shoot()
    {
        // Check Ammo and loaded
        if(LoadedAmmo == 0)
        {
            // Make event to trigger empty sound/maybe visual
            return;
        }
        // 
        LoadedAmmo--;
    }

    void Reload()
    {
        if(LoadedAmmo == 8)
        {
            return;
        }
        var ammoToReload = 8 - loadedAmmo;
        if(Ammo == 0)
        {
            // Call event to play empty noise
            return;
        }
        ammoToReload = Mathf.Min(ammoToReload, Ammo);
        LoadedAmmo = loadedAmmo +  ammoToReload;
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */
}
