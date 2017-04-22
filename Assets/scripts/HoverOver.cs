using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOver : MonoBehaviour
{
	public bool isHoveringOver = false;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	// Called on the first frame the mouse is over the object
	private void OnMouseOver()
	{
		isHoveringOver = true;
	}

	// Called on the first frame when the mouse is not hovering over the object
	private void OnMouseExit()
	{
		isHoveringOver = false;
	}
}
