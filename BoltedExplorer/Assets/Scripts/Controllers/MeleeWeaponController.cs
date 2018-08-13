using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : MonoBehaviour
{
    public float damage = 5f;

    void OnTriggerEnter2D( Collider2D col )
    {
        if( col.gameObject.CompareTag("Player") )
        {
            col.GetComponent<PlayerController>().Health -= damage;
        }
    }
}
