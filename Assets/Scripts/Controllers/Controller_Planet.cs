using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Planet : MonoBehaviour
{
	private Controller_Player m_PlayerRef;

	private Rigidbody m_Rig;
	private Transform m_Parent;

	[SerializeField] private float m_MoveSpeed;

	private void Awake()
	{
		m_Rig = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		m_PlayerRef = Controller_Player.instance;
		GetComponent<Renderer>().material.SetColor("_EmissionColor", Random.ColorHSV(0, 1, 1, 1, 1, 1));
	}

	private void Update()
	{
		transform.LookAt(m_PlayerRef.transform.position);
		m_Rig.velocity = transform.forward * m_MoveSpeed;
	}
}
