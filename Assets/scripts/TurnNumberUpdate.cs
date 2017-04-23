using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnNumberUpdate : MonoBehaviour {
    private Text textField;


	// Use this for initialization
	void Start () {
        textField = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        textField.text = "" + GameMasterScript.main.gameTurnNumber;
	}
}
