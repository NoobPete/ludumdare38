using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyDeckScript : MonoBehaviour {
	[Range(0.001f,0.1f)]
	public float cardSpacing;

	[Range(0f, 2f)]
	public float scambleOffset;
	private float currentCardSpacingOffset = 0;

	public int numberOfCards = 10;

	private List<GameObject> cardStack;
	public bool isFaceUp;
	
	public GameObject basicCard;
	[Range(0f, 1f)]
	public float movingSpeedOfCard;

	// Use this for initialization
	void Start () {
		cardStack = new List<GameObject>();

		for (int i = 0; i < numberOfCards; i++)
		{
			AddBasicCardToPile();
		}
	}
	
	// Update is called once per frame
	void Update () {
		currentCardSpacingOffset = currentCardSpacingOffset * 0.95f;

		float offset = cardSpacing + currentCardSpacingOffset;

		for (int i = 0; i < cardStack.Count; i++)
		{
			Vector3 targetPosition = this.transform.position + this.transform.up * offset * i;

			cardStack[i].transform.position = targetPosition;

			if (isFaceUp)
			{
				cardStack[i].transform.rotation = this.transform.rotation * Quaternion.Euler(270f, 180f, 0f);
			} else
			{
				cardStack[i].transform.rotation = this.transform.rotation * Quaternion.Euler(90f, 0f, 0f);
			}
		}
	}

    public void AddCardToPile(GameObject card)
    {
		cardStack.Add(card);
    }

	public GameObject TakeTopCard()
	{
		if (cardStack.Count > 0)
		{
			if (GameMasterScript.main.CanBuyThisCard(cardStack[0])) {
				GameObject element = cardStack[0];
				cardStack.RemoveAt(0);
				return element;
			}
		}

		return null;
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

			int k = Random.Range(1, n + 1);
			GameObject value = cardStack[k];
			cardStack[k] = cardStack[n];
			cardStack[n] = value;
		}
	}

	public void AddBasicCardToPile()
	{
		AddCardToPile(Instantiate<GameObject>(basicCard));
	}

	public void OnMouseDown()
	{
		GameObject element = TakeTopCard();

		if (element != null)
		{
			DeckScript.mainDiscard.AddCardToPile(element);
		}
	}
}
