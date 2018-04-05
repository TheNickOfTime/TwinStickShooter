using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeColor : MonoBehaviour
{
	private void Update()
	{
		GetComponent<Text>().CrossFadeColor(Color.clear, 5, true, true);
	}
}
