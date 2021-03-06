﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Planet : Pickup
{
	[SerializeField] private int m_BitsToSpawn = 15;
	[SerializeField] private AnimationCurve m_ExplosionCurve;

	[SerializeField] private AudioClip aClip;

	private GameObject spaceBit;
	private float m_SizeInWorld;

	protected override void Awake()
	{
		base.Awake();

		spaceBit = Resources.Load("SpaceBit") as GameObject;
		m_SizeInWorld = GetComponent<Collider>().bounds.size.x;
	}

	public override void OnPickup(Transform other)
	{
		GetComponent<Collider>().enabled = false;

		AudioSource.PlayClipAtPoint(aClip, transform.position, 2);

		if (other.tag == "Player")
		{
			other.GetComponent<Controller_Player>().AddSpaceBits(-Controller_Player.instance.SpaceBits/10);
			Controller_Player.instance.FlashRed();
		}
		else if( other.tag == "Projectile")
		{
			int bitsToSpawn = Random.Range(m_BitsToSpawn + 10, m_BitsToSpawn + 10);
			Rigidbody[] rigs = new Rigidbody[bitsToSpawn];

			for(int i = 0; i < bitsToSpawn; i ++)
			{
				Vector3 randomPos = Random.insideUnitCircle;
						randomPos = (new Vector3(randomPos.x, randomPos.y, 0) + transform.position);
				Quaternion randomRot = Random.rotation;

				GameObject bit = Instantiate(spaceBit, randomPos, randomRot);
				bit.GetComponent<Pickup_Spacebit>().canParticle = false;
				Rigidbody rig = bit.AddComponent<Rigidbody>();
				rig.useGravity = false;
				rig.AddExplosionForce(25, transform.position, m_SizeInWorld);
			}
		}
		StartCoroutine(LerpSize());
		PlanetSpawner.instance.Subtract();
	}

	private IEnumerator LerpSize()
	{
		float timer = 0.5f;
		float time = 0;
		Vector3 scale = transform.localScale;

		//GetComponent<OrbitingObject>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;

		while(time < timer)
		{
			time += Time.deltaTime;
			transform.localScale = scale * m_ExplosionCurve.Evaluate(time / timer);
			yield return null;
		}
		Destroy(gameObject);
	}
}
