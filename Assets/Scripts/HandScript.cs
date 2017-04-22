using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
	public static HandScript main;
	[Range(0f, 1f)]
	public float hoverOffset;

	[Range(0.1f, 1f)]
	public float cardSpacing;

	private List<GameObject> cardStack;

	[Range(-10f, 10f)]
	public float cardRotation;

	[Range(-1f, 1f)]
	public float cardForwardOffset;

	[Range(0f, 1f)]
	public float movingSpeedOfCard;

	// Use this for initialization
	void Start()
	{
		main = this;
		cardStack = new List<GameObject>();

		DrawCardsFromDeck(5);
	}

	// Update is called once per frame
	void Update()
	{
		for (int i = 0; i < cardStack.Count; i++)
		{
			Vector3 targetPosition;
			if (cardStack[i].GetComponent<HoverOver>().isHoveringOver)
			{
				targetPosition = this.transform.position + this.transform.forward * cardForwardOffset * i + this.transform.right * cardSpacing * i + this.transform.up * hoverOffset;
			}
			else
			{
				targetPosition = this.transform.position + this.transform.forward * cardForwardOffset * i + this.transform.right * cardSpacing * i;
			}

			cardStack[i].transform.position = Vector3.Lerp(cardStack[i].transform.position, targetPosition, movingSpeedOfCard);

			cardStack[i].transform.rotation = Quaternion.Lerp(cardStack[i].transform.rotation, this.transform.rotation * Quaternion.Euler(0, 180f, 0), movingSpeedOfCard);
		}
	}

	public bool DrawCardsFromDeck()
	{
		GameObject element = DeckScript.mainDeck.TakeTopCard();
		if (element != null)
		{
			AddCardToHand(element);
			return true;
		}
		return false;
	}

	public bool DrawCardsFromDeck(int numberOfCards)
	{
		for (int i = 0; i < numberOfCards; i++)
		{
			if (!DrawCardsFromDeck())
			{
				return false;
			}
		}
		return true;
	}

	public void AddCardToHand(GameObject card)
	{
		cardStack.Add(card);
	}

	public void DiscardAllCardsInHand()
	{
		foreach (GameObject o in new List<GameObject>(cardStack))
		{
			cardStack.Remove(o);
			DeckScript.mainDiscard.AddCardToPile(o);
		}
	}


	public bool PlayCard(GameObject card)
	{
		if (cardStack.Contains(card))
		{
			if (GameMasterScript.main.CanPlayThisCard(card))
			{
				cardStack.Remove(card);
				GameMasterScript.main.ApplyCardEffects(card);
				DeckScript.mainDiscard.AddCardToPile(card);
			}
		}
		return false;
	}
}
