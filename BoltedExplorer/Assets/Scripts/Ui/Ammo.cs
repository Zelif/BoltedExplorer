using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour {
    public List<GameObject> BulletList;
    public Image ReloadingImage;
    public Text AmmoCounter;

    public Color AmmoEmptyColour;
    public Color AmmoFullColour;

    private int ammoCount;

    [SerializeField]
    private int barrelAmmo;
    public int BarrelAmmo
    {
        get
        {
            return barrelAmmo;
        }
        set
        {
            barrelAmmo = value;
            for (var i = 0; i < 8; i++)
            {
                BulletList[i].SetActive(i < barrelAmmo);
            }
        }
    }

    private void OnValidate()
    {
        BarrelAmmo = barrelAmmo;
    }

    void HandleAmmoChange(int ammo)
    {
        BarrelAmmo = ammo;
    }

    void HandleTotalAmmo(int ammo)
    {
        ammoCount = ammo;
        AmmoCounter.text = ammo.ToString();
        if(ammoCount == 0)
        {
            AmmoCounter.color = AmmoEmptyColour;
        }
        else
        {
            AmmoCounter.color = AmmoFullColour;
        }
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
        PlayerController.AmmoEvent
            += new PlayerController.AmmoDelegate(HandleTotalAmmo);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        PlayerController.LoadedAmmoEvent -= HandleAmmoChange;
        PlayerController.ReloadEvent -= HandleReload;
        PlayerController.AmmoEvent -= HandleTotalAmmo;
    }
}
