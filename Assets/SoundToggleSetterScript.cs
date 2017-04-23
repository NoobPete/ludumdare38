using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggleSetterScript : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		if (PlayerPrefs.GetInt("sounds", 1) == 1)
		{
			GetComponent<Toggle>().isOn = true;
		}
		else
		{
			GetComponent<Toggle>().isOn = false;
		}


	}

	// Update is called once per frame
	void Update()
	{

	}
}
