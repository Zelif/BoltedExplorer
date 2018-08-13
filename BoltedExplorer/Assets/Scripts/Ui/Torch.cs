using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Torch : MonoBehaviour {

    [SerializeField]
    private float torchLeft = 360;
    public float TorchLeft
    {
        get
        {
            return torchLeft;
        }
        set
        {
            torchLeft = Mathf.Clamp(value, 0, 360);
            ImageFill.fillAmount = torchLeft/360;
        }
    }

    private void OnValidate()
    {
        TorchLeft = torchLeft;
    }

    public Image ImageFill;

    void HandleFlashLightEvent(float time)
    {
        TorchLeft = time;
    }

	// Use this for initialization
	void Awake () {
        PlayerController.FlashLightTimeEvent 
            += new PlayerController.FlashLightTimeDelegate(HandleFlashLightEvent);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        PlayerController.FlashLightTimeEvent -= HandleFlashLightEvent;

    }
}
