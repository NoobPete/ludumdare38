using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour {
	public static GameMasterScript main;

	public int gameTurnNumber = 0;
	public int goldAmount;


	// Use this for initialization
	void Start () {
		main = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EndTurn()
	{
		HandScript hand = HandScript.main;
		hand.DiscardAllCardsInHand();

		gameTurnNumber++;

		goldAmount = 0;

		for (int i = 0; i < 5; i++)
		{
			hand.DrawCardsFromDeck();
		}
	}

	public void ApplyCardEffects(GameObject card)
	{
		CardScript script = card.GetComponent<CardScript>();

		if (script == null)
		{
			throw new Exception("Card dose not have a card script");
		}

		goldAmount += script.goldAmountOnPlay;

	}

	public bool CanBuyThisCard(GameObject card)
	{
		CardScript script = card.GetComponent<CardScript>();

		if (goldAmount >= script.cost)
		{
			goldAmount -= script.cost;
			return true;
		}

		return false;
	}
}
