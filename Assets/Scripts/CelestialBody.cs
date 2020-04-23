using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
	[SerializeField]
	Vector3 initialVelocity;
	const float G = 100f;
	GameObject[] Planets;
	Rigidbody rb;


	private void Start()
	{
		Planets = GameObject.FindGameObjectsWithTag("Planet");
		rb = GetComponent<Rigidbody>();
		rb.velocity = initialVelocity;
	}

	private void FixedUpdate()
	{
		Vector3 dirBetweenPlanets;
		float dis;
		float force;
		for (int i = 0; i < Planets.Length; i++)
		{
			if (Planets[i] == gameObject) continue;
			dirBetweenPlanets = Planets[i].GetComponent<Rigidbody>().position - rb.position;
			dis = dirBetweenPlanets.magnitude;
			dis *= dis;
			force = G * rb.mass * Planets[i].GetComponent<Rigidbody>().mass / dis;
			rb.AddForce(dirBetweenPlanets.normalized * force);
		}
	}

}