using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour {
    public List<GameObject> BulletList;
    public Image ReloadingImage;

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

    void HandleReload(bool reloading)
    {
        ReloadingImage.enabled = reloading;
    }

    // Use this for initialization
    void Awake () {
        PlayerController.LoadedAmmoEvent 
            += new PlayerController.LoadedAmmoDelegate(HandleAmmoChange);
        PlayerController.ReloadEvent
            += new PlayerController.ReloadDelegate(HandleReload);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        PlayerController.LoadedAmmoEvent -= HandleAmmoChange;
        PlayerController.ReloadEvent -= HandleReload;
    }
}
