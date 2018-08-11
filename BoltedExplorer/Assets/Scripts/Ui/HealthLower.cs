using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLower : MonoBehaviour {
    public float health;
    public float height;

    private float originHeight;
    private float originalWidth;
    private RectTransform rectTransform;
    // Use this for initialization
    void Start () {
        health = 100;
        rectTransform = GetComponent<RectTransform>();
        originalWidth = rectTransform.sizeDelta.x;
        originHeight = rectTransform.sizeDelta.y;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        height = health /100 * originHeight;
        rectTransform.sizeDelta = new Vector2(originalWidth, height);

    }
}
