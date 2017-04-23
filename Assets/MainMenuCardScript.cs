using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCardScript : MonoBehaviour
{
	public Material[] materials;
	public float lifeTime = 10;
	private float timeAlive = 0;
	public float maxRotationForce;
	public float maxForceAdded;
	public float clickForce;
	Rigidbody r;

	// Use this for initialization
	void Start()
	{
		GetComponent<Renderer>().sharedMaterial = materials[Random.Range(0, materials.Length - 1)];

		r = GetComponent<Rigidbody>();

		r.AddTorque(new Vector3(Random.Range(-maxRotationForce, maxRotationForce), Random.Range(-maxRotationForce, maxRotationForce), Random.Range(-maxRotationForce, maxRotationForce)));
		r.AddForce(new Vector3(Random.Range(-maxForceAdded, maxForceAdded), Random.Range(-maxForceAdded, maxForceAdded), Random.Range(-maxForceAdded, maxForceAdded)));
	}

	// Update is called once per frame
	void Update()
	{
		timeAlive += Time.deltaTime;

		if (timeAlive >= lifeTime)
		{
			Destroy(this.gameObject);
		}
	}

	public void OnMouseDown()
	{
		r.AddForce(new Vector3(0, clickForce, 0));
	}
}
