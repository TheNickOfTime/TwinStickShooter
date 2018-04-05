using UnityEngine;
using System.Collections;

public class Pickup_Spacebit : Pickup
{
	[SerializeField] private AudioClip aClip;
	[SerializeField] private Gradient m_Color;

	public bool canParticle = true;

	protected override void Awake()
	{
		base.Awake();

		GetComponent<Renderer>().material.SetColor("_EmissionColor", Random.ColorHSV(0, 1, 0.5f, 0.75f, 0.75f, 0.75f));
		Color c = GetComponent<Renderer>().material.GetColor("_EmissionColor");
		m_Color = new Gradient();

		m_Color.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 1.0f), new GradientAlphaKey(1.0f, 1.0f) }
            );
	}

	public override void OnPickup(Transform other)
	{
		Controller_Player.instance.AddSpaceBits(1);
		AudioSource.PlayClipAtPoint(aClip, transform.position);
		if(canParticle)
		{
			GameObject particle = Instantiate(SpaceJunkSpawner.instance.m_Particles, transform.position, Quaternion.identity);
			var cl = particle.GetComponent<ParticleSystem>().colorOverLifetime;
			cl.color = new ParticleSystem.MinMaxGradient(m_Color);
		}
		Destroy(gameObject);
	}
}
