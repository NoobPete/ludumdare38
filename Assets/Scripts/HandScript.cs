using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {
	public static HandScript main;
    [Range(0f,1f)]
    public float hoverOffset;

	[Range(0.1f, 1f)]
	public float cardSpacing;

	private List<GameObject> cardStack;

	[Range(-10f,10f)]
	public float cardRotation;

	[Range(-1f, 1f)]
	public float cardForwardOffset;

	// Use this for initialization
	void Start () {
		main = this;
		cardStack = new List<GameObject>();

		DrawCardsFromDeck(5);
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < cardStack.Count; i++)
		{
            if (cardStack[i].GetComponent<HoverOver>().isHoveringOver)
            {
                cardStack[i].transform.position = this.transform.position + this.transform.forward * cardForwardOffset * i + this.transform.right * cardSpacing * i + this.transform.up * hoverOffset;
            }
            else {
                cardStack[i].transform.position = this.transform.position + this.transform.forward * cardForwardOffset * i + this.transform.right * cardSpacing * i;
            }
			cardStack[i].transform.rotation = this.transform.rotation;
			cardStack[i].transform.Rotate(cardStack[i].transform.up, cardRotation);
		}
	}

	public bool DrawCardsFromDeck()
	{
		GameObject element = DeckScript.main.TakeTopCard();
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
			DrawCardsFromDeck();
		}
		return true;
	}

	public void AddCardToHand(GameObject card)
	{
		cardStack.Add(card);
	}

	public void DiscardAllCardsInHand()
	{
		foreach (GameObject o in cardStack)
		{

		}
	}
}
