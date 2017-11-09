using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceJunkSpawner : MonoBehaviour
{
	[SerializeField] private int m_Amount;
	[SerializeField] private GameObject[] m_Objects;
	[SerializeField] private Transform m_Parent;

	private void Start()
	{
		for (int i = 0; i < m_Amount; i++)
		{
			GameObject junk = Instantiate(m_Objects[Random.Range(0, m_Objects.Length)], Random.insideUnitCircle * 300, Random.rotation, m_Parent);
			//junk.GetComponent<Rigidbody>().AddTorque(Random.rotation.eulerAngles / 2);
		}
	}
}
