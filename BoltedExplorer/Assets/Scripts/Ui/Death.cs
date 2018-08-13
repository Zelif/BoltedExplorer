using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Death : MonoBehaviour {
    public Text Score;
    public Text display;
	// Use this for initialization
	void Awake () {
        PlayerController.HealthEvent += new PlayerController.HealthDelegate((float health) =>
        {
            if(health == 0)
            {
                display.text = string.Format("Score:{0}", Score.text);
                StartCoroutine(WaitFor(3, () =>
                {
                    GetComponent<Canvas>().enabled = true;
                }));
            }
        });
	}

    IEnumerator WaitFor(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }

}
