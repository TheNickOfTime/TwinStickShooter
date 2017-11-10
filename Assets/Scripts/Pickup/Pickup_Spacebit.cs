using UnityEngine;
using System.Collections;

public class Pickup_Spacebit : Pickup
{
	protected override void Awake()
	{
		base.Awake();

		GetComponent<Renderer>().material.SetColor("_EmissionColor", Random.ColorHSV(0, 1, 0.5f, 0.75f, 0.75f, 0.75f));
	}

	public override void OnPickup(Transform other)
	{
		Controller_Player.instance.AddSpaceBits(1);
		Destroy(gameObject);
	}
}
