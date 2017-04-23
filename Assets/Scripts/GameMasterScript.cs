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
	public int extraCardsToTakeNextRound;
	[Range(0.01f, 1.2f)]
	public float linear;
	[Range(1f, 1.4f)]
	public float exponent;
	[Range(0f, 5f)]
	public float offset;
	public bool nextCoinGivesExtra = false;
	public int nextCoinGivesExtraMultiplyer = 1;
	private List<BuyActionDeckScript> actionDecks;

	internal void AddActionDeckAsListener(BuyActionDeckScript buyActionDeckScript)
	{
		actionDecks.Add(buyActionDeckScript);
	}


	// Use this for initialization
	void Start()
	{
		main = this;

		actionDecks = new List<BuyActionDeckScript>();

		ResetStats();
	}

	private void ResetStats()
	{
		actionPointsLeft = 1;
		goldAmount = 0;
		buyPoints = 1;
		extraGoldNextRound = 0;
		damageModifier = 1;
		extraCardsToTakeNextRound = 0;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void EndTurn()
	{
		SoundPlayerScript.main.PlayPickCardSound();
		HandScript hand = HandScript.main;
		hand.DiscardAllCardsInHand();

		bricks += goldAmount;

		gameTurnNumber++;

		health -= (int)(enemyNumber * damageModifier);

		goldAmount += extraGoldNextRound;

		enemyNumber += (int)Mathf.Round(offset + linear * gameTurnNumber + Mathf.Pow(exponent, gameTurnNumber));

		int extraCards = extraCardsToTakeNextRound;

		ResetStats();

		foreach (BuyActionDeckScript o in actionDecks)
		{
			o.ScrambleCards();
		}

		for (int i = 0; i < 5 + extraCards; i++)
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


		if (nextCoinGivesExtra && script.goldAmountOnPlay > 0)
		{
			goldAmount += script.goldAmountOnPlay * nextCoinGivesExtraMultiplyer;
			nextCoinGivesExtra = false;
			nextCoinGivesExtraMultiplyer = 1;
		} else
		{
			goldAmount += script.goldAmountOnPlay;
		}
		actionPointsLeft += script.numberOfNewActionPoints;
		if (script.numberOfCardsToDraw > 0)
		{
			SoundPlayerScript.main.PlayPickCardSound();
			HandScript.main.DrawCardsFromDeck(script.numberOfCardsToDraw);
		}
		buyPoints += script.buyPoints;
		extraGoldNextRound += script.extraGoldNextRound;
		DeckScript.mainDiscard.ScrapCard(script.numberOfCardsToScrapFromDiscard);
		HandScript.main.numberOfCardsToScrap += script.numberOfCardsTosCrapFromHand;
		damageModifier = Mathf.Min(script.damgeTakenThisRoundModifier, damageModifier);
		extraCardsToTakeNextRound += script.extraCardsToTakeNextRound;
		health += script.modifyPlayerHealth;

		if (script.nextCoinGivesExtra)
		{
			nextCoinGivesExtra = true;
			nextCoinGivesExtraMultiplyer = Mathf.Max(nextCoinGivesExtraMultiplyer, script.nextCoinGivesExtraMultiplyer);
		}

		if (script.discardHandAndDrawNewCards)
		{
			HandScript hand = HandScript.main;

			int numberOfCards = hand.NumberOfCards();
			hand.DiscardAllCardsInHand();
			hand.DrawCardsFromDeck(numberOfCards - 1);
		}

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
