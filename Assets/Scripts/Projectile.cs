using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	private float m_Speed = 5;

	private void Awake()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * m_Speed;
	}
}
