using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoard : MonoBehaviour {
    [SerializeField]
    private float health;
    public float height;

    public RectTransform HealthLiquid;
    public Rigidbody2D LeftChainToBreak;
    public Rigidbody2D RightChainToBreak;

    private float originHeight;
    private float originalWidth;
    private RectTransform rectTransform;

    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    private void OnValidate()
    {
        Health = health;
    }

    void HandleHealthUpdate(float health)
    {
        Health = health;
    }

    // Use this for initialization
    void Awake () {
        rectTransform = HealthLiquid;
        originalWidth = rectTransform.sizeDelta.x;
        originHeight = rectTransform.sizeDelta.y;
        PlayerController.HealthEvent += new PlayerController.HealthDelegate(HandleHealthUpdate);
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

    private void OnDestroy()
    {
        PlayerController.HealthEvent -= HandleHealthUpdate;
    }
}
