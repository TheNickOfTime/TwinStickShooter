using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;

	[SerializeField] private Canvas m_Canvas;
	[SerializeField] private RectTransform m_Crosshair;

	[SerializeField] private Vector2 m_ScreenScaleRatio;

	private void Awake()
	{
		if(instance != null)
		{
			Destroy(this);
		}
		instance = this;

		CanvasScaler canvasScaler = m_Canvas.GetComponent<CanvasScaler>();

		m_ScreenScaleRatio = new Vector2(canvasScaler.referenceResolution.x / Screen.width
										,canvasScaler.referenceResolution.y / Screen.height);
	}

	public void SetCrosshairPos()
	{
		Vector2 pos = new Vector2(Input.mousePosition.x * m_ScreenScaleRatio.x, Input.mousePosition.y * m_ScreenScaleRatio.y);

		m_Crosshair.anchoredPosition = pos;
	}
}
