using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour
{
	public static GameMasterScript main;

	public int gameTurnNumber = 0;
	public int actionPointsLeft;
	public int goldAmount;
	public int buyPoints;
	public int enemyNumber = 0;
	public int bricks = 0;
	public int health = 100;
	public int enemyBossHealth = 1000;
	public int extraGoldNextRound;
	public float damageModifier;
	[Range(0.01f, 1.2f)]
	public float linear;
	[Range(1f, 1.4f)]
	public float exponent;
	[Range(0f, 5f)]
	public float offset;


	// Use this for initialization
	void Start()
	{
		main = this;

		ResetStats();
	}

	private void ResetStats()
	{
		actionPointsLeft = 1;
		goldAmount = 0;
		buyPoints = 1;
		extraGoldNextRound = 0;
		damageModifier = 1;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void EndTurn()
	{
		HandScript hand = HandScript.main;
		hand.DiscardAllCardsInHand();

		bricks += goldAmount;

		gameTurnNumber++;

		health -= (int)(enemyNumber * damageModifier);

		goldAmount += extraGoldNextRound;

		enemyNumber += (int)Mathf.Round(offset + linear * gameTurnNumber + Mathf.Pow(exponent, gameTurnNumber));

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
		buyPoints += script.buyPoints;
		extraGoldNextRound += script.extraGoldNextRound;
		DeckScript.mainDiscard.ScrapCard(script.numberOfCardsToScrapFromDiscard);
		HandScript.main.numberOfCardsToScrap += script.numberOfCardsTosCrapFromHand;
		damageModifier = Mathf.Min(script.damgeTakenThisRoundModifier, damageModifier);

		if (enemyNumber - script.attackPoints < 0)
		{

			enemyBossHealth -= script.attackPoints - enemyNumber;
			enemyNumber = 0;
		}
		else
		{
			enemyNumber -= script.attackPoints;
		}

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
		else
		{
			return true;
		}
		return false;
	}
}
