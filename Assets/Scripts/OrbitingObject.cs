using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class OrbitingObject : MonoBehaviour
{
	// orbits around a "star" at the origin with fixed mass
	public float starMass = 1000f;

	void Start()
	{
		float initV = Mathf.Sqrt(starMass / transform.position.magnitude);
		GetComponent<Rigidbody>().velocity = new Vector3(0, initV, 0);
		GetComponent<Rigidbody>().useGravity = false;
	}

	void FixedUpdate()
	{
		float r = Vector3.Magnitude(transform.position);
		float totalForce = -(starMass) / (r * r);
		Vector3 force = (transform.position).normalized * totalForce;
		GetComponent<Rigidbody>().AddForce(force);
	}
}
