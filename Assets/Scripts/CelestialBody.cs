using UnityEngine;

public class CelestialBody : MonoBehaviour
{
	//  Initial velocity of the planet so it won't hit the sun when we start the game.
	[SerializeField]
	Vector3 initialVelocity;
	
	//  This will save the list of other planets at the start so we won't have to calculate them again each time at runtime.
	GameObject[] Planets;

	//  Support for gravitiy force calculation.
	Rigidbody rb;
	Vector3 dirBetweenPlanets;
	float dis;
	float force;


	private void Start()
	{
		Planets = GameObject.FindGameObjectsWithTag("Planet");
		rb = GetComponent<Rigidbody>();
		rb.velocity = initialVelocity;
	}

	//  This applies Newton's gravitational law at each fixed update.
	private void FixedUpdate()
	{
		for (int i = 0; i < Planets.Length; i++)
		{
			if (Planets[i] == gameObject) continue;
			dirBetweenPlanets = Planets[i].GetComponent<Rigidbody>().position - rb.position;
			dis = dirBetweenPlanets.magnitude;
			dis *= dis;
			force = GameManager.G * rb.mass * Planets[i].GetComponent<Rigidbody>().mass / dis;
			rb.AddForce(dirBetweenPlanets.normalized * force);
		}
	}
}