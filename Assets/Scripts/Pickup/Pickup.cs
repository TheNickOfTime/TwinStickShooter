using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
	[SerializeField] private string[] m_InteractionTags;

	protected virtual void Awake()
	{
		//if(m_InteractionTags[0] == null)
		//{

		//}
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		foreach(string tag in m_InteractionTags)
		{
			if (other.tag == tag)
			{
				OnPickup(other.transform);
			}
		}
	}

	public virtual void OnPickup(Transform other)
	{
		Debug.Log(name + " has been interacted with");
	}
}
