using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawnerScript : MonoBehaviour
{
	public GameObject spawn;
	public float delay = 0.4f;
	private float timePassed = 0;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		timePassed += Time.deltaTime;

		if (timePassed> delay)
		{
			GameObject o = Instantiate(spawn);
			o.transform.position = this.transform.position;

			timePassed -= delay;
		}


		if (Input.GetMouseButtonDown(0))
		{
			GameObject o = Instantiate(spawn);
			o.transform.position = this.transform.position;
		}
	}
}
