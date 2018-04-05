using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceJunkSpawner : MonoBehaviour
{
	public static SpaceJunkSpawner instance;

	[SerializeField] private int m_Amount;
	[SerializeField] private int m_Sections;
	[SerializeField] private GameObject[] m_Objects;
	[SerializeField] private Transform m_Parent;

	public GameObject m_Particles;


	private void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}
	}

	private IEnumerator Start()
	{
		int per = m_Amount / m_Sections;

		for (int i = 0; i < m_Sections; i ++)
		{
			for (int j = 0; j < per; j++)
			{
				GameObject junk = Instantiate(m_Objects[Random.Range(0, m_Objects.Length)], Random.insideUnitCircle * 300, Random.rotation, m_Parent);
			}
			yield return null;
		}

		
	}
}
