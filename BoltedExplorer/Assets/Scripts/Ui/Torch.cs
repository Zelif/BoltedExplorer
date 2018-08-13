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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
