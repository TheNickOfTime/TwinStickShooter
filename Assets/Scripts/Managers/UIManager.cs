using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;

	[SerializeField] private Canvas m_Canvas;
	[SerializeField] private Text m_Score;
	[SerializeField] private Slider m_ScoreSlider;
	private Image m_SliderBackground;
	private Image m_SliderBar;
	private Outline m_TextOutline;
	[SerializeField] private Image m_Fade;

	[SerializeField] private GameObject m_PausePanel;
	private bool isPaused;

	private void Awake()
	{
		if(instance != null)
		{
			Destroy(this);
		}
		instance = this;

		m_SliderBackground = m_ScoreSlider.transform.GetChild(0).GetComponent<Image>();
		m_SliderBar = m_ScoreSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
		m_TextOutline = m_ScoreSlider.transform.GetChild(2).GetComponent<Outline>();
	}

	private void Start()
	{
		StartCoroutine(FadeRev());
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(isPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	public void SetScoreText(int current, int max)
	{
		// m_Score.text = current.ToString("D4") + "/" + max;
		m_ScoreSlider.value = (float)current/max;
		m_SliderBackground.color = Controller_Player.instance.GetColor();
		m_SliderBar.color = Controller_Player.instance.GetColor();
		m_TextOutline.effectColor = Controller_Player.instance.GetColor();
	}

	public void SetFadeAlpha(float alpha)
	{
		m_Fade.color = new Color(0, 0, 0, alpha);
	}

	public IEnumerator Fade()
	{
		float time = 3;
		float timer = 0;
		while(timer < time)
		{
			timer += Time.deltaTime;

			SetFadeAlpha(timer / time);

			yield return null;
		}
	}

	public IEnumerator FadeRev()
	{
		float time = 3;
		float timer = time;
		while (timer > 0)
		{
			timer -= Time.deltaTime;

			SetFadeAlpha(timer / time);

			yield return null;
		}
	}

	public void Pause()
	{
		m_PausePanel.SetActive(true);
		isPaused = true;
		Time.timeScale = 0;
	}

	public void Resume()
	{
		m_PausePanel.SetActive(false);
		isPaused = false;
		Time.timeScale = 0;
	}

	public void QuitGame()//Quits game based on if it is the Unity Editor or not
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}
