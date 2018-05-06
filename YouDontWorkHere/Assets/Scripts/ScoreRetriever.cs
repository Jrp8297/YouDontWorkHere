using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRetriever : MonoBehaviour {

    public Text toDisplay;
    // Use this for initialization
    void Start () {
        toDisplay = gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        toDisplay.text = PlayerPrefs.GetInt("playerScore").ToString();
    }
}
