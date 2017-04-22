using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour {
	public static DeckScript main;

	[Header("Card Stack Properties")]
	[Range(0.001f,0.1f)]
	public float cardSpacing;
	[Range(0f, 30f)]
	public float randomCardRotation;
	[Range(0f, 2f)]
	public float scambleOffset;
	private float currentCardSpacingOffset = 0;

	public int amountOfCardsAtStart = 10;

	private List<GameObject> cardStack;
	private List<float> cardStackRotation;
	public bool faceUp;
	
	public GameObject basicCard;

	// Use this for initialization
	void Start () {
		main = this;
		cardStack = new List<GameObject>();
		cardStackRotation = new List<float>();
		faceUp = false;

		for (int i = 0; i < amountOfCardsAtStart; i++)
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
			cardStack[i].transform.position = this.transform.position + this.transform.up * offset * i;
			cardStack[i].transform.rotation = this.transform.rotation;
			cardStack[i].transform.Rotate(cardStack[i].transform.right, 90);
			cardStack[i].transform.Rotate(this.transform.forward, cardStackRotation[i]);
		}
	}

    public void AddCardToPile(GameObject card)
    {
		cardStack.Add(card);
		cardStackRotation.Add(Random.Range(-randomCardRotation, randomCardRotation));
    }

	public GameObject TakeTopCard()
	{
		if (cardStack.Count > 0)
		{
			GameObject element = cardStack[0];
			cardStack.RemoveAt(0);
			cardStackRotation.RemoveAt(0);
			return element;
		} else
		{
			return null;
		}
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
}
