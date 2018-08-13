using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour {

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Public Members

    public TriggerType type = TriggerType.Unassigned;

    public GameObject prefab;
    public GameObject target;
    public GameObject spawnLocation;

    public bool limited = false;
    public int count = 1;

    public string dialogue = "";
    public float dialogueDisplayTime = 5f;

    public float startDelayTime = 3f;
    public float endDelayTime = 3f;
    public float speed = 3f;

    private bool onEnter = false;
    private bool destroy = false;
    private float runTimer = 0f;

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Private Members

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Awake Function

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Start Function

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Update Function

    void Update()
    {
        if( onEnter )
        {

            if( destroy )
            {
                runTimer += Time.deltaTime;

                if( runTimer > startDelayTime )
                {
                    Destroy(target);
                }
            }

            if( !limited && type == TriggerType.Wraith )
            {
                runTimer += Time.deltaTime;

                if( runTimer > startDelayTime )
                {
                    InitialiseWraith();
                    runTimer = 0f;
                }
            }
        }
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Late Update Function

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Fixed Update Function

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region OnTrigger Functions

    void OnTriggerEnter2D( Collider2D col )
    {
        if( col.gameObject.CompareTag("Player") )
        {
            if( !onEnter )
            {
                onEnter = true;

                switch( type )
                {
                    case TriggerType.Dialogue:
                        InitialiseDialogue();
                        break;
                    case TriggerType.Wraith:
                        InitialiseWraith();
                        break;
                    case TriggerType.Trap:
                        InitialiseTrap();
                        break;
                    case TriggerType.Destroy:
                        destroy = true;
                        break;
                };
            }
        }
    }

    void OnTriggerExit2D( Collider2D col )
    {
        //if( col.gameObject.CompareTag("Player") )
        //{
        //    onEnter = false;
        //}
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Functions

    void InitialiseDialogue()
    {
        var dialoguePrefab = Instantiate( prefab, target.transform.position, Quaternion.identity) as GameObject;
        var component = dialoguePrefab.GetComponent<DialogueController>();
        component.Initalise( target, dialogue, dialogueDisplayTime );
        component.GetComponent<DialogueController>().Run();
        Destroy(this);
    }

    void InitialiseWraith()
    {
        var wraithPrefab = Instantiate(prefab, spawnLocation.transform.position, Quaternion.identity) as GameObject;
        var component = wraithPrefab.GetComponent<WraithController>();
        component.Initialise( target );
        component.Run();

        if( limited )
        {
            Destroy(this);
        }
    }

    void InitialiseTrap()
    {
        var trapPrefab = Instantiate(prefab, target.transform.position, Quaternion.identity) as GameObject;
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */
}
