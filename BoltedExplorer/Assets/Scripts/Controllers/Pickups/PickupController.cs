using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {
    public PickupEnum PickupType;
    public float Value;
    public float HoverHeight;
    private float originalYpos;

    // Use this for initialization
	void Start () {
        originalYpos = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        var t = Time.time % 1f;
        if (t > 0.5f)
            t = 1f - t;
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Lerp(originalYpos, originalYpos + HoverHeight, t),
            transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            switch(PickupType)
            {
                case PickupEnum.Health:
                    {
                        col.GetComponent<PlayerController>().Health += Value;
                        break;
                    }
                case PickupEnum.Ammo:
                    {
                        col.GetComponent<PlayerController>().Ammo += (int)Value;
                        break;
                    }
                case PickupEnum.Battery:
                    {
                        col.GetComponent<PlayerController>().FlashLightTime += Value;
                        break;
                    }
            }
            Destroy(gameObject);
        }
    }
}
