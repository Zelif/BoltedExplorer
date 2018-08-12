using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoard : MonoBehaviour {
    public float health;
    public float height;

    public RectTransform HealthLiquid;
    public Rigidbody2D LeftChainToBreak;
    public Rigidbody2D RightChainToBreak;

    private float originHeight;
    private float originalWidth;
    private RectTransform rectTransform;
    // Use this for initialization
    void Start () {
        health = 100;
        rectTransform = HealthLiquid;
        originalWidth = rectTransform.sizeDelta.x;
        originHeight = rectTransform.sizeDelta.y;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        height = health /100 * originHeight;
        rectTransform.sizeDelta = new Vector2(originalWidth, height);

        if(health < 0)
        {
            LeftChainToBreak.simulated = false;
            RightChainToBreak.simulated = false;
        }
    }
}
