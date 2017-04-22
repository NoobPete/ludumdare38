using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour {
	public static GameMasterScript main;

	public int gameTurnNumber = 0;
	public int actionPointsLeft;
	public int goldAmount;
	public int buyPoints;


	// Use this for initialization
	void Start () {
		main = this;

		ResetStats();
	}

	private void ResetStats()
	{
		actionPointsLeft = 1;
		goldAmount = 0;
		buyPoints = 1;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void EndTurn()
	{
		HandScript hand = HandScript.main;
		hand.DiscardAllCardsInHand();

		gameTurnNumber++;

		ResetStats();

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
		actionPointsLeft += script.numberOfNewActionPoints;
		HandScript.main.DrawCardsFromDeck(script.numberOfCardsToDraw);
		

	}

	public bool CanBuyThisCard(GameObject card)
	{
		CardScript script = card.GetComponent<CardScript>();

		if (goldAmount >= script.cost && buyPoints > 0)
		{
			goldAmount -= script.cost;
			buyPoints -= 1;

			return true;
		}

		return false;
	}

	public bool CanPlayThisCard(GameObject card)
	{
		CardScript script = card.GetComponent<CardScript>();

		if (script.isActionCard)
		{
			if (actionPointsLeft > 0)
			{
				actionPointsLeft--;
				return true;
			}
		}
		return false;
	}
}
