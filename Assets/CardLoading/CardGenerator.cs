using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGenerator : MonoBehaviour
{
	private static string[,] cardStats;
	public TextAsset csvFile;
	private static GameObject blankCardStatic;
	public GameObject blankCard;

	// Use this for initialization
	void Awake()
	{
		cardStats = CSVReader.SplitCsvGrid(csvFile.text);
		blankCardStatic = blankCard;
	}

	public static string GetItemInfo(int cardID, int column)
	{
		return cardStats[cardID, column];
	}

	public static GameObject GenerateNewCard(int cardID)
	{
		GameObject result = Instantiate(blankCardStatic);

		Text[] textFields = result.GetComponentsInChildren<Text>();

		textFields[0].text = cardStats[0, cardID];
		textFields[1].text = cardStats[1, cardID];
		textFields[2].text = cardStats[2, cardID];

		return result;
	}

	public static int NumberOfCards()
	{
		return cardStats.GetLength(1)-1;
	}
}
