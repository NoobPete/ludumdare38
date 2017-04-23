using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {
	public int currentPage = 0;
	public string[] texts;

	private Text header;
	private Text bread;

	// Use this for initialization
	void Start () {
		Text[] array = this.GetComponentsInChildren<Text>();
		header = array[1];
		bread = array[2];
	}
	
	// Update is called once per frame
	void Update () {
		header.text = "Tutorial (" + (currentPage + 1) + "/" + texts.Length + ")";
		bread.text = texts[currentPage];
	}

	public void NextPage()
	{
		currentPage = Mathf.Min(texts.Length-1, currentPage + 1);
	}

	public void PriviousPage()
	{
		currentPage = Mathf.Max(0, currentPage - 1);
	}

	public void Close()
	{
		Destroy(this.gameObject);
	}
}
