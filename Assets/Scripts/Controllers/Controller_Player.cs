using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player : MonoBehaviour
{
	public static Controller_Player instance;

	private Rigidbody m_Rig;
	private Camera m_Cam;
	private Material m_Mat;
	[SerializeField] private LineRenderer m_LineRen;

	[SerializeField] private Vector3 m_ProjectileTarget;

	[Header("Config")]
	[SerializeField] private float m_MoveSpeed = 1;
	[SerializeField] private float m_ScaleFactor;
	[SerializeField] private Gradient m_ColorBySize;
	[SerializeField] private int m_SpaceBitsMax;
	private float m_ShotTimerCurrent;

	[Header("Stats")]
	[SerializeField] private int m_SpaceBitsCurrent;
	[SerializeField] private float m_Scale;
	[SerializeField] private Color m_CurrentColor;

	private Coroutine currentScaleLerp;

	private bool isAlive = true;

	private void Awake()
	{
		if(instance != null)
		{
			Destroy(this);
		}
		instance = this;

		m_Rig = GetComponent<Rigidbody>();
		m_Cam = Camera.main;
		m_Mat = GetComponent<Renderer>().material;

		Cursor.lockState = CursorLockMode.Confined;
	}

	private void Start()
	{
		Controller_Cursor.ChangeCursorType(CursorType.crosshair);

		StartCoroutine(StartSequence());
	}

	private void Update()
	{
		if(isAlive)
		{
			m_Rig.velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * m_MoveSpeed;

			Vector3[] array = new Vector3[m_LineRen.positionCount];
				m_LineRen.GetPositions(array);
				for (int i = 0; i < array.Length - 1; i++)
				{
					RaycastHit hit;
					if (Physics.Linecast(array[i], array[i + 1], out hit))
					{
						if (hit.transform.tag == "Pickup")
						{
							hit.transform.GetComponent<Pickup>().OnPickup(m_LineRen.transform);
						}
					}
				}
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
		if(isAlive)
		{
			m_SpaceBitsCurrent += amount;
			if (m_SpaceBitsCurrent >= m_SpaceBitsMax)
			{
				GetComponent<Ending>().enabled = true;
				isAlive = false;
			}


			m_Scale = Mathf.Log(m_SpaceBitsCurrent, 5);
			SetScale();
			SetColor();
		}

		UIManager.instance.SetScoreText(m_SpaceBitsCurrent, m_SpaceBitsMax);
	}

	private IEnumerator StartSequence()
	{
		yield return new WaitForSecondsRealtime(1);
		AddSpaceBits(2);
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

	public Color GetColor()
	{
		return m_CurrentColor;
	}

	private void SetColor()
	{
		float value = Mathf.Lerp(0, 1, (float)m_SpaceBitsCurrent / m_SpaceBitsMax);
		m_CurrentColor = m_ColorBySize.Evaluate(value);
		m_Mat.SetColor("_EmissionColor", m_CurrentColor);
		//m_LineRen.startColor = m_ColorBySize.Evaluate(value);
	}

	public void FlashRed()
	{
		StartCoroutine(Hurt());
	}

	private IEnumerator Hurt()
	{
		for(int i = 0; i < 10; i++)
		{
			m_Mat.SetColor("_EmissionColor", Color.red);
			yield return null;
			m_Mat.SetColor("_EmissionColor", m_CurrentColor);
			yield return null;
		}
	}

	public float GetMoveSpeed()
	{
		return m_MoveSpeed;
	}

	public int SpaceBits
	{
		get
		{
			return m_SpaceBitsCurrent;
		}
	}
}
