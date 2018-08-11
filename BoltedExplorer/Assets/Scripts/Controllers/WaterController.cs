using UnityEngine;

public class WaterController : MonoBehaviour {

    public float waterLevel = 4f;
    public float floatHeight = 2;
    public float bounceDamp = 0.05f;
    public Vector3 buoyancyCentreOffset;
    private float forceFactor;
    private Vector3 actionPoint;
    private Vector3 uplift;

    void OnTriggerStay2D( Collider2D col )
    {
        if( col.gameObject.CompareTag("Player") )
        {
            Floating(col);
        }
    }

    void Floating( Collider2D col )
    {
        actionPoint = col.transform.position + col.transform.TransformDirection(buoyancyCentreOffset);
        forceFactor = 1f - ( ( actionPoint.y - waterLevel ) / floatHeight );
        if( forceFactor > 0f )
        {
            uplift = -Physics.gravity * ( forceFactor - col.GetComponent<Rigidbody2D>().velocity.y * bounceDamp );
            col.GetComponent<Rigidbody2D>().AddForceAtPosition(uplift, actionPoint);
        }
    }
}
