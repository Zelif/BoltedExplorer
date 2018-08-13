using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour {
    public Text ScoreText;
    public int score;
	// Use this for initialization
	void Awake () {
        WraithController.DeathEvent += new WraithController.DeathDelegate(HandleWraith);
	}
	
    void HandleWraith(Vector3 pos)
    {
        score += 10;
        ScoreText.text = score.ToString();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
