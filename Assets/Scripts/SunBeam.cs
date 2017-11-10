using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBeam : MonoBehaviour
{
	[SerializeField] private Transform m_Player;
	private Transform m_FirstChild;
	private Transform m_LastChild;
	private LineRenderer m_LineRen;

	private void Awake()
	{
		m_FirstChild = transform.GetChild(0);
		m_LastChild = transform.GetChild(transform.childCount - 1);
		m_LineRen = GetComponent<LineRenderer>();
		m_LineRen.positionCount = transform.childCount;
	}

	private void Update()
	{
		m_LineRen.startColor = m_Player.GetComponent<Controller_Player>().GetColor();

		if(Input.GetMouseButton(0))
		{
			m_LastChild.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(m_LastChild.position, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)), Time.deltaTime * 5.0f));
		}
		else
		{
			foreach (Transform child in transform)
			{
				child.GetComponent<Rigidbody>().velocity = Vector3.zero;
				child.GetComponent<Rigidbody>().MovePosition(m_FirstChild.transform.position);
			}
		}
	}
}
