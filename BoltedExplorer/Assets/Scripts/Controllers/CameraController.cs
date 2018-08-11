using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Public Members

    public GameObject player;       //Public variable to store a reference to the player game object
    public float yOffset = 0f;

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Private Members

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Awake Function

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Start Function

    void Start()
    {
        var additionalOffset = new Vector3(player.transform.position.x, player.transform.position.y - yOffset);
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - additionalOffset;
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Update Function

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Late Update Function

    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Fixed Update Function

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Functions

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */
}
