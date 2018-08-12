using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {

    #region Public Members

    [HideInInspector]
    public GameObject target;

    [HideInInspector]
    public string dialogue = "?!";

    [HideInInspector]
    public float dialogueDisplayTime = 5f;

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Private Members

    private float runTimer = 0f;
    private bool execute = false;

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
        if( execute )
        {
            if( runTimer < dialogueDisplayTime )
            {
                runTimer += Time.deltaTime;
            }
            else
            {
                Destroy( this.gameObject );
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

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Functions

    public void Initalise( GameObject newTarget, string text, float time )
    {
        var dialogueBox = gameObject.GetComponentInChildren<Text>().text = text;
        dialogue = text;
        dialogueDisplayTime = time;
    }

    public void Run()
    {
        execute = true;
    }

    #endregion
}
