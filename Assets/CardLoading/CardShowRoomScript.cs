using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShowRoomScript : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		int numberOfCards = CardGenerator.NumberOfCards();

		for (int i = 0; i < numberOfCards; i++)
		{
			GameObject o = CardGenerator.GenerateNewCard(i + 1);
			o.transform.position = new Vector3(-1.2f * i, 0, 0);
		}

	}

	// Update is called once per frame
	void Update()
	{

	}
}
