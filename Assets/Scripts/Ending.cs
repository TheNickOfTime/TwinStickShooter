using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
	[SerializeField] private float m_ImplodeTime = 1;
	//[SerializeField] private float 

	[SerializeField] private AnimationCurve m_ExplosionCurve;
	[SerializeField] private AudioClip aClip;

	private Light m_Light;

	private void Start()
	{
		//Destroy(GetComponent<Controller_Player>());
		m_Light = transform.GetChild(0).GetComponent<Light>();

		StartCoroutine(EndingSequence());
	}

	private IEnumerator EndingSequence()
	{
		Camera.main.GetComponent<Controller_Camera>().enabled = false;
		Camera.main.GetComponent<CameraShake>().enabled = true;

		Collider[] objects = Physics.OverlapSphere(transform.position, 20);

		foreach (Collider col in objects)
		{
			if(col.transform.tag == "Pickup")
			{
				Rigidbody rig = col.GetComponent<Rigidbody>();
				if(rig == null)
				{
					rig = col.gameObject.AddComponent<Rigidbody>();
				}
				rig.useGravity = false;
				rig.AddExplosionForce(-500, transform.position, 40);
			}
		}

		AudioSource.PlayClipAtPoint(aClip, transform.position);

		yield return LerpSize();
		Time.timeScale = 0.01f;
		yield return new WaitForSecondsRealtime(1);
		Time.timeScale = 1;

		foreach (Collider col in objects)
		{
			if(col != null)
			{
				Rigidbody rig = col.GetComponent<Rigidbody>();
				rig.velocity = Vector3.zero;
				rig.AddExplosionForce(1500, transform.position, 40);
			}
		}

		yield return UIManager.instance.Fade();
		SceneManager.LoadScene(0);

	}

	private IEnumerator LerpSize()
	{
		float timer = m_ImplodeTime;
		float time = 0;
		Vector3 scale = transform.localScale;

		//GetComponent<OrbitingObject>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;

		while (time < timer)
		{
			time += Time.deltaTime;
			transform.localScale = scale * m_ExplosionCurve.Evaluate(time / timer);

			m_Light.intensity = m_ExplosionCurve.Evaluate(time / timer);

			yield return null;
		}
	}
}
