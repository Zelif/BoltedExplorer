using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {
    void HandleFlashLightEvent(bool enabled)
    {
        GetComponent<Light>().enabled = enabled;
    }

	// Use this for initialization
	void Awake () {
        PlayerController.FlashLightEvent
            += new PlayerController.FlashLightDelegate(HandleFlashLightEvent);
    }
	
    private void OnDestroy()
    {
        PlayerController.FlashLightEvent -= HandleFlashLightEvent;
    }
}
