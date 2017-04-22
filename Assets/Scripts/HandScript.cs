using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {
	[Range(0.001f, 0.1f)]
	public float cardSpacing;

	private List<GameObject> cardStack;

	// Use this for initialization
	void Start () {
		cardStack = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
