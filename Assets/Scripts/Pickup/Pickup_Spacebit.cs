using UnityEngine;
using System.Collections;

public class Pickup_Spacebit : Pickup
{
	protected override void OnPickup(Transform other)
	{
		other.GetComponent<Controller_Player>().AddSpaceBits(5);
	}
}
