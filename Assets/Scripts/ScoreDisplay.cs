using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = ScoreKeeper.GetScore().ToString();
        ScoreKeeper.Reset();
	}
}
