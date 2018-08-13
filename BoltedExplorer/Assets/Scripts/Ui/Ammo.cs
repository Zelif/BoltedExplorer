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

    private void OnValidate()
    {
        AmmoCount = ammocount;
    }

    void HandleAmmoChange(int ammo)
    {
        AmmoCount = ammo;
    }

    // Use this for initialization
    void Awake () {
        PlayerController.LoadedAmmoEvent 
            += new PlayerController.LoadedAmmoDelegate(HandleAmmoChange);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        PlayerController.LoadedAmmoEvent -= HandleAmmoChange;
    }
}
