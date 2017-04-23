using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
	public static DeckScript mainDeck;
	public static DeckScript mainDiscard;

	[Header("Card Stack Properties")]
	[Range(0.001f, 0.1f)]
	public float cardSpacing;
	[Range(0f, 30f)]
	public float randomCardRotation;
	[Range(0f, 2f)]
	public float scambleOffset;
	private float currentCardSpacingOffset = 0;
	public bool isDeck;
	public bool isDiscardPile;

	public int amountOfCardsAtStart = 10;

	private List<GameObject> cardStack;
	private List<float> cardStackRotation;
	public bool isFaceUp;

	public GameObject basicCard;
	[Range(0f, 1f)]
	public float movingSpeedOfCard;

	// Use this for initialization
	void Start()
	{
		if (isDeck)
		{
			mainDeck = this;
		}
		if (isDiscardPile)
		{
			mainDiscard = this;
		}
		cardStack = new List<GameObject>();
		cardStackRotation = new List<float>();

		for (int i = 0; i < amountOfCardsAtStart; i++)
		{
			AddBasicCardToPile();
		}
	}

	// Update is called once per frame
	void Update()
	{
		currentCardSpacingOffset = currentCardSpacingOffset * 0.95f;

		float offset = cardSpacing + currentCardSpacingOffset;

		for (int i = 0; i < cardStack.Count; i++)
		{
			Vector3 targetPosition = this.transform.position + this.transform.up * offset * i;

			cardStack[i].transform.position = Vector3.Lerp(cardStack[i].transform.position, targetPosition, movingSpeedOfCard);

			if (isFaceUp)
			{
				cardStack[i].transform.rotation = this.transform.rotation * Quaternion.Euler(270f, 180f + cardStackRotation[i], 0f);
			}
			else
			{
				cardStack[i].transform.rotation = this.transform.rotation * Quaternion.Euler(90f, 0f + cardStackRotation[i], 0f);
			}
		}
	}

	public void AddCardToPile(GameObject card)
	{
		cardStack.Add(card);
		cardStackRotation.Add(UnityEngine.Random.Range(-randomCardRotation, randomCardRotation));
	}

	public GameObject TakeTopCard()
	{
		if (cardStack.Count > 0)
		{
			int target = cardStack.Count - 1;
			GameObject element = cardStack[target];
			cardStack.RemoveAt(target);
			cardStackRotation.RemoveAt(target);
			return element;
		}
		else
		{
			int cards = mainDiscard.CardCount();
			for (int i = 0; i < cards; i++)
			{
				AddCardToPile(mainDiscard.TakeTopCard());
			}

			if (cardStack.Count == 0)
			{
				return null;
			}

			ScrambleCards();

			return TakeTopCard();
		}
	}

	public int CardCount()
	{
		return cardStack.Count;
	}

	public void ScrambleCards()
	{
		currentCardSpacingOffset = scambleOffset;

		int n = cardStack.Count;
		while (n > 1)
		{
			n--;

			int k = UnityEngine.Random.Range(1, n + 1);
			GameObject value = cardStack[k];
			cardStack[k] = cardStack[n];
			cardStack[n] = value;
		}
	}

	public void AddBasicCardToPile()
	{
		AddCardToPile(Instantiate<GameObject>(basicCard));
	}

	public bool ScrapCard()
	{
		GameObject element = TakeRandomCard();

		if (element != null)
		{
			Destroy(element);
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool ScrapCard(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			bool result = ScrapCard();
			if (!result)
			{
				return false;
			}
		}
		return true;
	}

	public GameObject TakeRandomCard()
	{
		if (cardStack.Count == 0)
		{
			return null;
		}

		int target = UnityEngine.Random.Range(0, cardStack.Count - 1);

		GameObject element = cardStack[target];
		cardStack.RemoveAt(target);
		cardStackRotation.RemoveAt(target);
		return element;
	}
}
