﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame(int difficulty)
    {
		PlayerPrefs.SetInt("level", difficulty);
		SceneManager.LoadScene("main");
    }
}
