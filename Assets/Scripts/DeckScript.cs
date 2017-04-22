using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour {
	[Header("Card Stack Properties")]
	[Range(0.001f,0.1f)]
	public float cardSpacing;
	[Range(0f, 30f)]
	public float randomCardRotation;

	[Space]
	private List<GameObject> cardStack;
	private List<float> cardStackRotation;
	public bool faceUp;
	
	public GameObject basicCard;

	// Use this for initialization
	void Start () {
		cardStack = new List<GameObject>();
		cardStackRotation = new List<float>();
		faceUp = false;

		AddCardToPile(Instantiate<GameObject>(basicCard));
		AddCardToPile(Instantiate<GameObject>(basicCard));
		AddCardToPile(Instantiate<GameObject>(basicCard));
		AddCardToPile(Instantiate<GameObject>(basicCard));
		AddCardToPile(Instantiate<GameObject>(basicCard));
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < cardStack.Count; i++)
		{
			cardStack[i].transform.position = this.transform.position + this.transform.up * cardSpacing * i;
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
		GameObject element = cardStack[0];
		cardStack.Remove(element);
		return element;
	}

	public void ScrambleCards()
	{
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
}
