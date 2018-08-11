using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    // TODO(Shaun): Refactor to only pass in textures instead of images for the image list

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Public Members

    public float runTime = 5f;              // Display time for images
    public float fadeTime = 1f;             // Fade in and out time
    public bool fade = true;                // Enables or disables fade
    public List<Image> imageList;           // List of images passed in
    public Image fadeImage;                 // Fade cover passed in to hide image list

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Private Members

    private float displayTimer;             // Current timer for display image
    private float fadeTimer;                // Current timer for fade cover
    private bool isFading;                  // Overall fade state running
    private bool fadeIn = true;             // State for fading in
    private bool fadeOut = false;           // State for fading out
    private int imageCount;                 // Count of the image list
    private int imageIndex = 0;             // Current image index

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Awake Function

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Start Function

    void Start()
    {
        displayTimer = runTime;

        if( fade )
        {
            fadeTimer = fadeTime;
            isFading = true;
        }
        else
        {
            DisableFadeImage();
        }

        imageCount = imageList.Count;
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Update Function

    void Update()
    {
        if( Input.anyKey )
        {
            SceneManager.LoadScene("Scenes/MainMenu");
        }

        if( isFading )
        {
            Fading();
        }
        else
        {
            DisplayImage();
        }
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Fixed Update Function

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Functions

    /// <summary>
    /// Checks and calculates the time for the fade in and/or 
    /// fade out.
    /// </summary>
    void Fading()
    {
        if( fadeIn )
        {
            fadeTimer -= Time.deltaTime;

            if( fadeTimer <= 0 )
            {
                fadeIn = false;
                isFading = false;
                fadeOut = true;
                fadeTimer = 0f;
            }
        }

        if( fadeOut && isFading )
        {
            fadeTimer += Time.deltaTime;
            if( fadeTimer >= fadeTime )
            {
                fadeOut = false;
                CheckImages();
                fadeIn = true;
            }
        }

        Color tmp = fadeImage.color;
        tmp.a = fadeTimer / fadeTime;
        fadeImage.color = tmp;
    }

    /// <summary>
    /// Checks and calculates if the time that the image is 
    /// displayed is up.
    /// </summary>
    void DisplayImage()
    {
        displayTimer -= Time.deltaTime;

        if( displayTimer <= 0.0f )
        {
            TimerEnded();
        }
    }

    /// <summary>
    /// Gets called after the display timer has finished. It
    /// will either fade or check for the next image
    /// </summary>
    void TimerEnded()
    {
        displayTimer = runTime;

        if( fade )
        {
            isFading = true;
            fadeTimer = 0;
        }
        else
        {
            CheckImages();
        }
    }

    /// <summary>
    /// Checks if there are any more images to display. If not
    /// it will load the main menu
    /// </summary>
    void CheckImages()
    {
        if( imageIndex + 1 < imageCount )
        {
            imageList[imageIndex].enabled = false;
            imageIndex++;
        }
        else
        {
            SceneManager.LoadScene("Scenes/MainMenu");
        }
    }

    /// <summary>
    /// Disables the fade layer if fade is false
    /// </summary>
    void DisableFadeImage()
    {
        fadeImage.enabled = false;
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */
}
