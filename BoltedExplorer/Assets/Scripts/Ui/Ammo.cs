using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {
    public List<GameObject> BulletList;
    [SerializeField]
    private int ammocount;
    public int AmmoCount
    {
        get
        {
            return ammocount;
        }
        set
        {
            ammocount = value;
            for (var i = 0; i < 8; i++)
            {
                BulletList[i].SetActive(i < ammocount);
            }
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
