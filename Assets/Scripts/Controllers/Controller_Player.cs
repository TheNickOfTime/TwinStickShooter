using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player : MonoBehaviour
{
	private Rigidbody m_Rig;
	private Camera m_Cam;
	private Material m_Mat;
	private LineRenderer m_LineRen;

	[SerializeField] private Vector3 m_ProjectileTarget;

	[Header("Config")]
	[SerializeField] private float m_MoveSpeed = 1;
	[SerializeField] private float m_ScaleFactor;
	[SerializeField] private GameObject m_Projectile;
	[SerializeField] private Gradient m_ColorBySize;
	[SerializeField] private float m_ShotTimerMax;
	private float m_ShotTimerCurrent;

	[Header("Stats")]
	[SerializeField] private int m_SpaceBitsCurrent;
	[SerializeField] private int m_SpaceBitsMax;
	[SerializeField] private float m_Scale;

	private Coroutine currentScaleLerp;

	private void Awake()
	{
		m_Rig = GetComponent<Rigidbody>();
		m_Cam = Camera.main;
		m_Mat = GetComponent<Renderer>().material;
		//m_LineRen = transform.Find("SunBeam").GetComponent<LineRenderer>();
	}

	private void Start()
	{
		Controller_Cursor.ChangeCursorType(CursorType.crosshair);

		StartCoroutine(StartSequence());
	}

	private void Update()
	{
		m_ProjectileTarget = m_Cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
		m_Rig.velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * m_MoveSpeed;

		transform.LookAt(m_ProjectileTarget);

		if(Input.GetButton("Fire1") && m_ShotTimerCurrent <= 0)
		{
			m_ShotTimerCurrent = m_ShotTimerMax;
			GameObject projectile = Instantiate(m_Projectile, transform.position + (transform.forward * 0.5f), transform.rotation);
			projectile.transform.localScale = projectile.transform.localScale * (m_Scale / 5 + 0.1f);
		}
		else
		{
			m_ShotTimerCurrent -= Time.deltaTime;
		}

		Collider[] inGravity = Physics.OverlapSphere(transform.position, m_Scale);
		foreach(Collider col in inGravity)
		{
			if(col.tag == "Pickup")
			{
				if (col.GetComponent<Rigidbody>() == null)
				{
					col.gameObject.AddComponent<Rigidbody>().useGravity = false;
				}
				col.GetComponent<Rigidbody>().AddExplosionForce(-50, transform.position, m_Scale);
			}
		}
	}

	public void AddSpaceBits(int amount)
	{
		m_SpaceBitsCurrent += amount;
		m_Scale = Mathf.Log(m_SpaceBitsCurrent, 5);
		SetScale();
		SetColor();
	}

	private IEnumerator StartSequence()
	{
		yield return new WaitForSecondsRealtime(1);
		m_SpaceBitsCurrent += 2;
		//m_Scale = Mathf.Log(m_SpaceBitsCurrent, 5);
		m_Scale = Mathf.Sqrt(m_SpaceBitsCurrent);
		StartCoroutine(LerpScale(2.5f));
	}

	public float GetScale()
	{
		return m_Scale;
	}

	private void SetScale()
	{
		if(currentScaleLerp != null)
		{
			StopCoroutine(currentScaleLerp);
		}
		currentScaleLerp = StartCoroutine(LerpScale(0.5f));
	}

	private IEnumerator LerpScale(float lerpTime)
	{
		float currentScale = transform.localScale.x;
		float targetScale = m_Scale;
		float lerpTimer = 0;

		while (lerpTimer < lerpTime)
		{
			lerpTimer += Time.deltaTime;

			m_Scale = Mathf.Lerp(currentScale, targetScale, lerpTimer / lerpTime);
			transform.localScale = Vector3.one * m_Scale;
			yield return null;
		}
	}

	private void SetColor()
	{
		float value = Mathf.Lerp(0, 1, (float)m_SpaceBitsCurrent / m_SpaceBitsMax);
		m_Mat.SetColor("_EmissionColor", m_ColorBySize.Evaluate(value));
		//m_LineRen.startColor = m_ColorBySize.Evaluate(value);
	}
}
