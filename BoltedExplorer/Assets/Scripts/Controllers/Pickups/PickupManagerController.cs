using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManagerController : MonoBehaviour {
    public List<GameObject> PickupList;
    public int DropRate;
    void SpawnPickup(Vector3 pos)
    {
        if (Random.Range(0, 100) < DropRate)
        {
            var randPickupNumber = Random.Range(0, PickupList.Count - 1);
            Instantiate(PickupList[randPickupNumber], pos, new Quaternion());
        }
    }
	// Use this for initialization
	void Awake () {
        WraithController.DeathEvent
            += new WraithController.DeathDelegate(SpawnPickup);
	}

    private void OnDestroy()
    {
        WraithController.DeathEvent -= SpawnPickup;
    }
}
