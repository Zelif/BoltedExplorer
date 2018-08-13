using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    public float Velocity;
    public float Damage;


	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().velocity = -transform.up * Velocity;
        //
    }
	
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("wraith"))
        {
            collision.GetComponent<WraithController>().Health -= Damage;
            Destroy(gameObject);
            return;
        }
        if (collision.gameObject.CompareTag("UI"))
        {
            Debug.Log("HIT");
            var normDirection = (transform.position - collision.GetComponent<Transform>().position).normalized;
            collision.GetComponent<Rigidbody2D>().AddForce(normDirection * Velocity);
            Destroy(gameObject);
            return;
        }
        Destroy(gameObject, 0.1f);
    }
}
