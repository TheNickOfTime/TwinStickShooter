using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private float m_Speed = 5;

	private Camera mCamera;

	private float mHalfWidth;
	private float mHalfHeight;

	private void Awake()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * m_Speed;

		mCamera = Camera.main;

		mHalfHeight = mCamera.pixelHeight;
		mHalfWidth = mCamera.pixelWidth;
	}

	private void Update()
	{
		Vector3 screenPoint = mCamera.WorldToScreenPoint(transform.position);

		if (screenPoint.x <= 0 || screenPoint.x >= mHalfWidth || screenPoint.y <= 0 || screenPoint.y >= mHalfHeight)
		{
			Destroy(gameObject);
		}
	}
}
