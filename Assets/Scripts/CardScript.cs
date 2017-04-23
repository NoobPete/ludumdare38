using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
	public int goldAmountOnPlay = 0;
	public int cost = 0;

	public int numberOfCardsToDraw = 0;
	public int numberOfNewActionPoints = 0;

	public int attackPoints = 0;
	public int buyPoints = 0;
	public int extraGoldNextRound = 0;
	public int numberOfCardsToScrapFromDiscard = 0;
	public int numberOfCardsTosCrapFromHand = 0;
	public float damgeTakenThisRoundModifier = 1;
	public int extraCardsToTakeNextRound = 0;
	public bool discardHandAndDrawNewCards = false;

	[Header("Action card")]
	public bool isActionCard = false;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnMouseDown()
	{
		HandScript.main.PlayCard(this.gameObject);
	}
}
