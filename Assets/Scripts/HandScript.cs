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

    [Range(0f,1f)]
    public float movingSpeedOfCard;

    // Use this for initialization
    void Start () {
		main = this;
		cardStack = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < cardStack.Count; i++)
		{
            Vector3 targetPosition;
            if (cardStack[i].GetComponent<HoverOver>().isHoveringOver)
            {
                targetPosition = this.transform.position + this.transform.forward * cardForwardOffset * i + this.transform.right * cardSpacing * i + this.transform.up * hoverOffset;
            }
            else {
                targetPosition = this.transform.position + this.transform.forward * cardForwardOffset * i + this.transform.right * cardSpacing * i;
            }

            cardStack[i].transform.position = Vector3.Lerp(cardStack[i].transform.position, targetPosition, movingSpeedOfCard);

			cardStack[i].transform.rotation = this.transform.rotation;
			cardStack[i].transform.Rotate(cardStack[i].transform.up, cardRotation);
		}
	}

	public void AddCardToHand(GameObject card)
	{
		cardStack.Add(card);
	}
}
