using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    private AudioSource audio;

    void HandleFlashLightEvent(bool enabled)
    {
        GetComponent<Light>().enabled = enabled;
        audio.Play();
    }

    // Use this for initialization
    void Awake () {
        audio = GetComponent<AudioSource>();

        PlayerController.FlashLightEvent
            += new PlayerController.FlashLightDelegate(HandleFlashLightEvent);
    }
	
    private void OnDestroy()
    {
        PlayerController.FlashLightEvent -= HandleFlashLightEvent;
    }
}
