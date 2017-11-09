using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player : MonoBehaviour
{
	private Rigidbody m_Rig;
	private Camera m_Cam;

	[SerializeField] private Vector3 m_ProjectileTarget;

	[Header("Config")]
	[SerializeField] private float m_MoveSpeed = 1;
	[SerializeField] private float m_ScaleFactor;
	[SerializeField] private GameObject m_Projectile;
	[SerializeField] private Gradient m_ColorBySize;

	[Header("Stats")]
	[SerializeField] private int m_SpaceBits;
	[SerializeField] private float m_Scale;

	private Coroutine currentScaleLerp;

	private void Awake()
	{
		m_Rig = GetComponent<Rigidbody>();
		m_Cam = Camera.main;
	}

	private void Start()
	{
		Controller_Cursor.ChangeCursorType(CursorType.crosshair);
	}

	private void Update()
	{
		m_ProjectileTarget = m_Cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
		m_Rig.velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * m_MoveSpeed;

		transform.LookAt(m_ProjectileTarget);

		if(Input.GetButton("Fire1"))
		{
			GameObject projectile = Instantiate(m_Projectile, transform.position + (transform.forward * 0.5f), transform.rotation);
			AddSpaceBits(1);
		}
	}

	public void AddSpaceBits(int amount)
	{
		m_SpaceBits += amount;
		m_Scale = Mathf.Log(m_SpaceBits, 5);
		//m_Scale = Mathf.Sqrt(m_SpaceBits);
		SetScale();
	}

	private void SetScale()
	{
		if(currentScaleLerp != null)
		{
			StopCoroutine(currentScaleLerp);
		}
		currentScaleLerp = StartCoroutine(LerpScale());
	}

	private IEnumerator LerpScale()
	{
		float currentScale = transform.localScale.x;
		float targetScale = m_Scale;
		float lerpTime = 0.2f;
		float lerpTimer = 0;

		while (lerpTimer < lerpTime)
		{
			lerpTimer += Time.deltaTime;

			m_Scale = Mathf.Lerp(currentScale, targetScale, lerpTimer / lerpTime);
			transform.localScale = Vector3.one * m_Scale;
			yield return null;
		}
	}
}
