using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Camera : MonoBehaviour
{
	[SerializeField] private Transform m_PlayerTransform;
	private Controller_Player m_Player;
	private Camera m_Cam;
	private float m_OrgOrthoSize;

	private void Awake()
	{
		m_Player = m_PlayerTransform.GetComponent<Controller_Player>();
		m_Cam = transform.GetComponent<Camera>();
		m_OrgOrthoSize = m_Cam.orthographicSize;
	}

	private void Update()
	{
		transform.position = Vector3.Lerp(transform.position, new Vector3(m_PlayerTransform.position.x, m_PlayerTransform.position.y, transform.position.z), Time.deltaTime * 0.85f);
		m_Cam.orthographicSize = m_OrgOrthoSize + (m_Player.GetScale() * 0.75f);
	}
}
