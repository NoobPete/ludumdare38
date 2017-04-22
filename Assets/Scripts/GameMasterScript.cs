using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EndTurn()
	{
		HandScript hand = HandScript.main;
		hand.DiscardAllCardsInHand();

		for (int i = 0; i < 5; i++)
		{
			hand.DrawCardsFromDeck();
		}
	}
}
