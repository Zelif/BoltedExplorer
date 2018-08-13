using System;
using System.Collections;
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
    [SerializeField]
    private bool flashLightEnabled = false;
    [SerializeField]
    private float flashLightTime = 360;
    public float FlashlightDrainSpeed = 0.5f;
    public float armOffset = 0f;
    private float ReloadTimer = 2;
    private bool reloading = false;

    public static event HealthDelegate HealthEvent;
    public static event AnxietyDelegate AnxietyEvent;
    public static event AmmoDelegate AmmoEvent;
    public static event LoadedAmmoDelegate LoadedAmmoEvent;
    public static event ReloadDelegate ReloadEvent;
    public static event FlashLightDelegate FlashLightEvent;
    public static event FlashLightTimeDelegate FlashLightTimeEvent;

    public delegate void HealthDelegate(float h);
    public delegate void AnxietyDelegate(float a);
    public delegate void AmmoDelegate(int a);
    public delegate void LoadedAmmoDelegate(int lda);
    public delegate void FlashLightDelegate(bool flashLight);
    public delegate void FlashLightTimeDelegate(float flashLight);
    public delegate void ReloadDelegate(bool reload);

    public bool Reloading
    {
        get
        {
            return reloading;
        }
        set
        {
            reloading = value;
            if(ReloadEvent != null)
            {
                ReloadEvent(reloading);
            }
        }
    }

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

    public bool FlashLight
    {
        get
        {
            return flashLightEnabled;
        }
        set
        {
            flashLightEnabled = value;
            if(FlashLightEvent != null)
            {
                FlashLightEvent(flashLightEnabled);
            }
        }
    }

    public float FlashLightTime
    {
        get
        {
            return flashLightTime;
        }
        set
        {
            flashLightTime = value;
            if (FlashLightTimeEvent != null)
            {
                FlashLightTimeEvent(flashLightTime);
            }
        }
    }

    private void OnValidate()
    {
        LoadedAmmo = loadedAmmo;
        Health = health;
        Anxiety = anxiety;
        Ammo = ammo;
        FlashLight = flashLightEnabled;
        FlashLightTime = flashLightTime;
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
    private GameObject rightArm;
    private float originalJumpForce;
    private bool grounded = false;
    private bool inWater = false;
    private Animator anim;
    private new Rigidbody2D rigidbody;
    

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Awake Function

    void Awake()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("spawn");
        groundCheck = transform.Find("groundCheck");
        rightArm = GameObject.Find("RightArm");
        anim = GetComponent<Animator>();
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
        Health = 100;
        LoadedAmmo = 8;
        FlashLight = false;
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

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Reload();
        }

        if (Input.GetButtonDown("FlashLight"))
        {
            ToggleFlashLight();
        }

        if(FlashLightTime <= 0)
        {
            FlashLight = false;
        }

        if(FlashLight)
            FlashLightTime -= FlashlightDrainSpeed * Time.deltaTime;

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - rightArm.transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        rightArm.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + armOffset);
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Fixed Update Function

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        if( Input.GetAxis("Horizontal") != 0 )
        {
            anim.Play("Walking");
        }
        else
        {
            anim.Play("Idle");
        }

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
        if(LoadedAmmo == 0 || Reloading)
        {
            // Make event to trigger empty sound/maybe visual
            return;
        }
        // 
        LoadedAmmo--;
    }

    void Reload()
    {
        if(LoadedAmmo == 8 || Reloading)
        {
            return;
        }
        var ammoToReload = 8 - loadedAmmo;
        if(Ammo <= 0)
        {
            // Call event to play empty noise
            return;
        }
        Reloading = true;

        StartCoroutine(WaitFor(ReloadTimer, () => {
            ammoToReload = Mathf.Min(ammoToReload, Ammo);
            Ammo -= ammoToReload;
            LoadedAmmo = loadedAmmo + ammoToReload;
            Reloading = false;
        }));
    }

    IEnumerator WaitFor(float time, Action action) {
        yield return new WaitForSeconds(time);
        action();
    }

    void ToggleFlashLight()
    {
        if(flashLightTime <= 0)
        {
            flashLightEnabled = false;
            return;
        }
        FlashLight = !FlashLight;
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */
}
