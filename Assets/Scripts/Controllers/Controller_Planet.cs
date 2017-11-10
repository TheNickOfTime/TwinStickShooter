using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Planet : MonoBehaviour
{
	private Controller_Player m_PlayerRef;

	private Rigidbody m_Rig;
	private Transform m_Parent;

	[SerializeField] private float m_MoveSpeed;
	[SerializeField] private float m_RotateSpeed;
	private int m_RotationDirection;
	private float m_DistanceFromPlayer;

	private enum EnemyState { moving, orbiting};
	private EnemyState m_EnemyState;

	private void Awake()
	{
		m_Rig = GetComponent<Rigidbody>();

		m_RotationDirection = Random.Range(0, 2);
		switch(m_RotationDirection)
		{
			case 0:
				m_RotationDirection = 1;
				break;
			case 1:
				m_RotationDirection = -1;
				break;
		}
	}

	private void Start()
	{
		m_PlayerRef = Controller_Player.instance; 
		m_EnemyState = EnemyState.moving;
	}

	private void Update()
	{
		m_DistanceFromPlayer = Vector3.Distance(transform.position, m_PlayerRef.transform.position);

		switch(m_EnemyState)
		{
			case EnemyState.moving:
				transform.LookAt(m_PlayerRef.transform.position);
				m_Rig.velocity = transform.forward * m_MoveSpeed;
				if(m_DistanceFromPlayer < 8)
				{
					m_EnemyState = EnemyState.orbiting;
				}
				break;

			case EnemyState.orbiting:
				if (m_Parent == null)
				{
					m_Parent = Instantiate(new GameObject().transform, m_PlayerRef.transform.position, Quaternion.Euler(- new Vector3(0, 0, transform.eulerAngles.y))) as Transform;
					transform.parent = m_Parent;
				}
				m_Parent.transform.position = m_PlayerRef.transform.position;
				m_Parent.Rotate(Vector3.forward, Time.deltaTime * m_RotateSpeed /** m_PlayerRef.GetMoveSpeed()*/);
				transform.LookAt(m_PlayerRef.transform.position);
				m_Rig.velocity = transform.forward * 0.25f;
				break;
		}
	}
}
