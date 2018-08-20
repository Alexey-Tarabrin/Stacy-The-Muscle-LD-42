using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{

	public Slider HealthBar;
	public Text HeightText;
	public Text PauseHeightText;
	public PauseMenu PauseMenu;
	public GameObject GameUI;
	public void ReloadScene()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void PauseGame()
	{
		PauseHeightText.text = HeightText.text;
		Time.timeScale = 0;
		GameDataManager.Instance.IsPaused = true;
	}

	public void UnpauseGame()
	{
		Time.timeScale = 1;
		GameDataManager.Instance.IsPaused = false;
	}
	public void QuitGame()
	{
		Application.Quit();
	}

	public void ActivatePause()
	{

		StartCoroutine(DelayedPause());

	}

	IEnumerator DelayedPause()
	{
		yield return new WaitForSeconds(3);
		
		GameUI.SetActive(false);
		PauseMenu.gameObject.SetActive(true);
	}

}
