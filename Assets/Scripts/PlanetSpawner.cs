using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
	public static PlanetSpawner instance;

	[SerializeField] private GameObject[] planets;
	[SerializeField] private int planetsCurrent;
	[SerializeField] private int planetsMax;
	[SerializeField] private float spawnDelay;
	[SerializeField] private float spawnTimer;

	private void Awake()
	{
		if(instance != null)
		{
			Destroy(this);
		}

		instance = this;
	}

	private void Start()
	{
		spawnTimer = spawnDelay;
	}

	private void Update()
	{
		spawnTimer -= Time.deltaTime;
		if(spawnTimer <= 0)
		{
			spawnTimer = spawnDelay;
			Spawn();
		}
	}

	private void Spawn()
	{
		Debug.Log("Spawning...");
		if(planetsCurrent < planetsMax)
		{
			planetsCurrent += 1;
			Instantiate(planets[Random.Range(0, planets.Length)], Random.insideUnitCircle * 300, Quaternion.identity);
		}
	}

	public void Subtract()
	{
		planetsCurrent -= 1;
	}
}
