using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBeam : MonoBehaviour
{
	private Transform m_LastChild;
	private LineRenderer m_LineRen;

	private void Awake()
	{
		m_LastChild = transform.GetChild(transform.childCount - 1);
		m_LineRen = GetComponent<LineRenderer>();
		m_LineRen.positionCount = transform.childCount;
	}

	private void Update()
	{
		m_LastChild.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(m_LastChild.position, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)), Time.deltaTime * 5.0f));

		for(int i = 0; i < transform.childCount; i ++)
		{
			m_LineRen.SetPosition(i, transform.GetChild(i).position);
		}
	}
}
