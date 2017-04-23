using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGameScript : MonoBehaviour
{
	public bool isClosed = true;
	private RectTransform rect;

	// Use this for initialization
	void Start()
	{
		rect = GetComponent<RectTransform>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			isClosed = !isClosed;
		}

		if (isClosed)
		{
			rect.position = new Vector3(30000, 30000);
		}
		else
		{
			rect.position = new Vector3(Screen.width/2, Screen.height/2);
		}
	}

	public void Close()
	{
		isClosed = true;
	}

	public void Quit()
	{
		SceneManager.LoadScene("startMenu");
	}
}
