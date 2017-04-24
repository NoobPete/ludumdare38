using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WonGameScript : MonoBehaviour
{
	private Text score;

	// Use this for initialization
	void Start()
	{
		Text[] array = this.GetComponentsInChildren<Text>();
		score = array[2];

		GameMasterScript gm = GameMasterScript.main;

		int bosshp = Mathf.Max(0, gm.enemyBossHealth);

		int level = PlayerPrefs.GetInt("level", 1);

		score.text = "Final score: " + Mathf.Round(1000*(Mathf.Pow((2), (level + 2)) * (gm.health + 100) / (gm.gameTurnNumber + 20)) - (100 * bosshp));
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Quit()
	{
		SceneManager.LoadScene("startMenu");
	}
}
