using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// UIINTERACTIONS_BUTTON
/// Nick Cunningham
/// Handles various button interactions
/// </summary>

public class UIInteractions_Button : MonoBehaviour
{
	private UIInteractions_Canvas canvasInteractions;//The canvas interactions script that this script will reference
	private Button buttonRef;

	private void Awake()
	{
		canvasInteractions = GetComponent<UIInteractions_Canvas>();
	}

	public void SetActivePanel(string panelName)//Sets the active panel and disables previous one
	{
		canvasInteractions.SetActivePanel(panelName);
	}

	public void SetActivePanelAdditive(string panelName)//Sets the active panel and disables previous one
	{
		canvasInteractions.SetActivePanelAdditive(panelName);
	}

	public void SetActivePanelPrevious()
	{
		canvasInteractions.SetActivePanelPrevious();
	}

	public void SetButtonRef(Transform targetButton)
	{
		buttonRef = targetButton.GetComponent<Button>();
	}

	public void SetButtonText(string text)//Requires buttonRef to be set
	{
		if(buttonRef != null)
		{
			buttonRef.transform.GetChild(0).GetComponent<Text>().text = text;
		}
	}

	public void SetButtonVisibility(bool backgroundOnly)//Requires buttonRef to be set
	{
		if(!backgroundOnly && buttonRef.gameObject.activeSelf == true || backgroundOnly && buttonRef.GetComponent<Image>().enabled == true)
		{
			if (backgroundOnly)
			{
				buttonRef.GetComponent<Image>().enabled = false;
				buttonRef.GetComponent<Button>().enabled = false;
			}
			else
			{
				buttonRef.gameObject.SetActive(false);
			}
		}
		else
		{
			if (backgroundOnly)
			{
				buttonRef.GetComponent<Image>().enabled = true;
				buttonRef.GetComponent<Button>().enabled = true;
			}
			else
			{
				buttonRef.gameObject.SetActive(true);
			}
		}
	}

	public void OpenLevel(string levelName)//Opens level by string name
	{
		SceneManager.LoadScene(levelName);
	}

	public void OpenLevel(int buildIndex)//Opens level by build index
	{
		SceneManager.LoadScene(buildIndex);
	}

	public void Pause()
	{
		Time.timeScale = 0;
	}

	public void Resume()
	{
		Time.timeScale = 1;
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
